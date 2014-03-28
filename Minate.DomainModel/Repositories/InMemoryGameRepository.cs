using System.Data.Linq;
using Minate.DomainModel.Extensions;

namespace Minate.DomainModel.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;
    using Interfaces;

    /// <summary>
    /// A memory based repository for games.
    /// </summary>
    public class InMemoryGameRepository : GameRepository
    {
        private readonly IList<Game> _games;

        /// <summary>
        /// Creates the necessary backend for storing games in memory.
        /// </summary>
        public InMemoryGameRepository()
        {
            _games = new List<Game>();
        }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public override int Add(Game entity)
        {
            _games.Add(entity);

            return entity.Identifier = _games.IndexOf(entity);
        }

        /// <summary>
        /// Updates a set of entities on the repository matching a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to update must match.</param>
        /// <param name="entity">A entity which contains the update data to be applied to all the matched entities.</param>
        /// <returns>An enumeration with all the updated entities.</returns>
        public override IEnumerable<Game> Update(Predicate<Game> predicate, Game entity)
        {
            return _games.Where(g => predicate(g)).Select(g => g.Update(entity)).ToList();
        }

        /// <summary>
        /// Deletes a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to delete must match.</param>
        /// <returns>An enumeration with all the deleted entities.</returns>
        public override IEnumerable<Game> Delete(Predicate<Game> predicate)
        {
            IList<Game> deleted = _games.Where(game => predicate(game)).ToList();
            deleted.All(g => _games.Remove(g));
            return deleted;
        }

        /// <summary>
        /// Gets all the entities that are in the repository.
        /// </summary>
        public override IEnumerable<Game> FetchAll()
        {
            return _games.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to fetch must match.</param>
        /// <returns>An enumeration with all the matched entities.</returns>
        public override IEnumerable<Game> FetchBy(Predicate<Game> predicate)
        {
            return _games.Where(g => predicate(g)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Submits the changes to the underlying repository implementation.
        /// </summary>
        public override void SubmitChanges()
        {
            return;
        }
    }
}