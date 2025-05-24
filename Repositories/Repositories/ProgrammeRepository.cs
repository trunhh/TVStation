using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO.Event;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class ProgrammeRepository : GenericRepository<Programme>, IProgrammeRepository
    {
        public ProgrammeRepository(AppDbContext context) : base(context) { }

        public override Programme? Update(Guid id, object entity)
        {
            var prog = base.Update(id, entity);
            if (prog != null) return GetById(id);
            return null;
        }

        public ProgrammeListDTO GetAll(ProgrammeQuery query)
        {
            var queryable = _context.Programme.Where(s => s.IsDeleted == false);
            if (!string.IsNullOrEmpty(query.Keyword)) queryable = queryable.Where(s => s.Title.Contains(query.Keyword));
            if (!string.IsNullOrEmpty(query.Status)) queryable = queryable.Where(s => s.Status == query.Status);
            
            queryable = queryable.Include(m => m.Owner).Include(e => e.Collaborators).ThenInclude(c => c.User);
            if (query.UserName != null) queryable = queryable.Where(e => e.Owner != null && e.Owner.UserName == query.UserName);
            //|| (e.Collaborators != null && e.Collaborators.Any(c => c.User.UserName == query.UserName)));

            queryable = queryable.Include(m => m.Channel);
            if (query.ChannelId != null) queryable = queryable.Where(p => p.Channel != null && p.Channel.Id == query.ChannelId);

            queryable = queryable.Include(m => m.SiteMap);
            if (query.SiteMapId != null) queryable = queryable.Where(p => p.SiteMap != null && p.SiteMap.Id == query.SiteMapId);

            return new ProgrammeListDTO
            {
                List = queryable.ToList(),
                TotalCount = queryable.Count(),
                ApprovedCount = queryable.Where(p => p.Status == PlanStatus.Approved).Count(),
                InProgressCount = queryable.Where(p => p.Status == PlanStatus.InProgress).Count(),
                WaitingApprovalCount = queryable.Where(p => p.Status == PlanStatus.WaitingForApproval).Count()
            };
        }

        public override Programme? GetById(Guid id)
        {
            return _context.Programme
                    .Where(s => s.Id.Equals(id) && s.IsDeleted == false)
                    .Include(p => p.Collaborators)
                    .Include(m => m.Channel)
                    .Include(m => m.SiteMap)
                    .Include(p => p.Episodes)
                    .FirstOrDefault();
        }
    }
}
