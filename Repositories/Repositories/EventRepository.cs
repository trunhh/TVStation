using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context) { }

        public EventListDTO GetAll(EventQuery query)
        {
            var queryable = _context.Event.Where(s => s.IsDeleted == false);
            var listtt = queryable.ToList();
            if (!string.IsNullOrEmpty(query.Keyword)) queryable = queryable.Where(s => s.Title.Contains(query.Keyword));
            if (!string.IsNullOrEmpty(query.Sector)) queryable = queryable.Where(s => s.Sector == query.Sector);
            if (!string.IsNullOrEmpty(query.Status)) queryable = queryable.Where(s => s.Status == query.Status);
            listtt = queryable.ToList();
            queryable = queryable.Include(m => m.Creator).Include(e => e.Collaborators);
            if (query.CreatorId != null) queryable = queryable.Where(e => 
                (e.Creator != null && e.Creator.Id == query.CreatorId) ||
                (e.Collaborators != null && e.Collaborators.Any(c => c.Id == query.CreatorId)));

            listtt = queryable.ToList();
            

            var channelList = queryable.Include(e => e.Channel).Where(e => e.Channel != null).Select(e => e.Channel).Distinct().ToList();
            var eventList = queryable.Select(e => e.Map<Event, EventResDTO>()).ToList();
            listtt = queryable.ToList();
            return new EventListDTO
            {
                EventList = eventList,
                ChannelList = channelList,
                TotalCount = queryable.Count(),
                ApprovedCount = queryable.Where(p => p.Status == PlanStatus.Approved).Count(),
                InProgressCount = queryable.Where(p => p.Status == PlanStatus.InProgress).Count(),
                WaitingApprovalCount = queryable.Where(p => p.Status == PlanStatus.WaitingForApproval).Count()
            };
        }
    }
}
