using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
using kanban.Backend.Business;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    

    /*
     * ErrorMessage:string
     * ReturnValue:object
     * 
     * */
   public class UserService
    {
       
       

       private UserFacade userFacade;
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
        public string Register(string email, string password)
        {

            try
            {
                userFacade.Register(email, password);
                Response response = new Response();
                return JsonSerializer.Serialize(response);




            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);



            }
        }


            /// <summary>
            /// this function allows the user to log in using his email and his password 
            /// </summary>
            /// <param name="email">this is the email that the user have </param>
            /// <param name="password"this is the password the user is using ></param>
            /// <returns></returns>
            public string Login(string email, string password) { try { userFacade.Login(email, password);

                    Response response = new Response();
                    return JsonSerializer.Serialize(response);




                }
                catch (Exception ex) {
                    Response response = new Response(ex.Message);
                    return JsonSerializer.Serialize(response);
                }
            }


            /// <summary>
            /// this fun allows the user to logout
            /// </summary>
            /// <param name="email">this is the email the user is using</param>
            /// <returns></returns>
            public string Logout(string email) { try { userFacade.Logout(email);


                    Response response = new Response();
                    return JsonSerializer.Serialize(response);




                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message);
                    return JsonSerializer.Serialize(response);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>


        
    }
}

