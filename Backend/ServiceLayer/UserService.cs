using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using log4net;
using log4net.Config;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json.Serialization;
using IntroSE.Forum.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.User;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private readonly UserFacad userFacad;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UserService(UserFacad userFacad)
        {
            this.userFacad = userFacad;
            // Load configuration
        }

        // <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Register(string email, string password)
        {
            Response response;
            {
                try
                {
                    userFacad.Register(email, password);
                    response = new Response();
                    return JsonSerializer.Serialize(response);

                }
                catch (Exception e)
                {
                    response = new Response(e.Message, null);
                    return JsonSerializer.Serialize(response);
                }
            }
        }
        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>Response with user email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Login(string email, string password)
        {
            Response response;
            {
                try
                {
                    userFacad.Login(email, password);
                    response = new Response(null, email);
                    return JsonSerializer.Serialize(response);

                }
                catch (Exception e)
                {
                    response = new Response(e.Message, null);
                    return JsonSerializer.Serialize(response);
                }
            }
        }

        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Logout(string email)
        {
            Response response;
            try
            {
                userFacad.Logout(email);
                response = new Response();
                return JsonSerializer.Serialize(response);


            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }
    }


}



