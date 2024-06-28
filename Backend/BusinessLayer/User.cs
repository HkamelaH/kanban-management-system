using log4net;
using System;
using System.Net.Http.Headers;

namespace IntroSE.Kanban.Backend.BusinessLayer.User
{
    /// <summary>
    /// User class - make new users to use Kanban board
    /// </summary>
    public class UserClass
    {
        private string emaiil;
        private string password;
        private int Id;
        private bool LoggedIn;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public UserClass(string email, string password, int id)
        {
            this.email = email;
            this.password = password;
            Id = id;
            LoggedIn = true;
        }

        public string GetEmail() { return email; }
        public string GetPassword() { return password; }
        public int GetId() { return Id; }
        public bool IsLoggedIn() { return LoggedIn; }
        public void login(string password)
        {
            if (password.Equals(this.password))
            {
                LoggedIn = true;
            }
            else
            {
                log.Warn("Attempted to login with an incorrect password.");
                throw new ArgumentException("Incorrect password.");
            }

        }
        public void logout()
        {
            this.LoggedIn = false;
        }
    }
}
