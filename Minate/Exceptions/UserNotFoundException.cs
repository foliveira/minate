namespace Minate.Exceptions
{
    using System;

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : this("User not found")
        {
        }

        private UserNotFoundException(string msg)
            : base(msg)
        {
            
        }
    }
}