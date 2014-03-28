namespace Minate.DomainModel.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Entities;
    using Interfaces;
    using Extensions;

    /// <summary>
    /// A memory based repository for users.
    /// </summary>
    public class InMemoryUserRepository : UserRepository
    {
        private readonly IList<User> _users;

        /// <summary>
        /// Creates the necessary backend to support the management of users.
        /// </summary>
        public InMemoryUserRepository()
        {
            _users = new List<User>();
        }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public override int Add(User entity)
        {
            _users.Add(entity);

            return entity.Identifier = _users.IndexOf(entity);
        }

        /// <summary>
        /// Updates a set of entities on the repository matching a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to update must match.</param>
        /// <param name="entity">A entity which contains the update data to be applied to all the matched entities.</param>
        /// <returns>An enumeration with all the updated entities.</returns>
        public override IEnumerable<User> Update(Predicate<User> predicate, User entity)
        {
            return _users.Where(u => predicate(u)).Select(user => user.Update(entity)).ToList();
        }

        /// <summary>
        /// Deletes a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to delete must match.</param>
        /// <returns>An enumeration with all the deleted entities.</returns>
        public override IEnumerable<User> Delete(Predicate<User> predicate)
        {
            return _users.Where(u => predicate(u)).Where(user => _users.Remove(user)).ToList();
        }

        /// <summary>
        /// Gets all the entities that are in the repository.
        /// </summary>
        public override IEnumerable<User> FetchAll()
        {
            return _users.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a set of entities that match a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to which the entities to fetch must match.</param>
        /// <returns>An enumeration with all the matched entities.</returns>
        public override IEnumerable<User> FetchBy(Predicate<User> predicate)
        {
            return _users.Where(u => predicate(u)).ToList().AsReadOnly();
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