using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Data;
using Kwizzez.Domain.Common;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Kwizzez.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.CreatedAt = DateTime.UtcNow;

            _context.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.DeletedAt = DateTime.UtcNow;

            _context.Set<T>().RemoveRange(entities);
        }

        public IQueryable<T> GetAll(QueryFilter<T>? queryFilter)
        {
            var query = _context.Set<T>().AsNoTracking();

            foreach (var property in queryFilter.IncludeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            if (queryFilter.Filter != null)
                query = query.Where(queryFilter.Filter);

            if (queryFilter.OrderExpression != null)
                query = queryFilter.OrderExpression(query);

            if (queryFilter.Skip > 0)
                query = query.Skip(queryFilter.Skip);

            if (queryFilter.Take > 0)
                query = query.Take(queryFilter.Take);

            return query;
        }

        public IQueryable<T> GetAll() => _context.Set<T>().AsNoTracking();

        public T? GetById(string id, string includeProperties = "")
        {
            var query = _context.Set<T>().AsNoTracking();

            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

            return query.FirstOrDefault(e => e.Id == id);
        }

        public void Update(T entity)
        {
            var oldEntity = GetById(entity.Id);
            entity.CreatedAt = oldEntity.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var oldEntity = GetById(entity.Id);
                entity.CreatedAt = oldEntity.CreatedAt;
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _context.Set<T>().UpdateRange(entities);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
