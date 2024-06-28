using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kanban.Backend.Business;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer

{
    public class Response
    {
        public string? ErrorMessage { get; set; }
        public string? ReturnValue { get; set; }
        public Response() { }
        public Response(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public Response(string errorMessage, string returnValue)
        {
            ErrorMessage = errorMessage;
            ReturnValue = returnValue;
        }
       
    }
}

