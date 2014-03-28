namespace Minate.Services
{
    using System.Linq;
    using System.Web.Security;

    using Interfaces;
    using DomainModel.Entities;
    using DomainModel.Repositories.Interfaces;

    public class MinateMembership : IMembershipService
    {
        private readonly UserRepository _usersRepository;

        public MinateMembership(UserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public bool CreateUser(User user)
        {
            if (_usersRepository.FetchBy(u => string.Equals(u.Username, user.Username)).Any())
                return false;

            user.Password = EncryptPassword(user.Password);
            user.Identifier = _usersRepository.Add(user);
            _usersRepository.SubmitChanges();

            return true;
        }

        public bool RemoveUser(string username)
        {
            var user = _usersRepository.Delete(u => string.Equals(u.Username, username));
            _usersRepository.SubmitChanges();

            return user.Any();
        }

        public bool ValidateUser(string username, string password)
        {
            var user = _usersRepository.FetchBy(u => string.Equals(u.Username, username));

            if(!user.Any())
                return false;

            var stPassword = user.First().Password;
            var encPassword = EncryptPassword(password);

            return string.Equals(stPassword, encPassword);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _usersRepository.FetchBy(u => string.Equals(username, u.Username));

            if (!user.Any() && !string.Equals(user.First().Password, EncryptPassword(oldPassword))) 
                return false;

            user.First().Password = EncryptPassword(newPassword);
            _usersRepository.SubmitChanges();

            return true;
        }

        private static string EncryptPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
        }
    }
}