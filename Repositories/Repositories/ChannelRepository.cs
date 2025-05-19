using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class ChannelRepository : GenericRepository<Channel>, IChannelRepository
    {
        public ChannelRepository(AppDbContext context) : base(context) { }
    }
}
