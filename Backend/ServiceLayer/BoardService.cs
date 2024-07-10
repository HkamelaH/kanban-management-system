import org.json.JSONObject;

import java.util.List;

public class BoardService
{
    private FacadeManager facadeManager;

    public BoardService(FacadeManager facadeManager)
    {
        this.facadeManager = facadeManager;
    }

    public String createBoard(String email, String name)
    {
        try
        {
            facadeManager.createBoard(email, name);
            return new JSONObject().put("status", "success").put("message", "Board created").toString();
        }using IntroSE.Forum.Backend.ServiceLayer;
        using IntroSE.Kanban.Backend.BussinessLayer.Board;
        //using IntroSE.Kanban.Backend.BusinessLayer
        using System;
        using System.Collections.Generic;
        using System.Net.Http.Headers;
        using System.Text.Json.Serialization;
        using System.Text.Json;
        using System.Xml.Linq;
        using System.Globalization;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private readonly Boardfacad BoardUser;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public BoardService(Boardfacad BoardUser)
        {
            this.BoardUser = BoardUser;

        }
        public Boardfacad GetBoardFacad()
        {
            return this.BoardUser;
        }

        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {

            Response response;
            {
                try
                {
                    int i = 0;
                    Task[] result = BoardUser.GetTheColumn(email, boardName, columnOrdinal);
                    TaskToSend[] output = new TaskToSend[result.Length];
                    foreach (Task task in result)
                    {
                        TaskToSend newtask = new TaskToSend(task);
                        output[i] = newtask;
                        i++;
                    }
                    response = new Response(null, output);
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
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string CreateBoard(string email, string name)
        {
            Response response;
            {
                try
                {
                    BoardUser.CreateBoard(email, name);
                    response = new Response(null, null);
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
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string DeleteBoard(string email, string name)
        {
            Response response;
            {
                try
                {
                    BoardUser.DeleteBoard(email, name);
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
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string ProTasks(string email)
        {
            Response response;

            try
            {
                List<Task> inprogressTasks = BoardUser.ProTasks(email);
                Task[] tasksArray = inprogressTasks.ToArray();
                response = new Response(null, tasksArray);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string ChangeTaskPlace(string email, string boardName, int columnOrdinal, int taskId)
        {
            Response response;
            try
            {
                BoardUser.ChangeTaskPlace(email, boardName, columnOrdinal, taskId);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }

        }


        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column name value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Response response;
            try
            {
                string result = BoardUser.GetColumnName(email, boardName, columnOrdinal);
                response = new Response(result);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }



        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetTheColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response response;
            try
            {
                int limit = BoardUser.GetTheColumnLimit(email, boardName, columnOrdinal);
                response = new Response(null, limit);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }

        }


        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LimitTheColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Response response;
            try
            {
                BoardUser.LimitTheColumn(email, boardName, columnOrdinal, limit);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }



        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>

        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Response with empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Response response;
            try
            {
                BoardUser.AddTask(email, boardName, title, description, dueDate);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }


        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskDateDue(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response response;
            try
            {
                BoardUser.UpdateTaskDateDue(email, boardName, columnOrdinal, taskId, dueDate);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }

        }

        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>emoty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string ChangeTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            Response response;
            try
            {
                BoardUser.ChangeTaskTitle(email, boardName, columnOrdinal, taskId, title);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }


        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>emty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string ChangeTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            Response response;
            try
            {
                BoardUser.ChangeTaskDescription(email, boardName, columnOrdinal, taskId, description);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }

        ///// <summary>		 
        ///// the function returns the list of all user's boards.		 
        ///// </summary>		 
        ///// <param name="email">Email of the user. Must be logged in</param>		 
        ///// <returns>A response with a list of IDs of all user's boards, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string GetTheUserBoard(string email)
        {
            try
            {
                List<BoardDAO> boards = DALuserController.GetUserBoards(email);
                return new JSONObject().put("status", "success").put("boards", boards).toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }
        /// <summary>		 
        /// This method adds a user as member to an existing board.		 
        /// </summary>		 
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>		 
        /// <param name="boardID">The board's ID</param>		 
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string Join(string email, int boardID)
        {
            try
            {
                DALuserController.AddUserBoard(email, boardID);
                return new JSONObject().put("status", "success").put("message", "Joined board").toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }

        /// <summary>		 
        /// This method removes a user from the members list of a board.		 
        /// </summary>		 
        /// <param name="email">The email of the user. Must be logged in</param>		 
        /// <param name="boardID">The board's ID</param>		 
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string LeaveTheBoard(string email, int boardID)
        {
            try
            {
                userController.DeleteUserBoard(email, boardID);
                return new JSONObject().put("status", "success").put("message", "Left board").toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }



        /// <summary>		 
        /// This method assigns a task to a user		 
        /// </summary>		 
        /// <param name="email">Email of the user. Must be logged in</param>		 
        /// <param name="boardName">The name of the board</param>		 
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>		 
        /// <param name="taskID">The task to be updated identified a task ID</param>        		 
        /// <param name="emailAssignee">Email of the asignee user</param>		 
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string AssignTheTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            try
            {
                taskController.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
                return new JSONObject().put("status", "success").put("message", "Task assigned").toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }



        /// <summary>		 
        /// This method returns a board's name		 
        /// </summary>		 
        /// <param name="boardId">The board's ID</param>		 
        /// <returns>A response with the board's name, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string GetTheBoardName(int boardId)
        {
            try
            {
                String boardName = boardController.GetBoardName(boardId);
                return new JSONObject().put("status", "success").put("boardName", boardName).toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }


        }



        /// <summary>		 
        /// This method transfers a board ownership.		 
        /// </summary>		 
        /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>		 
        /// <param name="newOwnerEmail">Email of the new owner</param>		 
        /// <param name="boardName">The name of the board</param>		 
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string TransferTheOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            try
            {
                boardController.TransferOwnership(currentOwnerEmail, newOwnerEmail, boardName);
                return new JSONObject().put("status", "success").put("message", "Ownership transferred").toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }



        ///<summary>This method loads all persisted data.		 
        ///<para>		 
        ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method.		 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.		 
        ///</para>		 
        /// </summary>		 
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string LoadTheData()
        {
            try
            {
                boardController.LoadData();
                return new JSONObject().put("status", "success").put("message", "Data loaded").toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }

        ///<summary>This method deletes all persisted data.		 
        ///<para>		 
        ///<b>IMPORTANT:</b>		 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.		 
        ///</para>		 
        /// </summary>		 
        ///<returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>		 
        public string DeleteTheData()
        {
            try
            {
                boardController.DeleteData();
                return new JSONObject().put("status", "success").put("message", "Data deleted").toString();
            }
            catch (Exception e)
            {
                return new JSONObject().put("status", "error").put("message", e.getMessage()).toString();
            }

        }
























    }

}
  
