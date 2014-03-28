namespace Minate.Extensions
{
    using System.Linq;

    using DomainModel.Entities;
    using DomainModel.Repositories.Interfaces;
    using Exceptions;

    public static class RepositoriesExtensions
    {
        public static User GetUser(this UserRepository repository, int userid)
        {
            var user = repository.FetchBy(u => u.Identifier == userid);

            if (user.Any())
                return user.First();

            throw new UserNotFoundException();
        }

        public static Game GetGame(this GameRepository repository, int gameid)
        {
            if (gameid >= 0)
            {
                var game = repository.FetchBy(g => g.Identifier == gameid);

                if (game.Any())
                    return game.First();
            }

            throw new GameNotFoundException();
        }
    }
}