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
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(QueryFilter<T>? queryFilter);
        T? GetById(string id, string includeProperties = "");
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(string id);
        void DeleteRange(IEnumerable<string> ids);
        int Save();
    }
}
