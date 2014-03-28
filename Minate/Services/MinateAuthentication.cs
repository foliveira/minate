namespace Minate.Services
{
    using System.Web.Security;

    using DomainModel.Repositories.Interfaces;
    using Interfaces;

    public class MinateAuthentication : IAuthenticationService
    {
        private readonly UserRepository _usersRepository;

        public MinateAuthentication(UserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void Login(string username, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}