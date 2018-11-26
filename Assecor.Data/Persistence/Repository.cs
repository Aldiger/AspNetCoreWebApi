using Assecor.Data;
using Assecor.Data.Core;
using DataAccess.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Assecor.Data.Persistence
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDBContext Context;
        protected const int _pageSize = 8;

        public Repository(ApplicationDBContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            //if (entity.GetType().GetProperty("Modified") != null)
            //{
            //    entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
            //}
            //if(entity.GetType().GetProperty("ModifiedBy") != null)
            //{
            //    entity.GetType().GetProperty("ModifiedBy").SetValue(entity, actorId);
            //}
            //if(entity.GetType().GetProperty("Active") != null)
            //{
            //    entity.GetType().GetProperty("Active").SetValue(entity, true);
            //}
            Context.Set<TEntity>().Add(entity);
        }
        public TEntity AddWithReturn(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            //foreach (var entity in entities)
            //{
            //    if (entity.GetType().GetProperty("Modified") != null)
            //    {
            //        entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
            //    }
            //    if (entity.GetType().GetProperty("ModifiedBy") != null)
            //    {
            //        entity.GetType().GetProperty("ModifiedBy").SetValue(entity, actorId);
            //    }
            //    if (entity.GetType().GetProperty("Active") != null)
            //    {
            //        entity.GetType().GetProperty("Active").SetValue(entity, true);
            //    }
            //}
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity,int key)
        {
            if (entity == null)
                return;

            var existing = Context.Set<TEntity>().Find(key);

            if (existing != null)
            {
                //if (entity.GetType().GetProperty("Modified") != null)
                //{
                //    entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
                //}
                //if (entity.GetType().GetProperty("ModifiedBy") != null)
                //{
                //    entity.GetType().GetProperty("ModifiedBy").SetValue(entity);
                //}
                //if (entity.GetType().GetProperty("Active") != null)
                //{
                //    entity.GetType().GetProperty("Active").SetValue(entity, true);
                //}
                Context.Entry(existing).CurrentValues.SetValues(entity);
            }

        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).AsEnumerable();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Any();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).AnyAsync();
        }

        public IQueryable<TEntity> FindComplex(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.Where(predicate);
        }

        public List<TEntity> FindComplex(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            foreach (var include in includes)
                query = query.Include(include);

            return query.Where(predicate).ToList();
        }

        //public ListPaginated<TEntity> FindComplexPaginated(Expression<Func<TEntity, bool>> predicate,int pageNumber, params string[] includes)
        //{
        //    IQueryable<TEntity> query = Context.Set<TEntity>();
        //    if (!string.IsNullOrEmpty(includes[0]))
        //    {
        //        foreach (var include in includes)
        //            query = query.Include(include);

        //    }

        //    var items = query.Where(predicate)
        //                      .Skip((pageNumber - 1) * _pageSize)
        //                      .Take(_pageSize)
        //                      .ToList();

        //    var totalCount = Context.Set<TEntity>().Where(predicate).Count();

        //    var obj = new ListPaginated<TEntity>
        //    {
        //        Items = items,
        //        PageSize = _pageSize,
        //        CurrentPage = pageNumber,
        //        TotalCount = totalCount
        //    };

        //    return obj;


        //}

        //public ListPaginated<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageNumber)
        //{
        //    var items = Context.Set<TEntity>().Where(predicate)
        //                       .Skip((pageNumber - 1) * _pageSize)
        //                       .Take(_pageSize)
        //                       .ToList();

        //    var totalCount = Context.Set<TEntity>().Where(predicate).Count();

        //    var obj = new ListPaginated<TEntity>
        //    {
        //        Items = items,
        //        PageSize = _pageSize,
        //        CurrentPage = pageNumber,
        //        TotalCount = totalCount
        //    };

        //    return obj;
        //}

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Remove(int id)
        {
            var dbItem = Context.Set<TEntity>().Find(id);
            Context.Set<TEntity>().Remove(dbItem);
            //if(dbItem != null)
            //{
            //    if (dbItem.GetType().GetProperty("Modified") != null)
            //    {
            //        dbItem.GetType().GetProperty("Modified").SetValue(dbItem, DateTime.Now);
            //    }
            //    if (dbItem.GetType().GetProperty("ModifiedBy") != null)
            //    {
            //        dbItem.GetType().GetProperty("ModifiedBy").SetValue(dbItem, actorId);
            //    }
            //    if (dbItem.GetType().GetProperty("Active") != null)
            //    {
            //        dbItem.GetType().GetProperty("Active").SetValue(dbItem, false);
            //    }
            //}
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
	}
}
