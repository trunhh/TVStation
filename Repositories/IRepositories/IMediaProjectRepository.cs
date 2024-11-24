using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.Request;

namespace TVStation.Repositories.IRepositories
{
    public interface IMediaProjectRepository 
        : IGenericRepository<MediaProject, MediaProjectQuery>
    {
    }
}
