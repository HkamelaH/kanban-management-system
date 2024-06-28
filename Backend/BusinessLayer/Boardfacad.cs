using BGU_SE_Courses.Kanban.Backend.BusinessLayer.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using BGU_SE_Courses.Kanban.Backend.BussinessLayer.task;





namespace IntroSE.Kanban.Backend.BussinessLayer.Board
{

}
public class Boardfacad
{
    private Dictionary<string, List<Board>> User_Board;
    private Dictionary<string, bool> User_status; 
    private int CountBoards;
    private int CountTasks;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly UserFacad user; 
    private Dictionary<int, Task> mytasks;
    private Dictionary<Task, int> Ids;


    public Boardfacad(UserFacad SignedUser)
	{
        this.SignedUser = SignedUser;
        this.CountBoards = 0;
        this.User_Board = new Dictionary<string, List<Board>>();
        this.User_status = new Dictionary<string, bool>();
        this.CountTasks = 1;
        this.mytasks = new Dictionary<int, Task>();
         this.Ids = new Dictionary<Task, int>();

    }

    public void CreateBoard(string email, string BoardName)
    {
        if (string.IsNullOrWhiteSpace(BoardName)) 
        {
            throw new Exception("not valid input");
        }
        if (!user.IsRegestered(email))
        {
            log.Warn("trying to add a board to an non existing Email");
            throw new Exception("Email not existed!");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("offline user");
            throw new Exception("User is offline!");
        }
       
        if (User_Board.ContainsKey(email))
        {
            List<Board> boards = User_Board[email];
           
            foreach (Board b in boards)
            {
                if (b.GetBoardName().Equals(BoardName))
                {
                    log.Warn("the board name taken for this user");
                    throw new Exception("The input name for board already taken!");
                }
            }
            Board board1 = new Board(email, BoardName, CountBoards);
            User_Board[email].Add(board1);
            CountBoards += 1;
        }
        else
        {
            log.Info("added a new board");
            Board board2 = new Board(email, BoardName, CountBoards);
            List<Board> boardss = new List<Board>();
            boardss.Add(board2);
            User_Board.Add(email, boardss);
            CountBoards = CountBoards + 1;
        }
    }

