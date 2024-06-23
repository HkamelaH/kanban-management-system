using System;
using System.Collections.Generic; 




namespace IntroSE.Kanban.Backend.ServiceLayer
{

    class UserFacad
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();
        void Register(string email, string password )
        {
            if (_users.ContainsKey(email)) { 
            throw new Exception ($"Email {email} already exists")}
            User u =new User(email, password);
           _users.Add(email, u);
        }
    }
}

