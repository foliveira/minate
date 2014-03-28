using System.Data.Linq.Mapping;
using System.Linq;

namespace Minate.DomainModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// An entity that represents an User for the application
    /// </summary>
    public class User
    {
        public User()
        {
            Friends = new List<Friend>();
        }

        #region User Properties

        /// <summary>
        /// An unique identifier for the user.
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// The user name in the application.
        /// </summary>
        /// <remarks>Required</remarks>
        [Required(ErrorMessage = "Userame is required")]
        public string Username { get; set; }

        /// <summary>
        /// The user password.
        /// </summary>
        /// <remarks>It should be encrypted before storing.</remarks>
        /// <remarks>Required</remarks>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// The user real name.
        /// </summary>
        /// <remarks>Required</remarks>
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        /// <summary>
        /// The user email.
        /// </summary>
        /// <remarks>Required</remarks>
        [Required(ErrorMessage = "Invalid Email")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$")]
        public string Email { get; set; }

        /// <summary>
        /// The user location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The user birthday.
        /// </summary>
        /// <remarks>Used to keep track of age</remarks>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// A short bio about the user
        /// </summary>
        [StringLength(512, ErrorMessage = "The biography shouldn't be more than 512 words")]
        public string Biography { get; set; }

        /// <summary>
        /// The user profile image
        /// </summary>
        public ImageFile Image { get; set; }

        /// <summary>
        /// A list of this user friends
        /// </summary>
        public IList<Friend> Friends { get; set; }

        #endregion

        #region User friends management

        /// <summary>
        /// Adds an user as a friend for the current user.
        /// </summary>
        /// <param name="friend">The user to befriend</param>
        public void AddFriend(User friend)
        {
            if (!Friends.Where(f => f.UserId == friend.Identifier).Any())
                Friends.Add(new Friend
                                {
                                    Username = friend.Username,
                                    UserId = friend.Identifier,
                                    Confirmed = true
                                });

            if (!friend.Friends.Where(f => f.UserId == Identifier).Any())
                friend.Friends.Add(new Friend
                                       {
                                           Username = this.Username,
                                           UserId = Identifier,
                                           Confirmed = false
                                       });
        }

        /// <summary>
        /// Confirms a user as a friend
        /// </summary>
        /// <param name="friend">The user to confirm as friend</param>
        public void ConfirmFriend(User friend)
        {
            var user = Friends.Where(f => f.UserId == friend.Identifier && !f.Confirmed).First();
            user.Confirmed = true;
        }

        /// <summary>
        /// Removes an user from the current user friends list.
        /// </summary>
        /// <param name="friend">The user to remove as friend</param>
        /// <returns>True if removal is sucessful</returns>
        public bool RemoveFriend(User friend)
        {
            return Friends.Remove(Friends.Where(f => string.Equals(f.Username, friend.Username)).FirstOrDefault());
        }

        #endregion

        #region Helper class

        /// <summary>
        /// A class to store the user avatar information
        /// </summary>
        public class ImageFile
        {
            /// <summary>
            /// The image type.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// The image binary content.
            /// </summary>
            public byte[] Content { get; set; }
        }

        public class Friend
        {
            public string Username { get; set; }

            public int UserId { get; set; }

            public bool Confirmed { get; set; }
        }

        #endregion
    }
}