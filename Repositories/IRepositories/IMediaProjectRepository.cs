using TVStation.Data.Model.Plans;
using TVStation.Data.QueryObject.Plans.Productions;
using TVStation.Data.DTO;

namespace TVStation.Repositories.IRepositories
{
    public interface IMediaProjectRepository 
        : IPlanRepository<MediaProject, MediaProjectQuery>
    {
    }
}
