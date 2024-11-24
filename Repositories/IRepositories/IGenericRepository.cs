using TVStation.Data.Model;

namespace TVStation.Repositories.IRepositories
{
    public class Paginated<T> where T : class
    {
        public IEnumerable<T> Content { get; set; } = new List<T>();
        public int TotalPages { get; set; }

    }
    public interface IGenericRepository<T,Q> where T : class where Q : class
    {
        Paginated<T> GetAll(Q query);
        T? GetById(Guid id);
        T? Create(T entity);
        T? Update(Guid id, T entity);
        T? Delete(Guid id);
        T? DeletePermanent(Guid id);
    }
}
