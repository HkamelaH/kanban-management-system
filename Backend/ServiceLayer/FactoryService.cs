using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntroSE.Forum.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.User;
using IntroSE.Kanban.Backend.BussinessLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class FactoryService
    {
<<<<<<< HEAD
        private Boardfacad boardController;
        private UserFacad userController;
=======
        private Boardfacad boardfacad;
        private UserFacad userFacad;
>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        public BoardService boardService;
        private UserService userService;

        public FactoryService()
        {
<<<<<<< HEAD
            userController = new UserFacad();
            boardController = new Boardfacad(userController);
            userService = new UserService(userController);
            boardService = new BoardService(boardController);
=======
            userFacad = new UserFacad();
            boardfacad = new Boardfacad(userFacad);
            userService = new UserService(userFacad);
            boardService = new BoardService(boardfacad);
>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>An empty response, unless an error occurs </returns>
        public string Register(string email, string password)
        {
<<<<<<< HEAD
            string json;
            json = userService.Register(email, password);
            if (!checkForError(json))
            {
                boardService.GetBoardController().adduser(email);
            }
            return json;
=======
            throw new NotImplementedException();
>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }



        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response with the user's email, unless an error occurs</returns>
        public string Login(string email, string password)
        {
<<<<<<< HEAD
            string json;
            json = userService.Login(email, password);
            if (!checkForError(json))
            {
                boardService.GetBoardController().set_status(email, true);
            }
            return json;
=======
            throw new NotImplementedException();


>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string Logout(string email)
        {
<<<<<<< HEAD
            string json;
            json = userService.Logout(email);
            if (!checkForError(json))
            {
                boardService.GetBoardController().set_status(email, false);
            }
            return json;
=======
            throw new NotImplementedException();


>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            return boardService.LimitColumn(email, boardName, columnOrdinal, limit);
=======
        public string LimitTheColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            throw new NotImplementedException();


>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response with the column's limit, unless an error occurs</returns>
<<<<<<< HEAD
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            return boardService.GetTheColumnLimit(email, boardName, columnOrdinal);
=======
        public string GetTheColumnLimit(string email, string boardName, int columnOrdinal)
        {

            throw new NotImplementedException();


>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response with the column's name, unless an error occurs</returns>
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
<<<<<<< HEAD
            return boardService.GetColumnName(email, boardName, columnOrdinal);
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
<<<<<<< HEAD
            return boardService.AddTask(email, boardName, title, description, dueDate);
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method updates the due date of a task 
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            return boardService.UpdateTaskDateDue(email, boardName, columnOrdinal, taskId, dueDate);
=======
        public string UpdateTaskDateDue(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            return boardService.ChangeTaskTitle(email, boardName, columnOrdinal, taskId, title);
=======
        public string ChangeTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            return boardService.ChangeTaskDescription(email, boardName, columnOrdinal, taskId, description);
=======
        public string ChangeTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
<<<<<<< HEAD
            return boardService.ChangeTaskPlace(email, boardName, columnOrdinal, taskId);
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response with a list of the column's tasks, unless an error occurs</returns>
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
<<<<<<< HEAD
            return boardService.GetColumn(email, boardName, columnOrdinal);
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }





        /// <summary>
        /// This method deletes a board.
        /// </summary>
        /// <param name="email">Email of the user, must be logged in and an owner of the board.</param>
        /// <param name="name">The name of the board</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string RemoveBoard(string email, string name)
        {
            return boardService.DeleteBoard(email, name);
=======
        public string DeleteBoard(string email, string name)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        /// <summary>
        /// This method returns all in-progress tasks of a user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response with a list of the in-progress tasks of the user, unless an error occurs</returns>
<<<<<<< HEAD
        public string InProgressTasks(string email)
        {
            return boardService.pr(email);
=======
        public string ProTasks(string email)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A response with a list of IDs of all user's boards, unless an error occurs</returns>
<<<<<<< HEAD
        public string GetUserBoards(string email)
        {
            return boardService.GetUserBoards(email);
=======
        public string GetTheUserBoard(string email)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method returns a board's name
        /// </summary>
        /// <param name="boardId">The board's ID</param>
        /// <returns>A response with the board's name, unless an error occurs</returns>
<<<<<<< HEAD
        public string GetBoardName(int boardId)
        {
            return boardService.GetBoardName(boardId);
=======
        public string GetTheBoardName(int boardId)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method adds a user as member to an existing board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string JoinBoard(string email, int boardID)
        {
            return boardService.JoinBoard(email, boardID);
=======
        public string Join(string email, int boardID)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method removes a user from the members list of a board.
        /// </summary>
        /// <param name="email">The email of the user. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs </returns>
<<<<<<< HEAD
        public string LeaveBoard(string email, int boardID)
        {
            return boardService.LeaveBoard(email, boardID);
=======
        public string LeaveTheBoard(string email, int boardID)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified a task ID</param>        
        /// <param name="emailAssignee">Email of the asignee user</param>
        /// <returns>An empty response, unless an error occurs </returns>
<<<<<<< HEAD
        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            return boardService.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
=======
        public string AssignTheTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        ///<summary>This method loads all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method. 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string LoadData()
        {
            string json;
            json = userService.LoadData();
            if (!checkForError(json))
            {
                json = boardService.LoadData();
            }
            return json;
=======
        public string LoadTheData()
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        ///<summary>This method deletes all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        ///<returns>An empty response, unless an error occurs</returns>

<<<<<<< HEAD
        public string DeleteData()
        {
            string json;
            json = userService.DeleteData();
            if (!checkForError(json))
            {
                json = boardService.DeleteData();
                if (!checkForError(json))
                {
                    boardService = new BoardService(this.boardService.GetBoardController());
                    userService = new UserService(this.userService.GetUserController());
                }
            }
            return json;
=======
        public string DeleteTheData()
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8

        }

        /// <summary>
        /// This method transfers a board ownership.
        /// </summary>
        /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>
        /// <param name="newOwnerEmail">Email of the new owner</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>An empty response, unless an error occurs</returns>
<<<<<<< HEAD
        public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            return boardService.TransferOwnership(currentOwnerEmail, newOwnerEmail, boardName);
=======
        public string TransferTheOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }

        /// <summary>
        /// This method creates a board for the given user.
        /// </summary>
        /// <param name="email">Email of the user, must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string CreateBoard(string email, string name)
        {
<<<<<<< HEAD
            return boardService.CreateBoard(email, name);
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }



        public string ToJson(object obj)
        {
<<<<<<< HEAD
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            string json = JsonSerializer.Serialize(obj, obj.GetType(), options);
            return json;
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
        }


        public bool checkForError(string json)
        {
<<<<<<< HEAD
            Response curr;
            curr = JsonSerializer.Deserialize<Response>(json);
            return curr.CheckforError();
=======
            throw new NotImplementedException();

>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8

        }
    }
}
<<<<<<< HEAD

=======
>>>>>>> 8b34183002419accedc97059d36dedf57543fcb8