    /// <summary>
    /// This function removes a board
    /// </summary>
    /// <param name="email" > users email that we will add the Board to</param>
    /// <param name="boardname"> board name that we want to remove</param>
    /// <exception cref="Exception">if the email is invalid </exception>
    /// <exception cref="Exception">if the user not online </exception>
    /// <exception cref="Exception">if the board does not exsist</exception>
    /// <return> this function does not return anything</return>
    public void DeleteBoard(string email, string boardname)
    {
        if (string.IsNullOrWhiteSpace(BoardName)) /// check if the board name is null 
        {
            throw new Exception("not valid input");
        }
        if (!user.IsRegestered(email))
        {
            log.Warn("no such email");
            throw new Exception("no email");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("offline user");
            throw new Exception("User is offline");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.GetBoardName().Equals(boardname))
            {
                log.Info("board removed");
                boards.Remove(board);
                User_Board[email].Remove(board);
                return;
            }
        }
        log.Warn("trying to remove a board that is not existed");
        throw new Exception("boardname is not existed");
    }

    public string GetColumnName(string email, string boardname, int columnOrdinal)
    {
        check_Status(columnOrdinal);
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to get a column name using a non registered email"); throw new Exception("email is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to get column with an offline user");
            throw new Exception("offline user");
        }
        if (!User_Board.ContainsKey(email))
        {
            log.Warn("no boards");
            throw new Exception("no boards");
        }
        if (boardname is null)
        {
            log.Warn("invalid boardname");
            throw new Exception("invalid boardname");
        }

        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.GetBoardName().Equals(boardname))
            {
                return board.GetColumnName(columnOrdinal);
            }
        }
        log.Warn("board not existed for this user");
        throw new Exception("no such boardname");

    }



    /// <summary>
    /// change the limit of the board to a new limit 
    /// </summary>
    /// <param name="email">the user email</param>
    /// <param name="boardname">the board name </param>
    /// <param name="Ordinal">the column status that we work on </param>
    /// <param name="limit">the new limit </param>
    /// <exception cref="Exception">if the email is invalid </exception>
    /// <exception cref="Exception">if the user is offline</exception>
    /// <exception cref="Exception">if the boardname does not exsist </exception>
    /// <exception cref="Exception">if the email is not registered</exception>
    /// <return> this function does not return anything</return>
    public void LimitTheColumn(string email, string boardname, int Ordinal, int limit)
    {
        check_Status(Ordinal);
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to limit a column using a non registered email");
            throw new Exception($"No Such user!");

        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to limit a column of an offline user");
            throw new Exception($"User is not logged in!");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.GetBoardName().Equals(boardname))
            {
                board.setlimit(Ordinal, limit);
                return;
            }
        }
        log.Warn("attempt to limit a column using a non valid boardname");
        throw new Exception($"No such boardname!");
    }
    /// <summary>
    /// return the limit of the column 
    /// </summary>
    /// <param name="email">the email of the user </param>
    /// <param name="boardname">board name </param>
    /// <param name="columnstatus">the column status </param>
    /// <exception cref="Exception">if the column has no limit</exception>
    /// <exception cref="Exception">if the email is not valid</exception>
    /// <exception cref="Exception">if the user is offline</exception>
    /// <exception cref="Exception">if the email is not registered</exception>
    /// <returns> int with the column limit</returns>
    public int GetTheColumnLimit(string email, string boardname, int columnstatus)
    {
        int limit = 0;
        check_Status(columnstatus);
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to get a column name from a non registered email");
            throw new Exception("the given email is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to get a column limit from an offline user");
            throw new Exception($"User is not logged in!");
        }
        if (User_Board[email].Count <= 0)
        {
            log.Warn("attempt to get a column limit using a non valid boardname");
            throw new Exception("No Such Boardname");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.GetBoardName().Equals(boardname))
            {
                limit = board.getlimit(columnstatus);
                return limit;
            }

        }
        log.Warn("board not existed");
        throw new Exception("no such board");
    }




    /// <summary>
    /// this function return a list of all the inprogress tasks 
    /// <param name="email" >the email of the user </param>
    /// </summary>
    /// <exception cref="Exception">if the email is not valid</exception>
    /// <exception cref="Exception">if the user is not online </exception>
    /// <exception cref="Exception">if the email is not registered</exception>
    /// <returns>List of all in progress task to the user</returns>
    public List<Task> ProTasks(string email)
    {
        if (!user.IsRegestered(email))
        {
            log.Warn("email not found");
            throw new Exception($"email not found");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to list inprogress task from an offline user");
            throw new Exception($"User is not logged in!");
        }
        List<Task> res = new List<Task>();
        if (User_Board[email].Count < 1)
        {
            log.Warn("no board");
            throw new Exception("no board");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            List<Task> InProgtemp = board.In_progressTasks();
            foreach (Task task in InProgtemp)
            {
                res.Add(task);
            }
        }
        return res;
    }

    /// <summary>
    /// return list of task in specific column in the board 
    /// </summary>
    /// <param name="email">the email of the user</param>
    /// <param name="boardname">name of the board </param>
    /// <param name="columnstatus">the column status </param>
    /// <exception cref="Exception">if the email is not valid</exception>
    /// <exception cref="Exception">if the user is offline</exception>
    /// <exception cref="Exception">if the email is not registered</exception>
    /// <exception cref="Exception">if the board does not exsist</exception>
    /// <returns>List of task in the column</returns>

    public Task[] GetTheColumn(string email, string boardname, int Ordinal)
    {
        check_Status(Ordinal);
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to move a task to an unregistered user");
            throw new Exception("User is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to move a task to an offline user");
            throw new Exception("User is not logged in!");
        }
        if (boardname is null)
        {
            log.Warn("invalid board name");
            throw new Exception("");
        }
        if (User_Board[email].Count <= 0)
        {
            log.Warn("user has no boards");
            throw new Exception("user has no boards");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.GetBoardName().Equals(boardname))
            {
                //Console.WriteLine(board.getCol(Ordinal).List_Of_Tasks().ToString);
                return board.getCol(Ordinal).List_Of_Tasks().ToArray();
            }
        }
        log.Warn("attempt to get a column's tasks using a non valid boardname ");
        throw new Exception($"no such boardname!");
    }
    public List<Task> PlayingTasks(string email)
    {
        if (!SignedUser.IsRegistered(email))
        {
            log.Warn("Email is not found");
            throw new Exception("Email is not found");
        }

        // Check if the user is logged in
        if (!loggedIn.ContainsKey(SingedUser.email)) 
        {
            log.Warn("Attempt to list InProgress tasks from offline user");
            throw new Exception("User is not logged in!");
        }
        List<Task> Tmp = new List<Task>();
        if (User_Board[email].Count < 1)
        {
            log.Warn("No board found for the user");
            throw new Exception("No board found");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            List<Task> InProgtemp = board.In_progressTasks();
            foreach (Task task in InProgtemp)
            {
                Tmp.Add(task);
            }
        }
        return Tmp;
    }

    public void AddTask(string email, string boardname, string Title, string description, DateTime duedate)
    {
        if (!user.IsRegestered(email))
        {
            log.Warn("not registered email");
            throw new Exception("User is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to add a task to an offline user");
            throw new Exception("User is not logged in!");
        }
        if (!User_Board.ContainsKey(email))
        {
            log.Warn("the user has no boards");
            throw new Exception("the user has no boards");
        }
        if (!Check_Description(description))
        {
            log.Warn("invalid description");
            throw new Exception("invalid description");
        }
        if (!Check_Title(Title))
        {
            log.Warn("invalid Title");
            throw new Exception("invalid Title");
        }
        if (!Check_Date(duedate))
        {
            log.Warn("invalid duedate");
            throw new Exception("invalid duedate");
        }
        if (User_Board.ContainsKey(email))
        {
            List<Board> boards = User_Board[email];
            foreach (Board board in boards)
            {
                if (board.GetBoardName().Equals(boardname))
                {
                    Column column = board.getCol(0);
                    column.AddTask(email, Title, description, duedate, CountTasks);
                    Task newtask = new Task(CountTasks, duedate, Title, description, email, boardname);
                    mytasks.Add(CountTasks, newtask);
                    Ids.Add(newtask, CountTasks);
                    CountTasks = CountTasks + 1;
                    log.Info("Task added to the board successfully");
                    return;
                }
            }
            log.Warn("Board name is not in the dictionary");
            throw new Exception("Board does not exsist");
        }
        log.Warn("the user is not registered");
        throw new Exception("user not Registered");
    }

    public void ChangeTaskPlace(string email, string boardname, int Ordinal, int taskid)
    {
        check_Status(Ordinal); 
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to move a task to an unregistered user");
            throw new Exception("User is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to move a task to an offline user");
            throw new Exception("User is not logged in!");
        }
        if (boardname is null)
        {
            log.Warn("Invalid Boardname");
            throw new Exception("Invalid Boardname");
        }
        if (Ordinal == 2)
        {
            log.Warn("Can't advance from done");
            throw new Exception("Can't advance");
        }
        if (!User_Board.ContainsKey(email))
        {
            log.Warn("No Boards");
            throw new Exception("No Boards");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.GetBoardName().Equals(boardname))
            {
                Column column = board.getCol(Ordinal);
                List<Task> tasks = column.x();
                foreach (Task task in tasks)
                {
                    if (task.getid() == taskid)
                    {
                        Column col = getcolumn(boardname, Ordinal);
                        if (Ordinal == 0) col = board.getCol(1);
                        if (Ordinal == 1) col = board.getCol(2);
                        List<Task> thenextlevel = col.List_Of_Tasks();
                        if (thenextlevel.Count == col.getlim())
                        {
                            log.Warn("Maximum tasks");
                            throw new Exception("Maximum tasks");
                        }
                        else
                        {

                            board.getCol(Ordinal).List_Of_Tasks().Remove(task);

                            board.getCol(Ordinal + 1).List_Of_Tasks().Add(task);
                            log.Info("Task advanced successfully");
                            return;
                        }
                    }
                }
                log.Warn("TaskID does not existe");
                throw new Exception("No taskId");
            }
        }
        log.Warn("Board name is not found");
        throw new Exception("No similar board name");
    }
    public void ChangeTaskTitle(string email, string boardname, int Ordinal, int taskid, string newTitle)
    {
        check_Status(Ordinal);
        if (user.IsRegestered(email))
        {
            log.Warn("attempt to move a task to an unregistered user");
            throw new Exception("User is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to move a task to an offline user");
            throw new Exception("User is not logged in!");
        }
        if (Check_Title(newTitle))
        {
            if (User_Board.ContainsKey(email))
            {
                
                List<Board> boards = User_Board[email];
                foreach (Board board in boards)
                {
                    if (board.GetBoardName().Equals(boardname))
                    {
                        Column column = board.getCol(Ordinal);
                        List<Task> tasks = column.List_Of_Tasks();
                        foreach (Task task in tasks)
                        {
                            if (task.getid() == (taskid))
                            {
                                if (Ordinal != 2)
                                {
                                    task.setTitle(newTitle);
                                    log.Info("The title updated succesfully");
                                    return;
                                }
                                else
                                {
                                    log.Warn("Should not be changed");
                                    throw new Exception("Task already done");
                                }
                            }
                        }
                        log.Warn("Task id does not existed");
                        throw new Exception("Task does not existed");
                    }
                }
                log.Warn("Board name is not ");
                throw new Exception("Board does not exsist");
            }
            log.Warn("The user is not registered");
            throw new Exception("User is not Registered");
        }
    }

    public void UpdateTaskDescription(string email, string boardname, int Ordinal, int taskid, string newDescription)
    {
        check_Status(Ordinal);
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to move a task to an unregistered user");
            throw new Exception("User is not registered");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to move a task to an offline user");
            throw new Exception("User is not logged in!");
        }
        if (Check_Description(newDescription))
        {
            if (User_Board.ContainsKey(email))
            {
                List<Board> boards = User_Board[email];
                foreach (Board board in boards)
                {
                    if (board.GetBoardName().Equals(boardname))
                    {
                        Column column = board.getCol(Ordinal);
                        List<Task> tasks = column.List_Of_Tasks();
                        foreach (Task task in tasks)
                        {
                            if (task.getid() == (taskid))
                            {
                                if (Ordinal != 2)
                                {
                                    task.setdescription(newDescription);
                                    log.Info("task title description successfully");
                                    return;
                                }
                                else
                                {
                                    log.Warn("task is done should not be changed");
                                    throw new Exception("cant change a task that is done");
                                }
                            }
                        }
                        log.Warn("task id does not exsist");
                        throw new Exception("task does not exsist");
                    }
                }
                log.Warn("Board name is not in the dictionary");
                throw new Exception("Board does not exsist");
            }
            log.Warn("the user is not registered");
            throw new Exception("user not Registered");
        }
    }
    public void UpdateTaskDateDue(string email, string boardname, int Ordinal, int taskid, DateTime newDate)
    {
        check_Status(Ordinal);
        if (!user.IsRegestered(email))
        {
            log.Warn("attempt to update a task dueDate to an unexisted email");
            throw new Exception("email is not found");
        }
        if (!loggedIn.ContainsKey(user.email))
        {
            log.Warn("attempt to update a task dueDate to an offline user");
            throw new Exception("User is not logged in!");
        }
        if (Check_Date(newDate))
        {
            if (User_Board.ContainsKey(email))
            {
                List<Board> boards = User_Board[email];
                foreach (Board board in boards)
                {
                    if (board.GetBoardName().Equals(boardname))
                    {
                        Column column = board.getCol(Ordinal);
                        List<Task> tasks = column.List_Of_Tasks();
                        foreach (Task task in tasks)
                        {
                            if (task.getid() == (taskid))
                            {
                                if (Ordinal != 2)
                                {
                                    task.setDuedate(newDate);
                                    log.Info("task dueDate updated successfully");
                                    return;
                                }
                                else
                                {
                                    log.Warn("Task is done should not be changed");
                                    throw new Exception("cant change a task that is done");
                                }
                            }
                        }
                        log.Warn("Task id does not exsist");
                        throw new Exception("Task does not exsist");
                    }
                }
                log.Warn("Board name is not in the dictionary");
                throw new Exception("Board does not exsist");
            }
            log.Warn("The user is not registered");
            throw new Exception("User is  not Registered");
        }
    }

    private void check_Status(int columnOrdinal)
    {
        if (columnOrdinal < 0 || columnOrdinal > 2)
        {
            throw new Exception("Invalid column ordinal!");
        }
    }


    public bool Check_Date(DateTime date)
    {
        if(date == null)
        {
            log.Warn.("the date is null");
            throw new Exception("There is no date as null");
        }
        if(DateTime.Now > date)
        {

            log.Warn("the date time shouldnt be expired ");
                throw new Exception("There is no expired date");
        }
        return true;
    }
