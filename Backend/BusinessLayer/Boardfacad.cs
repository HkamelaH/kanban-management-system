using IntroSE.Kanban.Backend.BusinessLayer.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using IntroSE.Kanban.Backend.BussinessLayer.task;


namespace IntroSE.Kanban.Backend.BussinessLayer.Board
{

}
public class Boardfacad
{
    private Dictionary<string, List<Board>> User_Board;
 /// board   private Dictionary<string, bool> User_status; 
 /// private int CountBoards; this is for Boards
    private int CountTasks;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly UserFacad userFacad; /// Gader's implementaion
    private Dictionary<int, Task> mytasks;
  ///  private Dictionary<Task, int> Ids; this is used for the board 


    public Boardfacad(UserFacad SignedUser)
	{
        this.SignedUser = SignedUser;
        ///this.CountBoards = 0;
        this.User_Board = new Dictionary<string, List<Board>>();
        this.User_status = new Dictionary<string, bool>();
        this.CountTasks = 1;
        this.mytasks = new Dictionary<int, Task>();
        /// this.Ids = new Dictionary<Task, int>();

    }

    public void CreatBoard(string email, string BoardName)
    {
        if (string.IsNullOrWhiteSpace(BoardName)) /// check if the board name is null 
        {
            throw new Exception("not valid input");
        }
        if (!userController.IsRegestered(email))
        {
            log.Warn("trying to add a board to an non existing Email");
            throw new Exception("Email not existed!");
        }
        if (!userController.isLoggedin_user(email))
        {
            log.Warn("offline user");
            throw new Exception("User is offline!");
        }
        Console.WriteLine("am here");
        if (User_Board.ContainsKey(email))
        {
            List<Board> boards = User_Board[email];
            Console.WriteLine("am here2");
            foreach (Board b in boards)
            {
                if (b.getboardname().Equals(BoardName))
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
            Console.WriteLine("am here3");
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
    public void DeleteeBoard(string email, string boardname)
    {
        if (string.IsNullOrWhiteSpace(BoardName)) /// check if the board name is null 
        {
            throw new Exception("not valid input");
        }
        if (!userController.IsRegestered(email))
        {
            log.Warn("no such email");
            throw new Exception("no email");
        }
        if (!userController.isLoggedin_user(email))
        {
            log.Warn("offline user");
            throw new Exception("User is offline");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.getboardname().Equals(boardname))
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
        if (!userController.IsRegestered(email))
        {
            log.Warn("attempt to get a column name using a non registered email"); throw new Exception("email is not registered");
        }
        if (!userController.isLoggedin_user(email))
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
            if (board.getboardname().Equals(boardname))
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
        if (!userController.IsRegestered(email))
        {
            log.Warn("attempt to limit a column using a non registered email");
            throw new Exception($"No Such user!");

        }
        if (!userController.isLoggedin_user(email))
        {
            log.Warn("attempt to limit a column of an offline user");
            throw new Exception($"User is not logged in!");
        }
        List<Board> boards = User_Board[email];
        foreach (Board board in boards)
        {
            if (board.getboardname().Equals(boardname))
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
        if (!userController.IsRegestered(email))
        {
            log.Warn("attempt to get a column name from a non registered email");
            throw new Exception("the given email is not registered");
        }
        if (!userController.isLoggedin_user(email))
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
            if (board.getboardname().Equals(boardname))
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
        if (!userController.IsRegestered(email))
        {
            log.Warn("email not found");
            throw new Exception($"email not found");
        }
        if (!userController.isLoggedin_user(email))
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
        if (!userController.IsRegestered(email))
        {
            log.Warn("attempt to move a task to an unregistered user");
            throw new Exception("User is not registered");
        }
        if (!userController.isLoggedin_user(email))
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
            if (board.getboardname().Equals(boardname))
            {
                //Console.WriteLine(board.getCol(Ordinal).List_Of_Tasks().ToString);
                return board.getCol(Ordinal).List_Of_Tasks().ToArray();
            }
        }
        log.Warn("attempt to get a column's tasks using a non valid boardname ");
        throw new Exception($"no such boardname!");
    }

