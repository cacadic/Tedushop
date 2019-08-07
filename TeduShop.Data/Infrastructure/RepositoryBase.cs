using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private TeduShopDbContext dbContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        public TeduShopDbContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = DbFactory.Init());
            }
        }

        public RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #endregion

        #region Implements

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual bool CheckContains(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public virtual void Delete(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public virtual void Delete(int id)
        {
            T entity = GetSingleById(id);
            dbSet.Remove(entity);
        }

        public virtual void DeleteMulti(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return DbContext.Set<T>().AsQueryable();
        }

        public virtual T GetEntityByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, string[] includes = null)
        {
            return GetAll(includes).FirstOrDefault(expression);
        }

        public virtual IQueryable<T> GetMulti(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return DbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IQueryable<T> GetMultiPaging(System.Linq.Expressions.Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? DbContext.Set<T>().Where<T>(predicate).AsQueryable() : DbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public virtual void Update(T Entity)
        {
            dbSet.Attach(Entity);
            DbContext.Entry<T>(Entity).State = EntityState.Modified;
        }

        #endregion
    }
}