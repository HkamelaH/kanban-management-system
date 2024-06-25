using System;
using System.Collections.Generic;
using Backend.BusinessLayer;

namespace Backend.ServiceLayer
{
    public class BoardService
    {
        private Dictionary<string, Board> boards;
        private readonly BoardController boardController;
        //private readonly UserController userController;

        public BoardService()
        {
            boards = new Dictionary<string, Board>();
        }

        public string CreateBoard(string name)
        {
            if (boards.ContainsKey(name))
            {
                throw new ArgumentException("Board name must be unique.");
            }

            var board = new Board(name);
            boards[name] = board;
            return "Board created successfully.";
        }

        public string AddTaskToBoard(string boardName, string title, string description, DateTime dueDate)
        {
            if (!boards.ContainsKey(boardName))
            {
                throw new ArgumentException("Board not found.");
            }

            boards[boardName].AddTask(title, description, dueDate);
            return "Task added successfully.";
        }

        public string MoveTaskInBoard(string boardName, int taskId, string targetColumnName)
        {
            if (!boards.ContainsKey(boardName))
            {
                throw new ArgumentException("Board not found.");
            }

            boards[boardName].MoveTask(taskId, targetColumnName);
            return "Task moved successfully.";
        }
    }
    
    public BoardService(BoardController boardController)
    {
        this.boardController = boardController;

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
                Task[] result = boardController.GetColumn(email, boardName, columnOrdinal);
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
                boardController.CreateBoard(email, name);
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
                boardController.DeleteBoard(email, name);
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
            List<Task> inprogressTasks = boardController.ProTasks(email);
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
            string result = boardController.GetColumnName(email, boardName, columnOrdinal);
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
            int limit = boardController.GetTheColumnLimit(email, boardName, columnOrdinal);
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
            boardController.LimitTheColumn(email, boardName, columnOrdinal, limit);
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
            boardController.AddTask(email, boardName, title, description, dueDate);
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
    public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
    {
        Response response;
        try
        {
            boardController.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
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
    public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
    {
        Response response;
        try
        {
            boardController.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
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
    public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
    {
        Response response;
        try
        {
            boardController.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
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
