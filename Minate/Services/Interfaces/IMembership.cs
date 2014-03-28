namespace Minate.Services.Interfaces
{
    using DomainModel.Entities;

    public interface IMembershipService
    {
        bool CreateUser(User user);
        bool RemoveUser(string username);
        
        bool ValidateUser(string username, string password);
        bool ChangePassword(string username, string oldPassword, string newPassword);
    }
}