using System.Web;
using System.Web.Security;


namespace Mhotivo.Logic
{
    public class SessionLayer: ISessionManagement
    {

        private static SessionLayer _instance;
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
            return HttpContext.Current.Session[_userNameIdentifier].ToString();
        }

        public string GetUserLoggedRole()
        {
            return HttpContext.Current.Session[_userRoleIdentifier].ToString();
        }

        private bool ValidateUser(string userName, string password)
        {
            return true;
        }

    }
}