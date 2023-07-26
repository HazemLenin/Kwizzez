using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.Domain.Common;

namespace Kwizzez.DAL.Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null,
            string includeProperties = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> orderExpression = null,
            int take = 0, int skip = 0);
        T? GetById(string id, string includeProperties = "");
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        int Save();
    }
}
