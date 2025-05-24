using TVStation.Data.DTO.Plans;
using TVStation.Data.Model;
using TVStation.Data.QueryObject;

namespace TVStation.Repositories.IRepositories
{
    public interface IProgrammeRepository  : IGenericRepository<Programme>
    {
        ProgrammeListDTO GetAll(ProgrammeQuery query);
    }
}
