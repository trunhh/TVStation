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
            var queryable = base.GetQueriedData(query);
            if (query.StartDate != null)
            {
                queryable = queryable.Where(s => s.Airdate > query.StartDate);
            }
            if (query.EndDate != null) {
                queryable = queryable.Where(s => s.Airdate < query.EndDate);
            }
            if (query.SiteMapId != null && query.SiteMapId != Guid.Empty)
            {
                queryable = queryable
                    .Include(s => s.SiteMap)
                    .AsQueryable()
                    .Where(s => s.SiteMap != null && s.SiteMap.Id == query.SiteMapId);
            }
            return queryable;
        }
    }
}
