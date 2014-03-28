namespace Minate.Services.Interfaces
{
    public interface IAuthenticationService
    {
        void Login(string userName, bool createPersistentCookie);
        void Logout();
    }
}
