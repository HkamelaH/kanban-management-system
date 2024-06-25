using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;




namespace kanban.Backend.Business
{

    public class UserFacad
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();
        private readonly Dictionary<string, User> loggedIn = new Dictionary<string, User>();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Register(string email, string password)
        {
            if (email is null || password is null)
            {
                log.Warn("User with null email or password attempted register");
                throw new Exception("Email or password is null");
            }
            if (_users.ContainsKey(email)) {
                log.Warn("attemp to register with exsisting email");
                throw new Exception($"Email {email} already exists")
                    }


            if (ValidPassword(password){
                log.Warn("attemp to register with exsisting email");
                throw new Exception($"password {password} isn't ");
        }

            User u = new User(email, password);




            _users.Add(email, u);
        }
        string Login(String email, String password) {
            if (email is null || password is null)
            {
                log.Warn("Attempted to login with a null email address or password");
                throw new Exception("Email and password cannot be null");
            }

            if (!IsRegestered(email))
            {
                log.Warn("Attempted to login with an unregistered email address");
                throw new Exception("The email is not registered");
            }
            if (loggedIn.ContainsKey(email))
            {
                log.Warn($"Login attempt for user '{email}' who is already logged in.");
                throw new InvalidOperationException($"User '{email}' is already logged in.");
            }
            if (!ValidPassword(password))
            {
                log.Warn("invalid password");
                throw new InvalidOperationException("user password is invalid");
            }
            
            if (loggedIn.ContainsKey(email))
            {
                throw new Exception("This email is Allready logged in");
                User LoggedInUser = new User(email, password);
                if (LoggedInUser.Login(password) {
                    loggedIn.Add(email, LoggedInUser);
                }
                else {
                    log.Warn("Attempted to login with an incorrect password.");
                    throw new ArgumentException("Incorrect password.");
                }
            }
             string Message ="User loogged in successfully"
            return Message;

        }
        string Logout(String email) {

            if (email is null)
            {
                throw new Exception("Email is null");
            }

            if (!IsRegestered(email))
            {
                log.Warn("Attempted to logout with an unregistered email address");
                throw new Exception("This email is not registered");
            }
            if (loggedIn.ContainsKey(email))
                throw new Exception("This email is Allready logged out");
                loggedIn.Remove(email);

            string Message = "User loogged out successfully"
            return Message;




        }



        public bool IsRegestered(string email)
        {
            
            return _users.ContainsKey(email);
        }




        private bool ValidPassword(string password)
        {
            bool NumberIncluded = false;
            bool UpperCaseIncluded = false;
            bool LowerCaseIncluded = false;


            if (password.Length < 6 || password.Length > 20)
            {
                log.Warn(" password has invalid length");
                throw new Exception("Password should be whithin 6 characters and 20 characters");
            }
            foreach (char c in password)
            {
                if (char.IsDigit(c) && !NumberIncluded)
                {
                    NumberIncluded = true;
                }
                else if (char.IsUpper(c) && !UpperCaseIncluded)
                {
                    UpperCaseIncluded = true;
                }
                else if (char.IsLower(c) && !LowerCaseIncluded)
                {
                    LowerCaseIncluded = true;
                }
            }
            if (!NumberIncluded)
            {
                log.Warn("attempt to register using password that doesn't have a number in it ");
                throw new Exception("password must contain a number!");
            }
            if (!UpperCaseIncluded)
            {
                log.Warn("Attempt to register using password that doesn't have an uppercase letter");
                throw new Exception("Password needs to have at least one uppercase letter!");
            }
            if (!LowerCaseIncluded)
            {
                log.Warn("Attempted to register using password that doesn't have a lowercase letter");
                throw new Exception("Password needs to have at least one lowercase letter!");
            }
            return true;
        }
    }
}

