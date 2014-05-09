
namespace Mhotivo.Logic
{
    public interface ISessionManagement
    {
        bool LogIn(string userName, string password, bool remember = false);

        void LogOut(bool redirect = false);

        string GetUserLoggedName(string userName, string password);

        string GetUserLoggedRole();

    }
}