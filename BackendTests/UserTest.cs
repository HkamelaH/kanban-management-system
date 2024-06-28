using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Kanban.Backend.ServiceLayer;
using kanban.Backend.Service;


namespace BackendTests
{
    internal class UserTest
    {
        private UserService userService;

        public UserTests(UserService userService)
        {
            this.userService = userService;
        }
        public void MainTests()
        {
            RegisterTest();
            //LoginTest();
            //LogoutTest();
        }
        // <summary>
        // for testing Register .
        // </summary>
        // <example> For RegisterNumber1 the function prints registered successfully<example>
        // <example> For RegisterNumber2 the function prints email already exist<example>
        // <example> For RegisterNumber3 the function prints email is null<example>
        // <example> For RegisterNumber4 function prints password should include a number<example>
        // <example> For RegisterNumber5 the function prints password should include an upperCase letter<example>
        // <example> For RegisterNumber6 the function prints password should include a lowerCase letter<example>
        // <example> For RegisterNumber7 the function prints password should be less that 20 in length<example>
        // <example> For RegisterNumber8 the function prints password should be more that 6 in length<example>
        public void RegisterTest()
        {
            //good example
            string RegisterNumber1 = userService.Register("ghadeerd@post.bgu.ac.il", "Welovebgu3");
            Response response1 = JsonSerializer.Deserialize<Response>(RegisterNumber1);
            Console.WriteLine(response1.ErrorMessage);
            //already registered
            string RegisterNumber2 = userService.Register("ghadeerd@post.bgu.ac.il", "Welovebgu3");
            Response response2 = JsonSerializer.Deserialize<Response>(RegisterNumber2);
            Console.WriteLine(response2.ErrorMessage);
            //empty email
            string RegisterNumber3 = userService.Register("", "Nopassword1!");
            Response response3 = JsonSerializer.Deserialize<Response>(RegisterNumber3);
            Console.WriteLine(response3.ErrorMessage);
            //no numbers
            string RegisterNumber4 = userService.Register("Kamela@gmail.com", "Welovebgu");
            Response response4 = JsonSerializer.Deserialize<Response>(RegisterNumber4);
            Console.WriteLine(response4.ErrorMessage);
            ////no uppercase
            string RegisterNumber5 = userService.Register("Ahmed@post.bgu.ac.il", "welovebgu3!");
            Response response5 = JsonSerializer.Deserialize<Response>(RegisterNumber5);
            Console.WriteLine(response5.ErrorMessage);
            ////no lowercase
            string RegisterNumber6 = userService.Register("AHM@post.bgu.ac.il", "WELOVEBGU1");
            Response response6 = JsonSerializer.Deserialize<Response>(RegisterNumber6);
            Console.WriteLine(response6.ErrorMessage);
            ////pass longer than 20
            string RegisterNumber7 = userService.Register("Lona@post.bgu.ac.il", "Welovebgu3333333333333333333333333333");
            Response response7 = JsonSerializer.Deserialize<Response>(RegisterNumber7);
            Console.WriteLine(response7.ErrorMessage);
          //  pass shorter than 6
            string RegisterNumber8 = userService.Register("soso@post.bgu.ac.il", "haha");
            Response response8 = JsonSerializer.Deserialize<Response>(RegisterNumber8);
            Console.WriteLine(response8.ErrorMessage);
        }
        //<summary>
        // For testing Login
        // <summary>
        // <example> For LoginNumber1 the function should print logIn successfully<example>
        // <example> For LoginNumber2 the function should print password is wrong<example>
        // <example> For LoginNumber3 the function should print email is not registered<example>
        public void LoginTest()
        {
            //good example
            string LoginNumber1 = userService.Login("ghadeeerd@post.bgu.ac.il", "Welovebgu3");
            Response response1 = JsonSerializer.Deserialize<Response>(login1);
            Console.WriteLine(response1.ErrorMessage);
            //wrong password
            string LoginNumber2 = userService.Login("ghadeerd@post.bgu.ac.il", "wrongpass");
            Response response2 = JsonSerializer.Deserialize<Response>(login2);
            Console.WriteLine(response2.ErrorMessage);
            // The email is not registered
            string loginNumber3 = userService.Login("whynotregesteeerd@gmail.com", "Nopassword!1");
            Response response3 = JsonSerializer.Deserialize<Response>(login3);
            Console.WriteLine(response3.ErrorMessage);
        }

        //<summary>
        //For testing logout
        //<summary>
        //<example> the funvtion should print logged out succesfully 
        public void LogoutTest()
        {
            string logout = userService.Logout("ghadeeerd@post.bgu.ac.il");
            Response response = JsonSerializer.Deserialize<Response>(logout);
            Console.WriteLine(response.ErrorMessage);
        }
    }
}
