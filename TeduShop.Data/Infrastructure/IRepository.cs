using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        //Add new Entity
        void Add(T entity);

        //Update Entity
        void Update(T Entity);

        //Delete Entity
        void Delete(T Entity);

        void Delete(int id);

        //Delete Multi Entity
        void DeleteMulti(Expression<Func<T, bool>> where);

        //Get Single Entity By ID
        T Get(int id);

        //Get Single Entity By Condition
        T GetEntityByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetAll(string[] includes = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}