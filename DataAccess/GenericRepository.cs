#region Directives 
using EF;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IDataAccess;
#endregion

namespace DataAccess
{
    /// <summary>
    ///  Generic repository class for common CRUD operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //EF DB context
        internal PopulationAndHouseholdDataContext context;

        //DB Set for DB table entities 
        internal DbSet<TEntity> dbSet;

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(PopulationAndHouseholdDataContext context)
        {
            // Assign DB context 
            this.context = context;
            // Assign EF context entities
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        ///  Load EF entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns>IEnumerable<TEntity></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)


            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        ///  Get EF entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TEntity</returns>
        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        ///  Add value to DB entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>void</returns>
        public virtual async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        /// <summary>
        ///  Delete EF Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        /// <summary>
        ///  Delete EF entity
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        ///  Update FE entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
