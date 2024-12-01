namespace TVStation.Repositories.IRepositories
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        T? Create(T entity);
        T? Update(Guid id, T entity);
        T? Delete(Guid id);
        T? DeletePermanent(Guid id);
    }
}
