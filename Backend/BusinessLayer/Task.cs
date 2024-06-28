using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinessLayer.Board

{
    public class Task
    {
        public int id { get; set; }
        public readonly DateTime creationTime;
        public DateTime dueDate { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int ColumnOrdinal { get; set; }
        public string Email { get; set; }
        public string myboard { get; set; }


        /// <summary>
        /// constructor for the task class
        /// </summary>

        /// <param> name="id">the id and its unique</param>
        /// <param> name="creationTime"> the creationtime of the task</param>
        /// <param> name="dueDate">the task dueDate</param>
        /// <param> name="title">the task title</param>
        /// <param> name="Description">the task decsription</param>
        /// <param> name="board_Status">the task status or the column name of the task </param>
        /// <param> name="Email">the user email who created the task</param>
        public Task(int id, DateTime dueDate, string title, string description, string Email, string myboard)
        {
            this.id = id;
            this.creationTime = DateTime.Now;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
            this.ColumnOrdinal = 0;
            this.Email = Email;
            this.myboard = myboard;
        }
        /// <summary>
        /// this is a getter for the task id
        /// </summary>
        /// <returns>the task id</returns>
        public int getid()
        {
            return id;
        }
        /// <summary>
        /// this is a getter for the creationtime
        /// </summary>
        /// <returns>the task creationtime</returns>
        public DateTime getcreationTime()
        {
            return creationTime;
        }
        /// <summary>
        /// this is a getter for the task dueDate
        /// </summary>
        /// <returns>the task dueDate</returns>
        public DateTime getdueDate()
        {
            return dueDate;
        }
        /// <summary>
        /// this is a getter for the task title
        /// </summary>
        /// <returns>the task title</returns>
        public string gettitle()
        {
            return title;
        }
        /// <summary>
        /// this is a getter for the task description
        /// </summary>
        /// <returns>the task description</returns>
        public string getdescription()
        {
            return description;
        }
        /// <summary>
        /// this is a getter for the task status
        /// </summary>
        /// <returns>the task status</returns>
        public int getColumnOrdinal()
        {
            return ColumnOrdinal;
        }
        /// <summary>
        /// this is a getter for the task email
        /// </summary>
        /// <returns>the task email</returns>
        public string getEmail()
        {
            return Email;
        }
        public string getmyboard()
        {
            return myboard;
        }

        /// <summary>
        /// this is a setter for the task duedate
        /// </summary>
        /// <param name="title">the new duedate we want to set to the task</param>
        /// <return>the function does not return anything</return>
        public void setDuedate(DateTime newdueDate)
        {
            this.dueDate = newdueDate;
        }

        /// <summary>
        /// this is a setter for the task title
        /// </summary>
        /// <param name="title">the new title we want to set to the task</param>
        /// <return>the function does not return anything</return>
        public void setTitle(string newTitle)
        {
            this.title = newTitle;
        }


        /// <summary>
        /// this is a setter for the task description
        /// </summary>
        /// <param name="title">the new description we want to set to the task</param>
        /// <return>the function does not return anything</return>
        public void setdescription(string newdescription)
        {
            this.description = newdescription;
        }

        /// <summary>
        /// this is a setter for the task status
        /// </summary>
        /// <param name="title">the new status we want to set to the task</param>
        /// <return>the function does not return anything</return>

        public void setColumnOrdinal(int newOrdinal)
        {
            this.ColumnOrdinal = newOrdinal;
        }

        /// <summary>
        /// this is a setter for the task Email
        /// </summary>
        /// <param name="title">the new Email we want to set to the task</param>
        /// <return>the function does not return anything</return>
        public void setEmail(string newEmail)
        {
            this.Email = newEmail;
        }

        public Task getTask(int id, string email, string board, int Ordinal)
        {
            return new Task(id, dueDate, title, description, email, board);
        }
    }
}
