namespace Minate.DomainModel.Repositories.Interfaces
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// A generic interface for implementing a repository for entities.
    /// </summary>
    /// <remarks>
    /// Supports the creation, addition, update and remotion of entities from the repository, aswell as fetching and persisting of data to a database
    /// </remarks>
    /// <typeparam name="TIdentityType"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<out TIdentityType, TEntity> 
        where TEntity : new()
    {
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <remarks>
        /// Should not add the created entity to the repository. That operation should be supported via the Add(entity) method.
        /// </remarks>
        /// <returns>The created entity.</returns>
        TEntity Create();

        #region Mutability Enabling Operations

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        TIdentityType Add(TEntity entity);

        /// <summary>
        /// Updates a set of entities on the repository matching a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to update must match.</param>
        /// <param name="entity">A entity which contains the update data to be applied to all the matched entities.</param>
        /// <returns>An enumeration with all the updated entities.</returns>
        IEnumerable<TEntity> Update(Predicate<TEntity> predicate, TEntity entity);

        /// <summary>
        /// Deletes a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to delete must match.</param>
        /// <returns>An enumeration with all the deleted entities.</returns>
        IEnumerable<TEntity> Delete(Predicate<TEntity> predicate);

        #endregion

        #region Fetch Operations

        /// <summary>
        /// Gets all the entities that are in the repository.
        /// </summary>
        IEnumerable<TEntity> FetchAll();

        /// <summary>
        /// Gets a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to fetch must match.</param>
        /// <returns>An enumeration with all the matched entities.</returns>
        IEnumerable<TEntity> FetchBy(Predicate<TEntity> predicate);

        #endregion

        #region Persistence Operations
        
        /// <summary>
        /// Submits the changes to the underlying repository implementation.
        /// </summary>
        void SubmitChanges();

        #endregion
    }
}
