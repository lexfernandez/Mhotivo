using System.Linq;
using System.Web;
using System.Web.Security;
using Mhotivo.App_Data.Repositories;


namespace Mhotivo.Logic
{
    public class SessionLayer: ISessionManagement
    {
        private static SessionLayer _instance;
        private static readonly UserRepository UserRepo = UserRepository.Instance;
        private readonly string _userNameIdentifier;
        private readonly string _userRoleIdentifier;

        private SessionLayer()
        {
            _userNameIdentifier = "loggedUserName";
            _userRoleIdentifier = "loggedUserRole";
        }

        public static SessionLayer Instance
        {
            get { return _instance ?? (_instance = new SessionLayer()); }
        }

        public bool LogIn(string userName, string password, bool remember = false)
        {
            if (!ValidateUser(userName, password)) return false;

            FormsAuthentication.RedirectFromLoginPage(userName, true);
            HttpContext.Current.Session[_userNameIdentifier] = userName;
            HttpContext.Current.Session[_userRoleIdentifier] = GetUserRole(userName);

            return true;
        }

        public void LogOut(bool redirect = false)
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Remove(_userNameIdentifier);
            HttpContext.Current.Session.Remove(_userRoleIdentifier);

            if(redirect) FormsAuthentication.RedirectToLoginPage();

        }

        public string GetUserLoggedName()
        {
            var userName = HttpContext.Current.Session[_userNameIdentifier];
            return userName != null ? userName.ToString() : null;
        }

        public string GetUserLoggedRole()
        {
            var userRole = HttpContext.Current.Session[_userRoleIdentifier];
            return userRole != null ? userRole.ToString() : null;
        }

        private static bool ValidateUser(string userName, string password)
        {    
            var myUsers = UserRepo.Filter(x => x.Email.Equals(userName) && x.Password.Equals(password));
            return myUsers != null && myUsers.Count() == 1; 
        }

        private static string GetUserRole(string userName)
        {
            var users = UserRepo.Filter(x => x.Email.Equals(userName));
            if (users != null && users.Count() !=0)
                return users.First().Role.Name;
            return "";
            
        }



    }
}
