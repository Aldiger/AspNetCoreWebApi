using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assecor.Data.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Return an item by its primary key. The item may be inacctive.
        /// </summary>
        /// <param name="id">Object primary key</param>
        /// <returns>Object of type <see cref="TEntity"/></returns>
        TEntity Get(int id);

        /// <summary>
        /// Return all items of <see cref="TEntity"/>
        /// </summary>
        /// <returns>Collection</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Return a collection of type <see cref="TEntity}"/> based on the provided predicate.
        /// </summary>
        /// <param name="predicate">Conditions to search for an item.</param>
        /// <returns>Collection</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Return an asyncronus result collection of type <see cref="TEntity"/> on on provided predicate
        /// </summary>
        /// <param name="predicate">Conditions to search for this collection</param>
        /// <returns>Collection</returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a bool indicating the predicate is true of false
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///  Returns a bool indicating the predicate is true of false
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        System.Linq.IQueryable<TEntity> FindComplex(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        List<TEntity> FindComplex(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        ///// <summary>
        ///// Return a collection of 8 items
        ///// </summary>
        ///// <param name="predicate">Condition on search</param>
        ///// <param name="pageNumber">Requested Page Number</param>
        ///// <param name="includes">Requested Includes objects</param>
        ///// <returns></returns>
        //ListPaginated<TEntity> FindComplexPaginated(Expression<Func<TEntity, bool>> predicate, int pageNumber, params string[] includes);

        ///// <summary>
        ///// Return a collection of 8 items
        ///// </summary>
        ///// <param name="predicate">Condition to search on</param>
        ///// <param name="pageNumber">Requested page number</param>
        ///// <returns>Collection</returns>
        //ListPaginated<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageNumber);

        /// <summary>
        /// Return single or default for a <see cref="TEntity"/> object.
        /// Use this only in cases when is neccessary to search for one and only one item in db table. This runs a full scan.
        /// </summary>
        /// <param name="predicate">Predicate to search for this item</param>
        /// <returns>Object of type <see cref="TEntity"/></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Return first or default instance of <see cref="TEntity"/> object
        /// </summary>
        /// <param name="predicate">Predicate to search for this item</param>
        /// <returns>Object</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Return first or default element
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Insert a new object of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity">Object</param>
        void Add(TEntity entity);

        TEntity AddWithReturn(TEntity entity);

        /// <summary>
        /// Updates an object of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity">Object</param>
        /// <param name="key">Existent object id</param>
        void Update(TEntity entity, int key);

        /// <summary>
        /// Insert a collection of items of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="entities">Collection</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove/Deactivate an object from our db based in object identifyer
        /// </summary>
        /// <param name="id">integer representing unique identifyer of this object</param>
        void Remove(int id);

        /// <summary>
        /// Remove an object from our db.
        /// </summary>
        /// <param name="entity">Object to be removed</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove a collection of objects from our db
        /// </summary>
        /// <param name="entities">Collection to remove</param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
