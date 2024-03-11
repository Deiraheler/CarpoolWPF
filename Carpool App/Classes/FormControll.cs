using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpool_App.Classes
{
    internal class FormControll
    {
        //Check login form
        public static bool CheckLoginForm(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }

        //Check register form
        public static bool CheckRegisterForm(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }
    }
}
