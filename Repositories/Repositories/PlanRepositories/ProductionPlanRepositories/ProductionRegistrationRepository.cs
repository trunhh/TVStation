using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories.PlanRepositories.ProductionPlanRepositories
{
    public class ProductionRegistrationRepository :
        ProductionPlanRepository<ProductionRegistration, ProductionRegistrationQuery>, IProductionRegistrationRepository
    {
        public ProductionRegistrationRepository(AppDbContext context) : base(context) { }
    }
}
