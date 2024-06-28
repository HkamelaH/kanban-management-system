using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kanban.Backend.Business;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace BGU_SE_Courses.Kanban.Backend.ServiceLayer

{
    public class Response
    {
        public string? ErrorMessage { get; set; }
        public string? ReturnValue { get; set; }
        public Response() { }
        public Response(object errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public Response(object errorMessage, object returnValue)
        {
            ErrorMessage = errorMessage;
            ReturnValue = returnValue;
        }
       
    }
}

