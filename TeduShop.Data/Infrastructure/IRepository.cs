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
        T Add(T entity);

        //Update Entity
        void Update(T Entity);

        //Delete Entity
        T Delete(T Entity);

        T Delete(int id);

        //Delete Multi Entity
        void DeleteMulti(Expression<Func<T, bool>> where);

        //Get Single Entity By ID
        T GetSingleById(int id);

        //Get Single Entity By Condition
        T GetEntityByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}