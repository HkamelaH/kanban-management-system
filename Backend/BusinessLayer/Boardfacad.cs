using IntroSE.Kanban.Backend.BusinessLayer.User;
using IntroSE.Kanban.Backend.DataAccessLayer.DAL;
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
    public class Boardfacad
    {
        private Dictionary<string, List<BoardClass>> User_Board;
        private Dictionary<string, bool> User_status;
        private int CountBoards;
        private int CountTasks;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserFacad user;
        private Dictionary<int, Task> mytasks;
        private Dictionary<Task, int> Ids;
        private Dictionary<int, BoardClass> ListOfBoards;
        private Dictionary<string, int> listofids;
        private readonly DALtaskcontroller daltaskcontroller;
        private readonly DALcolumncontroller dalcolumncontroller;


        public Boardfacad(UserFacad SignedUser)
        {
            this.user = SignedUser;
            this.CountBoards = 0;
            this.User_Board = new Dictionary<string, List<BoardClass>>();
            this.User_status = new Dictionary<string, bool>();
            this.CountTasks = 1;
            this.mytasks = new Dictionary<int, Task>();
            this.Ids = new Dictionary<Task, int>();

        }
        public Dictionary<string, bool> getUserStatus() { return this.User_status; }
        /// <summary>
        /// this function add board 
        /// </summary>
        /// <param name="email" > This is the users email that we will add the Board to </param>
        /// <param name="boardname"> name of the new board </param>
        /// <exception cref="Exception">if the email is invalid </exception>
        /// <exception cref="Exception">if the board name is null or empty</exception>
        /// <exception cref="Exception">if the user not online </exception>
        /// <exception cref="Exception">if the boardname exsist already </exception>
        /// <exception cref="Exception">if the email is not registered</exception>
        public void CreateBoard(string email, string BoardName)
        {
            Console.WriteLine("users size");
            Console.WriteLine(userController.users.Count);
            if (string.IsNullOrWhiteSpace(BoardName))
            {
                throw new Exception("not valid input");
            }
            if (!user.IsRegestered(email))
            {
                log.Warn("trying to add a board to an non existing Email");
                throw new Exception("Email not existed!");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("offline user");
                throw new Exception("User is offline!");
            }
            if (!userController.users.ContainsKey(email))
            {
                log.Warn("trying to add a board to an non existing Email");
                throw new Exception("Email not existed!");
            }

            if (User_Board.ContainsKey(email))
            {
                List<BoardClass> boards = User_Board[email];

                foreach (BoardClass b in boards)
                {
                    if (b.getboardname().Equals(BoardName))
                    {
                        log.Warn("the board name taken for this user");
                        throw new Exception("The input name for board already taken!");
                    }
                }
                BoardClass board1 = new BoardClass(email, BoardName, CountBoards);
                User_Board[email].Add(board1);
                CountBoards += 1;
            }
            else
            {

                log.Info("added a new board");
                BoardClass board2 = new BoardClass(email, BoardName, CountBoards);
                List<BoardClass> boardss = new List<BoardClass>();
                boardss.Add(board2);
                User_Board.Add(email, boardss);
                CountBoards += 1;
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
            if (!user.IsRegestered(email))
            {
                log.Warn("no such email");
                throw new Exception("no email");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("offline user");
                throw new Exception("User is offline");
            }
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
            {
                if (board.owner.Equals(userController.users[email]))
                {
                    if (board.getboardname().Equals(boardname))
                    {
                        log.Info("board removed");
                        //boards.Remove(board);
                        board.deleteAllTasks(email);
                        //board.columns_list = new List<Column>();
                        boards.Remove(board);
                        User_Board[email].Remove(board);
                        ListOfBoards.Remove(board.getboardid());
                        this.listofids.Remove(boardname);
                        return;
                    }
                }
                log.Warn("trying to remove a board while he is not the owner");
                throw new Exception("not the owner");
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
            if (!user.isLoggedin_user(email))
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

            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
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
            if (!user.IsRegestered(email))
            {
                log.Warn("attempt to limit a column using a non registered email");
                throw new Exception($"No Such user!");

            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("attempt to limit a column of an offline user");
                throw new Exception($"User is not logged in!");
            }
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
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
            if (!user.IsRegestered(email))
            {
                log.Warn("attempt to get a column name from a non registered email");
                throw new Exception("the given email is not registered");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("attempt to get a column limit from an offline user");
                throw new Exception($"User is not logged in!");
            }
            if (User_Board[email].Count <= 0)
            {
                log.Warn("attempt to get a column limit using a non valid boardname");
                throw new Exception("No Such Boardname");
            }
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
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
            List<Task> InProgress = new List<Task>();
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
            {
                List<Task> InProgtemp = board.In_progressTasks();
                foreach (Task task in InProgtemp)
                {
                    if (task.GetAssignee().Equals(email))
                    {
                        InProgress.Add(task);
                    }
                }
            }
            return InProgress;
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
            if (!user.isLoggedin_user(email))
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
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
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
        /// <summary>
        /// add new task to a column in the board . 
        /// <param name="email" > user email </param>
        /// <param name="Title">title of the task </param>
        /// <param name="boardname"> board name </param>
        /// <param name="decription"> the dicription of the task </param>
        /// <param name="duedate"> this is the due date for the task</param>
        /// </summary>
        /// <exception cref="Exception">if the email is invalid</exception>
        /// <exception cref="Exception">if the user is offline</exception>
        /// <exception cref="Exception">if the board does not exsist</exception>
        /// <exception cref="Exception>">if the user is not registered</exception>
        /// <returns>the function does not return anything</returns>
        public void AddTask(string email, string boardname, string Title, string description, DateTime duedate)
        {
            if (!user.IsRegestered(email))
            {
                log.Warn("not registered email");
                throw new Exception("User is not registered");
            }
            if (!user.isLoggedin_user(email))
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
            if (!User_Board.ContainsKey(email))
            {
                log.Warn("the user is not registered");
                throw new Exception("user not Registered");
            }
            BoardClass board = GetBoard(email, boardname);
            Column column = board.getCol(0);
            column.AddTask(email, Title, description, duedate, listofids[boardname]);
            CountTasks += 1;
            board.ToDal().Save();
            log.Info("Task added to the board successfully");
        }

        /// <summary>
        /// This function move a task . 
        /// <param name="email" > email of the user </param>
        /// <param name="boardname"> the board name </param>
        /// <param name="columnstatus"> the column status .</param>
        /// <param name="taskid">task id </param>
        /// </summary>
        /// <exception cref="Exception">if the email is invalid</exception>
        /// <exception cref="Exception">if the user is offline</exception>
        /// <exception cref="Exception">if the board does not exsist</exception>
        /// <exception cref="Exception">if the column reach its limit</exception>
        /// <exception cref="Exception">if task id does not exsist</exception>
        /// <exception cref="Exception">if the task is done</exception>
        /// <exception cref="Exception>">if the user is not registered</exception>
        /// <return>the function does not return anything</return>
        public void ChangeTaskPlace(string email, string boardname, int Ordinal, int taskid)
        {
            check_Status(Ordinal);
            if (!user.IsRegestered(email))
            {
                log.Warn("attempt to move a task to an unregistered user");
                throw new Exception("User is not registered");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("attempt to move a task to an offline user");
                throw new Exception("User is not logged in!");
            }
            if (boardname is null)
            {
                log.Warn("invalid boardname");
                throw new Exception("invalid boardname");
            }
            if (Ordinal == 2)
            {
                log.Warn("can't advance from done");
                throw new Exception("can't advance");
            }
            if (!User_Board.ContainsKey(email))
            {
                log.Warn("no boards");
                throw new Exception("no boards");
            }
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
            {
                if (board.getboardname().Equals(boardname))
                {
                    Column column = board.getCol(Ordinal);
                    List<Task> tasks = column.List_Of_Tasks();
                    foreach (Task task in tasks)
                    {
                        if (task.getid() == taskid)
                        {

                            Column col = board.getCol(Ordinal + 1);
                            List<Task> thenextlevel = col.List_Of_Tasks();
                            if (thenextlevel.Count == col.getlim())
                            {
                                log.Warn("maximum tasks");
                                throw new Exception("maximum tasks");
                            }
                            else
                            {

                                board.getCol(Ordinal).List_Of_Tasks().Remove(task);
                                board.getCol(Ordinal + 1).List_Of_Tasks().Add(task);
                                log.Info("task advanced successfully");
                                return;
                            }
                        }
                    }
                    log.Warn("taskid is not existed");
                    throw new Exception("no taskId");
                }
            }
            log.Warn("board name is not found");
            throw new Exception("no similar board name");
        }


        /// <summary>
        /// change the task title 
        /// <param name="email" > email address </param>
        /// <param name="boardname"> the board name </param>
        /// <param name="taskid">the task id that we wont to change </param>
        /// <param name="columnstatus"> the column status that include the task </param>
        /// <param name="newTitle">the new task tiltle </param>
        /// </summary>
        /// <exception cref="Exception">if the email is invalid</exception>
        /// <exception cref="Exception">if the user is offline</exception>
        /// <exception cref="Exception">if the board does not exsist</exception>
        /// <exception cref="Exception">if task id does not exsist</exception>
        /// <exception cref="Exception">if the task is done</exception>
        /// <exception cref="Exception>">if the user is not registered</exception>
        /// <return>the function does not return anything</return>
        public void ChangeTaskTitle(string email, string boardname, int Ordinal, int taskid, string newTitle)
        {
            check_Status(Ordinal);
            if (!user.IsRegestered(email))
            {
                log.Warn("attempt to move a task to an unregistered user");
                throw new Exception("User is not registered");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("attempt to move a task to an offline user");
                throw new Exception("User is not logged in!");
            }
            if (Check_Title(newTitle))
            {
                if (User_Board.ContainsKey(email))
                {
                    List<BoardClass> boards = User_Board[email];
                    foreach (BoardClass board in boards)
                    {
                        if (board.getboardname().Equals(boardname))
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
                                        log.Info("the title updated succesfully");
                                        return;
                                    }
                                    else
                                    {
                                        log.Warn("should not be changed");
                                        throw new Exception("task already done");
                                    }
                                }
                            }
                            log.Warn("task id is not existed");
                            throw new Exception("task is not existed");
                        }
                    }
                    log.Warn("board name is not ");
                    throw new Exception("Board does not exsist");
                }
                log.Warn("the user is not registered");
                throw new Exception("user not Registered");
            }
        }

        /// <summary>
        /// change the description of the task 
        /// <param name="email" > email address</param>
        /// <param name="boardname"> board name </param>
        /// <param name="newDescription"> new description </param>
        /// <param name="columnstatus"> the column status .</param>
        /// <param name="taskid">task id that we work on </param>
        /// </summary>
        /// <exception cref="Exception">if the email is invalid</exception>
        /// <exception cref="Exception">if the user is offline</exception>
        /// <exception cref="Exception">if the board does not exsist</exception>
        /// <exception cref="Exception">if task id does not exsist</exception>
        /// <exception cref="Exception">if the task is done</exception>
        /// <exception cref="Exception>">if the user is not registered</exception>
        /// <return>the function does not return anything</return>
        public void ChangeTaskDescription(string email, string boardname, int Ordinal, int taskid, string newDescription)
        {
            check_Status(Ordinal);
            if (!user.IsRegestered(email))
            {
                log.Warn("attempt to move a task to an unregistered user");
                throw new Exception("User is not registered");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("attempt to move a task to an offline user");
                throw new Exception("User is not logged in!");
            }
            if (Check_Description(newDescription))
            {
                if (User_Board.ContainsKey(email))
                {
                    List<BoardClass> boards = User_Board[email];
                    foreach (BoardClass board in boards)
                    {
                        if (board.getboardname().Equals(boardname))
                        {
                            Column column = board.getCol(Ordinal);
                            List<Task> tasks = column.List_Of_Tasks();
                            foreach (Task task in tasks)
                            {
                                if (task.GetAssignee().Equals(email))
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
                                log.Warn("not the assignee");
                                throw new Exception("not the assignee");
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

        /// <summary>
        /// change the date of the task . 
        /// <param name="email" > the email address</param>
        /// <param name="boardname"> the board name </param>
        /// <param name="newDate"> new date </param>
        /// <param name="colomnstatus"> the status of the column that we work on .</param>
        /// <param name="taskid">task id that we work on </param>
        /// </summary>
        /// <exception cref="Exception">if the email is invalid</exception>
        /// <exception cref="Exception">if the user is offline</exception>
        /// <exception cref="Exception">if the board does not exsist</exception>
        /// <exception cref="Exception">if task id does not exsist</exception>
        /// <exception cref="Exception">if the task is done</exception>
        /// <exception cref="Exception>">if the user is not registered</exception>
        /// <return>the function does not return anything</return> 
        public void UpdateTaskDateDue(string email, string boardname, int Ordinal, int taskid, DateTime newDate)
        {
            check_Status(Ordinal);
            if (!user.IsRegestered(email))
            {
                log.Warn("attempt to update a task duoDate to an unexisted email");
                throw new Exception("email is not found");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("attempt to update a task duoDate to an offline user");
                throw new Exception("User is not logged in!");
            }
            if (Check_Date(newDate))
            {
                if (User_Board.ContainsKey(email))
                {
                    List<BoardClass> boards = User_Board[email];
                    foreach (BoardClass board in boards)
                    {
                        if (board.getboardname().Equals(boardname))
                        {
                            Column column = board.getCol(Ordinal);
                            List<Task> tasks = column.List_Of_Tasks();
                            foreach (Task task in tasks)
                            {
                                if (task.GetAssignee().Equals(email))
                                {
                                    if (task.getid() == (taskid))
                                    {
                                        if (Ordinal != 2)
                                        {
                                            task.setDuedate(newDate);
                                            log.Info("task duoDate updated successfully");
                                            return;
                                        }
                                        else
                                        {
                                            log.Warn("task is done should not be changed");
                                            throw new Exception("cant change a task that is done");
                                        }
                                    }
                                }
                                log.Warn("not the assignee");
                                throw new Exception("not the assignee");
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

        /// <summary>
        /// the function return true if the email is leggel else return false 
        /// </summary>
        /// <param name="title">the title we have to check</param>
        /// <exception cref="Exception">if null</exception>
        /// <exception cref="Exception"> if empty</exception>
        /// <exception cref="Exception">if more than 50 in length</exception>
        /// <returns> true if the title is legall</returns>
        public bool Check_Title(string title)
        {
            if (title == null)
            {
                log.Warn("the title is null");
                throw new Exception("the title is null");
            }
            else if (title.Length == 0)
            {
                log.Warn("the title is empty ");
                throw new Exception("the title is empty ");
            }
            else if (title.Length > 50)
            {
                log.Warn("the titel cant be longer than 50 ");
                throw new Exception("the title length longer than 50 ");
            }
            else
                return true;
        }

        /// <summary>
        /// this function return true if the description is leggal else return false 
        /// </summary>
        /// <param name="desc">the decsription we have to check</param>
        /// <exception cref="Exception">if the decsription is null</exception>
        /// <exception cref="Exception">if the decsriprion is longer then 300 </exception>
        /// /// <returns>true if the decsription is legall</returns>
        public bool Check_Description(string desc)
        {
            if (desc == null)
            {
                log.Warn("description is null");
                throw new Exception(" the description cant be null ");
            }
            else
            {
                if (desc.Length > 300)
                {
                    log.Warn("the description cant be longer than 300 ");
                    throw new Exception("the description length more than 300 ");
                }
            }
            return true;
        }
        /// <summary>
        /// this function check if a date is expired 
        /// </summary>
        /// <param name="date">the date we havr to check</param>
        /// <exception cref="Exception">if the duo date is null</exception>
        /// <exception cref="Exception">if the date is expired</exception>
        /// <returns>true if the date is not expired</returns>
        public bool Check_Date(DateTime date)
        {
            if (date == null)
            {
                log.Warn("the date is null");
                throw new Exception("the date is null");
            }
            if (DateTime.Now > date)
            {
                log.Warn("the date should not be expired");
                throw new Exception("the date should not be expired ");
            }
            return true;
        }
        public Column getcolumn(string board, int Ordinal)
        {
            return new Column(Ordinal, board);
        }
        /// <summary>
        /// this function checks if the email is valid
        /// </summary>
        /// <param name="email">the email we have to check</param>
        /// <returns>true if the email is valid</returns>
        /// <exception cref="Exception">if the email is not valid</exception>



        /// <summary>
        /// this function checks if the user is online
        /// </summary>
        /// <param name="email">the users email we have to check</param>
        /// <exception cref="Exception">if the user is not registered</exception>
        /// <returns>return true if the user online alse return false </returns>

        /// <summary>
        /// this function check if the column status is valid
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="Exception">if the column ordinal is valid</exception>
        private void check_Status(int s)
        {
            if (s < 0 || s > 2)
                throw new Exception("invalid column ordinal!");
        }
        //private Task getTask(int id ,string email ,string boardname,int Ordinal)
        //{
        //    return new Task(id,)
        //}
        public void set_status(string e, bool s)
        {
            this.User_status[e] = s;
        }

        public void JoinBoard(string email, int boardID)
        {
            //List<Board> boards = new List<Board>();
            //check vaild email
            if (email == null || email == "")
            {
                log.Debug("attempt to join board using a non valid email");
                throw new Exception("not vaild email");
            }
            if (!user.IsRegestered(email) || !user.isLoggedin_user(email))
            {
                log.Warn("the is not registered or loggedin");
                throw new Exception("invalid input");
            }
            //check if the boardid is existing
            if (!ListOfBoards.ContainsKey(boardID))
            {
                log.Warn("attempt to join board using a non valid boardID");
                throw new Exception("Board does not exist!");
            }
            BoardClass tojoinboard = ListOfBoards[boardID];
            List<BoardClass> userBoards = User_Board[email];
            foreach (BoardClass board in userBoards)
            {
                if (board.getboardname().Equals(tojoinboard.getboardname()))
                {
                    log.Warn("you already are a member of the board");
                    throw new Exception("cant join a board with the same name of an already joined board");
                }
            }

            //if (User_Board[email].Count > 0)
            User_Board[email].Add(tojoinboard);
            //else
            //{
            //    userBoards.Add(tojoinboard);
            //    User_Board.Add(email, userBoards);
            //}
            dalboardcontroller.Insert(email, boardID);
        }


        /// <summary>
        /// this function gets a board
        /// </summary>
        /// <param name="email">the email who wants the board</param>
        /// <param name="Boardname">the board name</param>
        /// <returns>a board</returns>
        /// <exception cref="Exception">if the board does npt exsists</exception>
        public  BoardClass GetBoard(string email, string Boardname)
        {
            if (email == null || email == " ")
            {
                log.Warn("invalid email");
                throw new Exception("invalid email");
            }
            if (!user.IsRegestered(email) || !user.isLoggedin_user(email))
            {
                log.Warn("the is not registered or loggedin");
                throw new Exception("invalid input");
            }
            if (!User_Board.ContainsKey(email))
            {
                log.Warn("the user is not registered");
                throw new Exception("user not Registered");
            }
            List<BoardClass> boards = User_Board[email];
            foreach (BoardClass board in boards)
            {
                if (board.getboardname().Equals(Boardname))
                {
                    return board;
                }
            }
            log.Warn("Board name is not in the dictionary");
            throw new Exception("Board does not exsist");
        }

        public void TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            if (currentOwnerEmail == null || newOwnerEmail == null || boardName == null || currentOwnerEmail == "" || newOwnerEmail == "" || boardName == "")
            {
                log.Warn("one or more of the inputs are not vaild ");
                throw new Exception("one or more of the inputs are not vaild");
            }
            if (!user.IsRegestered(currentOwnerEmail) || !user.IsRegestered(newOwnerEmail))
            {
                log.Warn("one or more user is not registered");
                throw new Exception("invalid user");
            }
            if (!user.isLoggedin_user(currentOwnerEmail) || !user.isLoggedin_user(newOwnerEmail))
            {
                log.Warn("one or more user is not loggedIn");
                throw new Exception("invalid user");
            }
            BoardClass board = GetBoard(currentOwnerEmail, boardName);
            if (!board.getemail().Equals(currentOwnerEmail))
            {
                log.Warn("the user is not the owner of the board");
                throw new Exception("the user is not the owner of the board");
            }
            if (!User_Board[newOwnerEmail].Contains(board))
            {
                log.Warn("the new owner is not a member in the board");
                throw new Exception("the new owner is not a member in the board");
            }
            board.SetEmail(newOwnerEmail);
            board.owner = userController.users[newOwnerEmail];
            board.ToDal().Save();
        }


        /// <summary>
        /// This method removes a user from the members list of a board.
        /// </summary>
        /// <param name="email">The email of the user. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs </returns>
        public void LeaveBoard(string email, int boardID)
        {
            BoardClass BoardToLeave = ListOfBoards[boardID];
            //check vaild email
            if (email == null || email == "")
            {
                log.Debug("attempt to join board using a non valid email");
                throw new Exception("not vaild email");
            }
            if (!ListOfBoards.ContainsKey(boardID))
            {
                log.Warn("attempt to leave a non existent board");
                throw new Exception("Board doesnt exist!");
            }
            if (!User_Board[email].Contains(BoardToLeave))
            {
                log.Warn("the user doesnt have the board we  want to leave");
                throw new Exception("no such a board");
            }
            if (user.users[email].Equals(BoardToLeave.owner))
            {
                log.Warn("a board owner attempted to leave the board");
                throw new Exception("Board owner cannot leave the board!");
            }
            BoardToLeave.deleteAllTasks(email);
            //BoardToLeave.columns_list.Clear();
            User_Board[email].Remove(BoardToLeave);
            dalboardcontroller.DeleteJoin(email, boardID);
        }

        public void AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            if (columnOrdinal < 0 || columnOrdinal > 2)
            {
                throw new Exception("invalid column ordinal!");
            }
            if (email == null || emailAssignee == null || emailAssignee == "" || email == "")
            {
                log.Debug("one or more of the inputs are not vaild ");
                throw new Exception("one or more of the inputs are not vaild");
            }
            if (columnOrdinal == 2)
            {
                log.Warn("cant change the assignee while the task is done");
                throw new Exception("cant change a task assignee that is done");
            }
            BoardClass board = GetBoard(email, boardName);
            Task task = GetTask(email, boardName, columnOrdinal, taskID);
            if (!User_Board[emailAssignee].Contains(board))
            {
                log.Warn("the email assignee is not a member of the board");
                throw new Exception("the email assignee is not a member of the board");
            }
            if (!User_Board[email].Contains(board))
            {
                log.Warn("the email is not a member of the board");
                throw new Exception("the email is not a member of the board");
            }

            if (task.GetAssignee().Equals("unassigned"))
            {
                task.SetAssignee(emailAssignee);
                task.ToDal().Assignee = emailAssignee;
                task.ToDal().Save();
            }
            else
            {
                if (!task.GetAssignee().Equals(email))
                {
                    log.Warn("this user is not the assignee of the task");
                    throw new Exception("The user is not the assigne of this task");
                }
                task.SetAssignee(emailAssignee);
                task.ToDal().Assignee = emailAssignee;
                task.ToDal().Save();
            }
        }




        /// <summary>
        /// this function gets a task
        /// </summary>
        /// <param name="email">the user email</param>
        /// <param name="Boardname">the boardname </param>
        /// <param name="columnordinal">the column ordinal the task in</param>
        /// <param name="taskid">the task id we want to return</param>
        /// <returns>a task</returns>
        /// <exception cref="Exception">if the task does not exsists</exception>
        private Task GetTask(string email, string Boardname, int columnordinal, int taskid)
        {
            BoardClass tasksBoard = GetBoard(email, Boardname);
            Column column = tasksBoard.getCol(columnordinal);
            List<Task> tasks = column.List_Of_Tasks();
            foreach (Task task in tasks)
            {
                if (task.getid() == taskid)
                {
                    return task;
                }
            }
            log.Warn("task id does not exsist");
            throw new Exception("task does not exsist");
        }
        public string GetBoardName(int boardId)
        {

            foreach (BoardClass b in ListOfBoards.Values)
            {
                if (b.getboardid() == boardId)
                {
                    Console.WriteLine("youre amazing");
                    Console.WriteLine(b.getboardname());
                    return b.getboardname();
                }
            }
            //if (!ListOfBoards.ContainsKey(boardId))

            log.Warn("no board exist with this boardid");
            throw new Exception("board does not exists");

            // return ListOfBoards[boardId].getboardname();
        }


        public List<int> GetUserBoards(string email)
        {
            // check the mail 
            if (!user.IsRegestered(email))
            {
                log.Warn("no such email");
                throw new Exception("no email");
            }
            if (!user.isLoggedin_user(email))
            {
                log.Warn("offline user");
                throw new Exception("User is offline");
            }
            if (email == "" || email.Equals(null))
            {
                log.Warn("email cant be null or empty");
                throw new Exception("email cant be null or empty");
            }
            if (!User_Board.ContainsKey(email))
            {
                log.Warn("attempt to get list of ids using a non registered email");
                throw new Exception("User email is not registered");
            }

            List<BoardClass> str = this.User_Board[email];
            List<int> id = new List<int>();
            foreach (BoardClass b in str)
            {
                id.Add(b.getboardid());
            }
            return id;
        }

    }

}
