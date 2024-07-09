using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static log4net.Appender.RollingFileAppender;
using IntroSE.Kanban.Backend.BussinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.User;
using IntroSE.Kanban.Backend.BussinessLayer.Board;


namespace BackendTests
{
    internal class Porgram
    {
        static void Main(String[] args)
        {

            BoardService boardService;
            UserService userService;






            UserFacad User = new UserFacad();
            Boardfacad boardUser = new Boardfacad(User);
            userService = new UserService(User);
            boardService = new BoardService(boardUser);

            TaskTests taskTests = new TaskTests(boardService);

            TaskTests task = new TaskTests(boardService);
            BoardTests board = new BoardTests(boardService);

            UserTest user = new UserTest(userService);

            userService.Register("ahmad", "ahamd1653");
            boardService.CreateBoard("ahmad", "ahmad's board");
            boardService.AddTask("ahmad", "ahmad's board", "ass", "assignment", new DateTime(2023, 2, 28));
            user.MainTests();
            board.AddBoardTest();
            board.MainTests();
            task.MainTests();
            user.RegisterTest();
            user.LoginTest();
            user.LogoutTest();


            board.AddBoardTest();
            board.RemoveBoardTest();
            board.GetColumnTest();
            board.InProgressTest();
            board.GetColumnLimitTest();
            board.ColumnLimitTest();
            board.GetNameTest();
            board.AdvanceTaskTest();

            task.UpdateTaskTitleTest();
            task.addTaskTest();
            task.UpdateTaskDueDateTest();
            task.UpdateTaskDescription();


        }
    }
}