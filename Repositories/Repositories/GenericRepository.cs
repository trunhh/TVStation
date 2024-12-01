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
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual T? Delete(Guid id)
        {
            var tEntity = _context.Set<T>()
              .AsNoTracking()
              .FirstOrDefault(e => e.Id.Equals(id));
            if (tEntity != null)
            {
                tEntity.IsDeleted = true;
                try
                {
                    _context.Set<T>().Update(tEntity).Property(x => x.Id).IsModified = false;
                    _context.SaveChanges();
                    return tEntity;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public virtual T? DeletePermanent(Guid id)
        {
            var entity = _context.Set<T>().FirstOrDefault(e => e.Id.Equals(id));
            if (entity != null)
            {
                try
                {
                    _context.Set<T>().Remove(entity);
                    _context.SaveChanges();
                    return entity;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().Where(s => s.IsDeleted == false);
        }

        public virtual T? GetById(Guid id)
        {
            return _context.Set<T>()
                .AsNoTracking().Where(s => s.Id.Equals(id) && s.IsDeleted == false).FirstOrDefault();
        }

        public virtual T? Update(Guid id, T entity)
        {
            var tEntity = _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id.Equals(id));
            if (tEntity != null)
            {
                try
                {
                    _context.Set<T>().Update(entity).Property(x => x.Id).IsModified = false;
                    _context.SaveChanges();
                    return tEntity;
                }
                catch (Exception)
                {
                    return null;
                }

            }
            return null;
        }
    }
}
