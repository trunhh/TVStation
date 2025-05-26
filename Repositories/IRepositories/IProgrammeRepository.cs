using TVStation.Data.DTO;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;

namespace TVStation.Repositories.IRepositories
{
    public interface IProgrammeRepository  : IGenericRepository<Programme>
    {
        List<Programme> GetAll(ProgrammeQuery query);
    }
}
