using Microsoft.EntityFrameworkCore;
using TVStation.Data.Model;
using TVStation.Repositories.IRepositories;

namespace TVStation.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> 
        where T : class, IEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public virtual T? Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }


        public virtual T? Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) throw new KeyNotFoundException("Data not found");
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual T? GetById(Guid id)
        {
            return _context.Set<T>().Where(s => s.Id.Equals(id)).FirstOrDefault();
        }

        public virtual T? Update(Guid id, object entity)
        {
            var tEntity = GetById(id);
            if (tEntity == null) throw new KeyNotFoundException("Data not found");
            _context.Entry(tEntity).CurrentValues.SetValues(entity);
            _context.Entry(tEntity).Property(x => x.Id).IsModified = false;
            _context.SaveChanges();
            return tEntity;
        }

    }
}
