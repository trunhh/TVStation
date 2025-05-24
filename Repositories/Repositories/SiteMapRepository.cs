using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class SiteMapRepository : GenericRepository<SiteMap>, ISiteMapRepository
    {
        public SiteMapRepository(AppDbContext context) : base(context) { }

        public override SiteMap? GetById(Guid id)
        {
            return _context.SiteMap.Include(s => s.Members).FirstOrDefault(s => s.Id == id);
        }
    }
}
