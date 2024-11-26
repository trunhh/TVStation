using TVStation.Data.Model;
using TVStation.Data.Response;

namespace TVStation.Repositories.IRepositories
{
    public interface IGenericRepository<T,Q> where T : class where Q : class
    {
        IResponse GetAll(Q query);
        T? GetById(Guid id);
        T? Create(T entity);
        T? Update(Guid id, T entity);
        T? Delete(Guid id);
        T? DeletePermanent(Guid id);
    }
}
