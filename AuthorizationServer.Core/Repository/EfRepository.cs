using AuthorizationServer.Core.MyCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Core.Repository
{
    public partial class EfRepository<T> : IRepository<T> where T : EntityBase
    {
        private DbSet<T> _entities;

        private readonly MyContext _context;

        public EfRepository(MyContext context)
        {
            this._context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public Task<T> GetByIdAsync(object id)
        {
            return this.Entities.FindAsync(id);
        }

        public List<T> GetAll()
        {
            return this.Entities.ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return this.Entities.ToListAsync();
        }

        public List<T> ToList(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).ToList();
        }

        public Task<List<T>> ToListAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).ToListAsync();
        }

        public T Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);

                this._context.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);

                await this._context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entity");
                }

                foreach (var entity in entities)
                {
                    this.Entities.Add(entity);
                }

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this.Entities.Add(entity);
                }

                await this._context.SaveChangesAsync();

                return entities.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this._context.Entry(entity).State = EntityState.Modified;

                this._context.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this._context.Entry(entity).State = EntityState.Modified;

                await this._context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this._context.Entry(entity).State = EntityState.Modified;
                }

                this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this._context.Entry(entity).State = EntityState.Modified;
                }

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }


                foreach (var entity in entities)
                {
                    this.Entities.Remove(entity);
                }

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this.Entities.Remove(entity);
                }

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Count(predicate);
        }

        public async Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await this.Entities.CountAsync(predicate);
        }

        public T IncludeSingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            this.Include(paths);

            return this.Entities.SingleOrDefault(predicate);
        }

        public async Task<T> IncludeSingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            IQueryable<T> query = this.Entities;

            foreach (var path in paths)
            {
                query = query.Include(path);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<T>> IncludeToListAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            IQueryable<T> query = this.Entities;

            foreach (var path in paths)
            {
                query = query.Include(path);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.SingleOrDefault(predicate);
        }

        public Task<T> SingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.SingleOrDefaultAsync(predicate);
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.FirstOrDefault(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await this.Entities.FirstOrDefaultAsync(predicate);
        }

        public void Include(params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            foreach (var path in paths)
            {
                this.Entities.Include(path);
            }
        }

        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }

                return _entities;
            }
        }
    }
}
