namespace Minate.DomainModel.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

    /// <summary>
    /// 
    /// </summary>
    public abstract class GameRepository : IRepository<int, Game> 
    {
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <remarks>
        /// Should not add the created entity to the repository. That operation should be supported via the Add(entity) method.
        /// </remarks>
        /// <returns>The created entity.</returns>
        public Game Create()
        {
            return new Game();
        }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public abstract int Add(Game entity);

        /// <summary>
        /// Updates a set of entities on the repository matching a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to update must match.</param>
        /// <param name="entity">A entity which contains the update data to be applied to all the matched entities.</param>
        /// <returns>An enumeration with all the updated entities.</returns>
        public abstract IEnumerable<Game> Update(Predicate<Game> predicate, Game entity);

        /// <summary>
        /// Deletes a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to delete must match.</param>
        /// <returns>An enumeration with all the deleted entities.</returns>
        public abstract IEnumerable<Game> Delete(Predicate<Game> predicate);

        /// <summary>
        /// Gets all the entities that are in the repository.
        /// </summary>
        public abstract IEnumerable<Game> FetchAll();

        /// <summary>
        /// Gets a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to fetch must match.</param>
        /// <returns>An enumeration with all the matched entities.</returns>
        public abstract IEnumerable<Game> FetchBy(Predicate<Game> predicate);

        /// <summary>
        /// Submits the changes to the underlying repository implementation.
        /// </summary>
        public abstract void SubmitChanges();
    }
}
