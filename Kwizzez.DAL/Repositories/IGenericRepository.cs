using Kwizzez.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Repositories
{
    public interface IGenericRepository<T> where T : Base
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null,
            string includeProperties = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> orderExpression = null,
            int take = 0, int skip = 0);
        T? GetById(Guid id);
        void Add(T entity);
        void AddRange(T[] entities);
        void Update(T entity);
        void UpdateRange(T[] entities);
        void Delete(T entity);
        void DeleteRange(T[] entities);
    }
}
