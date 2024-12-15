using TVStation.Data.DTO.Plans;

namespace TVStation.Repositories.IRepositories
{
    public interface IPlanRepository<T,Q> : IGenericRepository<T>
    {
        PlanListDTO<T> GetAllPaging(Q query);
        IEnumerable<T> GetByStatus(string status);
    }
}
