using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class SiteMapRepository : GenericRepository<SiteMap>, ISiteMapRepository
    {
        public SiteMapRepository(AppDbContext context) : base(context) { }
    }
}
