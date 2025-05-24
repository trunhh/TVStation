using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(AppDbContext context) : base(context) { }

        public List<Episode> CheckSchedulingConflict(Episode episode)
        {
            if (episode.Programme?.Channel == null)
                return new List<Episode>();

            return _context.Episode
                .Include(e => e.Programme)
                .ThenInclude(p => p.Channel)
                .Where(e => e.Id != episode.Id && !e.IsDeleted)
                .Where(e => e.Programme != null && !e.Programme.IsDeleted)
                .Where(e => e.Programme.Channel.Id == episode.Programme.Channel.Id)
                .Where(e => e.Start < episode.End && e.End > episode.Start)
                .ToList();
        }
    }
}
