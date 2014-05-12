using System.Linq;
using System.Web;
using System.Web.Security;
using Mhotivo.App_Data.Repositories;


namespace Mhotivo.Logic
{
    public class SessionLayer: ISessionManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly string _userNameIdentifier;
        private readonly string _userRoleIdentifier;
        private readonly string _userEmailIdentifier;

        public SessionLayer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userNameIdentifier = "loggedUserName";
            _userEmailIdentifier = "loggedUserEmail";
            _userRoleIdentifier = "loggedUserRole";
        }

        public bool LogIn(string userEmail, string password, bool remember = false)
        {
            if (!ValidateUser(userEmail, password)) return false;

            HttpContext.Current.Session[_userEmailIdentifier] = userEmail;
            HttpContext.Current.Session[_userNameIdentifier] = GetUserName(userEmail,password);
            HttpContext.Current.Session[_userRoleIdentifier] = GetUserRole(userEmail);
            FormsAuthentication.RedirectFromLoginPage(userEmail, true);

            return true;
        }

        public void LogOut(bool redirect = false)
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Remove(_userNameIdentifier);
            HttpContext.Current.Session.Remove(_userRoleIdentifier);

            if(redirect) FormsAuthentication.RedirectToLoginPage();

        }

        private string GetUserName(string userEmail ,string password )
        {
            var myUsers =
                _userRepository.Filter(x => x.Email.Equals(userEmail) && x.Password.Equals(password)).FirstOrDefault();
            return myUsers.DisplayName;
        }

        public string GetUserLoggedName()
        {
            return HttpContext.Current.Session[_userNameIdentifier].ToString();
        }

        public string GetUserLoggedEmail()
        {
            return HttpContext.Current.Session[_userEmailIdentifier].ToString();
        }

        public string GetUserLoggedRole()
        {
            var userRole = HttpContext.Current.Session[_userRoleIdentifier];
            return userRole != null ? userRole.ToString() : null;
        }

        private bool ValidateUser(string userName, string password)
        {    
            var myUsers = _userRepository.Filter(x => x.Email.Equals(userName) && x.Password.Equals(password));
            return myUsers != null && myUsers.Count() == 1; 
        }

        private string GetUserRole(string userName)
        {
            var users = _userRepository.Filter(x => x.Email.Equals(userName));
            if (users != null && users.Count() !=0)
                return users.First().Role.Name;
            return "";
            
        }



    }
}
