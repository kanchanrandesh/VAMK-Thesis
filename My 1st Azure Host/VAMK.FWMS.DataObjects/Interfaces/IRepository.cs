using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VAMK.FWMS.Models.Interfaces;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IRepository<T> where T : IEntityBase
    {
        /// <summary>
        /// Load the proxy object for given Type
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        T GetProxy(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Get the given type with collections,related object 
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Get Collection of type for given expression
        /// </summary>
        /// <example>
        /// Usage: _repository.GetAll(t=>t.id==10);
        /// </example>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        IList<T> GetAll(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Get collection of All objects in the underlining database.
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Get collection of last ten modified objects in the underlining database.
        /// </summary>
        /// <returns></returns>
        IList<T> GetLastTenModified(int domainID);

        /// <summary>
        /// Get collection of last ten created objects in the underlining database.
        /// </summary>
        /// <returns></returns>
        IList<T> GetLastTenCreated(int domainID);

        /// <summary>
        /// Returns a Query that supports for quering the collections
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Provide funtionality to Update/Edit/Delete given type/collections.
        /// </summary>
        /// <param name="entity"></param>
        int Save(T entity);

        /// <summary>
        /// Provide Function to delete entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(int id);
    }
}
