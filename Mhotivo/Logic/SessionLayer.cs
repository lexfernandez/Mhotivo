using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using Mhotivo.App_Data;
using Mhotivo.Models;


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
            using (var ctx = new MhotivoContext())
            {
                var myUser =(from u in ctx.Users
                                 where u.Email.Equals(userName)
                                 select u);
                if (myUser.Count() != 0 && myUser.First().Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        private static string GetUserRole(string userName)
        {
            using (var ctx = new MhotivoContext())
            {
                User user = ctx.Users.Where(x => x.Email.Equals(userName)).Include(x => x.Role).FirstOrDefault();
                if(user!=null)
                    return user.Role.Name;
                return "Guest";
            }
        }



    }
}