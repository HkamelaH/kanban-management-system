using System;
using System.Collection.Generic;





namespace IntroSE.Kanban.Backend.ServiceLayer.Response
{
    public class Response
{
    public string ErrorMessage { get; set; }
    public string ReturnValue { get; set; }
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
    public string ToString()
    {

    }
}
}

