using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(AppDbContext context) : base(context) { }

        public List<Episode> GetAll(EpisodeQuery query)
        {
            var queryable = _context.Episode.Where(s => DateOnly.FromDateTime(s.Start) == query.Date);            

            queryable = queryable
                        .Include(m => m.Programme)
                        .ThenInclude(p => p.Channel)
                        .Where(e => e.Programme != null && e.Programme.Channel != null && e.Programme.Channel.Id == query.ChannelId);

            return queryable.ToList();
        }


        public List<Episode> CheckSchedulingConflict(Episode episode)
        {
            if (episode.Programme?.Channel == null)
                return new List<Episode>();

            return _context.Episode
                .Include(e => e.Programme)
                .ThenInclude(p => p.Channel)
                .Where(e => e.Id != episode.Id)
                .Where(e => e.Programme != null)
                .Where(e => e.Programme.Channel.Id == episode.Programme.Channel.Id)
                .Where(e => e.Start < episode.End && e.End > episode.Start)
                .ToList();
        }   

        public int GetNextIndex(Guid progId)
        {
            return _context.Episode.Include(e => e.Programme)
                .Where(e => e.Programme != null && e.Programme.Id == progId)
                .Count() + 1;
        }


        public override Episode? GetById(Guid id)
        {
            return _context.Episode
                    .Where(s => s.Id.Equals(id))
                    .Include(e => e.Programme)
                    .FirstOrDefault();
        }

        public override Episode? Delete(Guid id)
        {
            var entity = base.Delete(id);
            if (entity != null)
            {
                var nextEps = _context.Episode.Include(e => e.Programme).AsQueryable();


                nextEps = nextEps.Where(e => e.Programme.Id == entity.Programme.Id && e.Index > entity.Index);

                foreach (var ep in nextEps) ep.Index--;
            }
            _context.SaveChanges();
                

            return entity;
        }
    }
}
