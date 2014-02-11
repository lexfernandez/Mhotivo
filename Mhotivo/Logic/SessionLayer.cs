using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;

using WebMatrix.WebData;
using Mhotivo.Filters;
using Mhotivo.Models;

namespace Mhotivo.Logic
{
    public class SessionLayer
    {
        public static bool LogIn(string email, string password, bool remember){
            return WebSecurity.Login(email, password, persistCookie: remember);
        }

        public static bool Validate(string email, string password, string confirmPassword)
        {
            if (password.Equals(confirmPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool LogIn(string email, string password)
        {
            return LogIn(email, password, false);
        }

        public static void LogOff()
        {
            WebSecurity.Logout();
        }



        
    
    }
}