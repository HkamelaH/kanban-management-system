using System;
using System.Collections.Generic;

namespace IntroSE.kanban2024-2024-100.Backend.BusinessLayer { 

public class User {
        private string password;
        public string Email
        {
            get { return email; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("email");
                email = value;
            }
        }
        private string email;
        public User(string email, string password)
        { 
            if(email==null)
                throw new ArgumentNullException ("email");
                this.email = email;
        
        }
    }
}
