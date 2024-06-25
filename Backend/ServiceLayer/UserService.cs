using System;
using System;
using System.Collection.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IntroSE.Kanban.Backend.ServiceLayer.Response;







    /*
     * ErrorMessage:string
     * ReturnValue:object
     * }
     * */
    class UserService
    {
   
    
    
        UserFacade userFacade;
        public UserService()
        {
            this.userFacade = new UserFacade();
        
        }

        /// <summary>
        /// the function registers a new user .
        /// </summary>
        /// <param name="email">the email the user is using </param>
        /// <param name="password">the password the user is using </param>
        /// <returns></returns>
        public string Register(string email, string password) { try { userFacade.Register(email, password); }
            catch {
            Response response = new Response(Exception.Message);
            
            



            return $"ErrorMessage:\"{Exception.Message}\",RetuenValue:\"}}";

            }


            /// <summary>
            /// this function allows the user to log in using his email and his password 
            /// </summary>
            /// <param name="email">this is the email that the user have </param>
            /// <param name="password"this is the password the user is using ></param>
            /// <returns></returns>
            public string Login(string email, string password) { userFacade.Login(email, password); }


            /// <summary>
            /// this fun allows the user to logout
            /// </summary>
            /// <param name="email">this is the email the user is using</param>
            /// <returns></returns>
            public string Logout(string email) { userFacade.Logout(email); }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>


        }
    }

