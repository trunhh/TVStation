using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject;
using TVStation.Data.QueryObject.Plans;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories
{
    public abstract class ProductionPlanRepository<T, Q> : PlanRepository<T, Q>
        where T : class, IEntity, IProduction where Q : class, IPagingQuery, IProductionQuery
    {
        public ProductionPlanRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override IQueryable<T> GetQueriedData(Q query)
        {
            var queryable = base.GetQueriedData(query)
                .Where(s => s.Airdate > query.StartDate
                && s.Airdate < query.EndDate)
                .Include(s => s.SiteMap)
                .AsQueryable();
            if (query.SiteMapId != Guid.Empty)
            {
                queryable = queryable.Where(s => s.SiteMap != null && s.SiteMap.Id == query.SiteMapId);
            }
            return queryable;
        }
    }
}
