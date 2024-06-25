using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kanban.Backend.Business { 

public class User {
        private string email;
        private string password;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public user(string email, string password, int id)
        {
            this.email = email;
            this.password = password;
           
        }

        public string GetEmail() { return email; }
        public string GetPassword() { return password; }
        public boolean Login(string password)
        {
            if (password.Equals(this.password) == false)return false;
           
  
           
                return true;
        }
    }
}
