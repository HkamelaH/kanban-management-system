using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.BusinessLayer.User;
using IntroSE.Kanban.Backend.BussinessLayer.Board;
using IntroSE.Kanban.Backend.ServiceLayer;
//using IntroSE.Kanban.BackendTest;

namespace BackendTests
{
    internal class Porgram
    {
        static void Main(String[] args)
        {

            BoardService boardService;
            UserService userService;


            Userfacad User = new Userfacad();
            Borardfacad boardUser = new Borardfacad(User);
            userService = new UserService(User);
            boardService = new BoardService(boardUser);

            TaskTests taskTests = new TaskTests(boardService);

            TaskTests task = new TaskTests(boardService);
            BoardTests board = new BoardTests(boardService);

            UserTests user = new UserTests(userService);
           
            // userService.Register("ahmad", "ahamd1653");
            // boardService.AddBoard("ahmad", "ahmad's board");
            //boardService.AddTask("ahmad", "ahmad's board", "ass", "assignment", new DateTime(2024, 2, 30));
            user.MainTests();
            board.AddBoardTest();
            //board.MainTests();
            task.MainTests();
            board.AdvanceTaskTest();
            //user.RegisterTest();
            //user.LoginTest();
            //user.LogoutTest();


            //board.AddBoardTest();
            //board.RemoveBoardTest();
            //board.GetColumnTest();
            //board.InProgressTest();
            //board.GetColumnLimitTest();
            //board.ColumnLimitTest();
            //board.GetNameTest();
            //board.AdvanceTaskTest();

            //task.UpdateTaskTitleTest();
            //task.addTaskTest();
            //task.UpdateTaskDueDateTest();
            //task.UpdateTaskDescription();


        }
    }
}
