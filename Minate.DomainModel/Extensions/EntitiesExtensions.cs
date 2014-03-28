namespace Minate.DomainModel.Extensions
{
    using System.Collections.Generic;
    using Entities;

    public static class EntitiesExtensions
    {
        public static User Update(this User user, User updated)
        {
            if (!user.Equals(updated))
            {
                user.Identifier = updated.Identifier;
                user.Username = updated.Username ?? user.Username;
                user.Password = updated.Password ?? user.Password;

                user.Name = updated.Name ?? user.Name;
                user.Email = updated.Email ?? user.Email;
                user.Location = updated.Location ?? user.Location;
                user.Image = updated.Image ?? user.Image;
                user.Birthday = updated.Birthday ?? user.Birthday;
                user.Biography = updated.Biography ?? user.Biography;
            }

            return user;
        }

        public static Game Update(this Game game, Game updated)
        {
            if (!game.Equals(updated))
            {
                game.Identifier = updated.Identifier;

                game.Board = updated.Board ?? game.Board;
                game.CurrentPlayer = updated.CurrentPlayer ?? game.CurrentPlayer;

                game.Players = new List<Game.Player>(updated.Players);
                game.Plays = new List<Game.Play>(updated.Plays);
                game.Messages = new List<Game.Message>(updated.Messages);
            }

            return game;
        }
    }
}
