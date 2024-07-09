
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.BussinessLayer.Board;

namespace IntroSE.Kanban.Backend.BusinessLayer.User

{

    public class UserFacad
    {
        public Dictionary<string, UserClass> users;
        private int counter;


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DALuserfacad daluser;
        public UserFacad()
        {

            users = new Dictionary<string, UserClass>();
            counter = 0;
            daluser = new DALuserfacad();
        }
        internal void Register(string email, string password)
        {
            if (email is null)
            {
                log.Warn("User with null email attempted register");
                throw new Exception("Email is null");
            }
            if (users.ContainsKey(email))
            {
                log.Warn("attemp to register with exsisting email");
                throw new Exception($"Email {email} already exists");
            }
            if (!checkThePassword(password))
            {
                log.Warn("attemp to register with exsisting email");
                throw new Exception($"password {password} isn't ");
            }
            UserClass newUser = new UserClass(email, password, counter);
            users.Add(email, newUser);
            users[email].login(password);
            counter = counter + 1;
            daluser.Insert(newUser.UserToDal());
            log.Info("new user is registered");
        }
        public bool isLoggedin_user(string email)
        {
            return users[email].IsLoggedIn();
        }
        public bool IsRegestered(string email)
        {
            Console.WriteLine(users.ContainsKey(email));
            return users.ContainsKey(email);
        }


        internal string Login(String email, String password)
        {
            if (email is null || password is null)
            {
                log.Warn("Attempted to login with a null email address");
                throw new Exception("Email cannot be null");
            }
            //if (!ValidateEmail(email))
            //{
            //    log.Warn("Invalid email");
            //    throw new InvalidOperationException("user email is Invalid");
            //}
            // Check if the user is registered.
            if (!IsRegestered(email))
            {
                log.Warn("Attempted to login with an unregistered email address");
                throw new Exception("This email is not registered");
            }
            if (users[email].IsLoggedIn())
            {
                log.Warn($"Login attempt for user '{email}' who is already logged in.");
                throw new InvalidOperationException($"User '{email}' is already logged in.");
            }
            if (!checkThePassword(password))
            {
                log.Warn("invalid password");
                throw new InvalidOperationException("user password is invalid");
            }
            users[email].login(password);

            log.Info($"User '{email}' logged in successfully");
            return email;
        }
        internal void Logout(string email)
        {
            if (email is null)
            {
                throw new Exception("Email is null");
            }
            if (!users.ContainsKey(email))
            {
                log.Warn("Attempted to logout a non existing user.");
                throw new ArgumentException("User doesn't exist");
            }
            if (!users[email].IsLoggedIn())
            {
                log.Warn("Attempted to logout a user that is already logged out.");
                throw new InvalidOperationException("User is already logged out.");
            }
            users[email].logout();

            log.Info("User logged out successfully");
        }
        private bool checkThePassword(string password)
        {
            bool HasUpperCase = false;
            bool HasLowerCase = false;
            bool HasNumber = false;
            if (password.Length < 6 || password.Length > 20)
            {
                log.Warn("Attempted to register using a password of invalid length");
                throw new Exception("Passwords must be between 6 and 20 characters");
            }
            foreach (char c in password)
            {
                if (char.IsDigit(c) && !HasNumber)
                {
                    HasNumber = true;
                }
                else if (char.IsUpper(c) && !HasUpperCase)
                {
                    HasUpperCase = true;
                }
                else if (char.IsLower(c) && !HasLowerCase)
                {
                    HasLowerCase = true;
                }
            }
            if (!HasNumber)
            {
                log.Warn("attempt to register using password that doesn't contain a number");
                throw new Exception("password must contain a number!");
            }
            if (!HasUpperCase)
            {
                log.Warn("Attempted to register using a password that doesn't contain an uppercase letter");
                throw new Exception("Password must contain at least one uppercase letter");
            }
            if (!HasLowerCase)
            {
                log.Warn("Attempted to register using a password that doesn't contain a lowercase letter");
                throw new Exception("Password must contain at least one lowercase letter");
            }
            return true;
        }
        /// <summary>
        /// This function loads the data needed for the User Service.
        /// <param> It has no Parametrs</param>
        /// </summary>
        /// <returns></returns>
        public void LoadTheData()
        {
            List<UserDTO> GETALLUSERS = daluser.allusers();
            foreach (UserDTO userdto in GETALLUSERS)
            {
                users.Add(userdto.Email, new user(userdto));
            }
        }
        /// <summary>
        /// This function delete all the data.
        /// <param> It has no Parametrs</param>
        /// </summary>
        /// <returns></returns>
        public void DeleteTheData()
        {
            daluser.DeleteTheData();
        }


        //private bool ValidateEmail(string email)
        //{

        //    if (string.IsNullOrWhiteSpace(email))
        //        return false;

        //    try
        //    {
        //        // Normalize the domain
        //        email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
        //                              RegexOptions.None, TimeSpan.FromMilliseconds(200));

        //        // Examines the domain part of the email and normalizes it.
        //        string DomainMapper(Match match)
        //        {
        //            // Use IdnMapping class to convert Unicode domain names.
        //            var idn = new IdnMapping();

        //            // Pull out and process domain name (throws ArgumentException on invalid)
        //            var domainName = idn.GetAscii(match.Groups[2].Value);

        //            return match.Groups[1].Value + domainName;
        //        }
        //    }
        //    catch (RegexMatchTimeoutException e)
        //    {
        //        return false;
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return false;
        //    }

        //    try
        //    {
        //        return Regex.IsMatch(email,
        //            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        //            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        //            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        //    }
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return false;
        //    }
        //}

    }
}
