namespace Minate.Exceptions
{
    using System;

    public class GameNotFoundException : Exception
    {
        public GameNotFoundException() : this("Requested game not found")
        {
        }

        private GameNotFoundException(string msg)
            : base(msg)
        {
            
        }
    }
}