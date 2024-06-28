using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Forum.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.User;
using IntroSE.Kanban.Backend.BussinessLayer.Board;

namespace BackendTests
{
    internal class BoardTests
    {
        private BoardService boardService;
        //private UserService userService;
        public BoardTests(BoardService boardService)
        {

            this.boardService = boardService;
        }
        public void MainTests()
        {
            AddBoardTest();
            //ColumnLimitTest();
            //GetColumnLimitTest();
            //GetNameTest();
            //GetColumnTest();
            //RemoveBoardTest();
            //InProgressTest();
        }
        //<summary>
        // testing for addBoard
        //<summary>
        //<example> for json the function should print Board added succesfully<example>
        public void AddBoardTest()
        {
            //should be added
            string json = boardService.CreateBoard("kamle@post.bgu.ac.il", "kamle");
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);

            ////should be added
            //string json1 = boardService.AddBoard("essabsh@post.bgu.ac.il", "shushu");
            //Response response1 = JsonSerializer.Deserialize<Response>(json1);
            //Console.WriteLine(response1.ErrorMessage);

            ////should not be added
            //string json2 = boardService.AddBoard("layan@gmail.com", "layan");
            //Response response2 = JsonSerializer.Deserialize<Response>(json2);
            //Console.WriteLine(response2.ErrorMessage);

            //string json3 = boardService.AddBoard("alaa@gmail.com", "alaa");
            //Response response3 = JsonSerializer.Deserialize<Response>(json3);
            //Console.WriteLine(response2.ErrorMessage);
        }
        //<summary>
        // testing for LimitColumn
        //<summary>
        // <example> for each json the function should print column limit set succesfully<example>
        public void ColumnLimitTest()
        {
            //should be added
            string json = boardService.LimitTheColumn("kamle@post.bgu.ac.il", "kamle", 1, 100);
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);

            //should be added
            string json1 = boardService.LimitTheColumn("kamle@post.bgu.ac.il", "kamle", 2, 100);
            Response response1 = JsonSerializer.Deserialize<Response>(json1);
            Console.WriteLine(response1.ErrorMessage);

            //should be added
            string json2 = boardService.LimitTheColumn("kamle@post.bgu.ac.il", "kamle", 3, 100);
            Response response2 = JsonSerializer.Deserialize<Response>(json2);
            Console.WriteLine(response2.ErrorMessage);
        }
        //<summary>
        //testing for GetColumnLimit
        //<summary> 
        //<example> for json the column limit should be returned successfully<example>
        public void GetColumnLimitTest()
        {
            string json = boardService.GetTheColumnLimit("kamle@post.bgu.ac.il", "kamle", 0);
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);

            string json1 = boardService.GetTheColumnLimit("ahmad@gmail.com", "ahmad", 0);
            Response response1 = JsonSerializer.Deserialize<Response>(json1);
            Console.WriteLine(response1.ErrorMessage);

            string json2 = boardService.GetTheColumnLimit("ghadeer@gmail.com", "ghadeer", 0);
            Response response2 = JsonSerializer.Deserialize<Response>(json2);
            Console.WriteLine(response2.ErrorMessage);
        }
        //<summary>
        //testing for GetColumnNameTest
        //<summary>
        //<example> for json the function should return the column name successfully<example>
        public void GetNameTest()
        {
            string json = boardService.GetColumnName("kamle@post.bgu.ac.il", "kamle", 2);
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);

            string json1 = boardService.GetColumnName("ahmad@gmail.com", "ahmad", 0);
            Response response1 = JsonSerializer.Deserialize<Response>(json1);
            Console.WriteLine(response1.ErrorMessage);

            string json2 = boardService.GetColumnName("ghadeer@gmail.com", "ghadeer", 0);
            Response response2 = JsonSerializer.Deserialize<Response>(json2);
            Console.WriteLine(response2.ErrorMessage);

        }
        //<summary>
        //testing for GetColumnTest
        //<summary>
        //<example> for json the function should return the column successfully<example>
        public void GetColumnTest()
        {
            string json = boardService.GetColumn("kamle@post.bgu.ac.il", "kamle", 2);
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);
        }
        // <summary>
        // testing for RemoveBoard
        // </summary>
        // <example> for json the function should print Board removed successfully</example>
        public void RemoveBoardTest()
        {
            string json = boardService.DeleteBoard("kamle@post.bgu.ac.il", "kamle");
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);
        }

        //<summary>
        //testing for InProgressTasks
        //<summary>
        //<example> for json the function print InProgressTasks shown successfully<example>
        public void InProgressTest()
        {
            //--------------------
            string json = boardService.ProTasks("kamle@post.bgu.ac.il", "kamle");
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);
            //----------------------
            string json1 = boardService.ProTasks("kamle@post.bgu.ac.il");
            Response response2 = JsonSerializer.Deserialize<Response>(json1);
            Console.WriteLine(response2.ErrorMessage);
        }
        //<summary>
        //testing for AdvanceTask
        //<summary>
        //<example> for json the function print the task has been advanced successfully<example>

        public void AdvanceTaskTest()
        {
            string json = boardService.ChangeTaskPlace("kamle@post.bgu.ac.il", "kamle", 0, 1);
            Response response = JsonSerializer.Deserialize<Response>(json);
            Console.WriteLine(response.ErrorMessage);
        }
    }
}