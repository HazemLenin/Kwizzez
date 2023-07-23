using Kwizzez.DAL.Data;
using Kwizzez.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(T[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null,
            string includeProperties = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> orderExpression = null,
            int take = 0, int skip = 0)
        {
            var query = _dbSet.AsNoTracking();

            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            if (filter != null)
                query = query.Where(filter);

            if (orderExpression != null)
                query = orderExpression(query);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return query.ToList();
        }

        public T? GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
