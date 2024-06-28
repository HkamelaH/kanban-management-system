using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinessLayer.Board;
//using IntroSE.Kanban.Backend.BussinessLayer.task;

namespace IntroSE.Kanban.Backend.BussinessLayer.Board
{
    public class Column
    {
        private int limit_of_tasks;
        private int col_Ordinal;
        private List<Task> List_of_tasks;
        private string ColumnName;
        private string board;
        /// <summary>
        /// constructor for the column class
        /// </summary>
        /// <param name="status_of_column ">the  column status </param>
        public Column(int Ordinal, string board)
        {
            this.col_Ordinal = Ordinal;
            this.List_of_tasks = new List<Task>();
            this.limit_of_tasks = -1;
            if (Ordinal == 0) ColumnName = "backlog";
            if (Ordinal == 1) ColumnName = "in progress";
            else ColumnName = "done";

            this.board = board;
        }
        /// <summary>
        /// this function return the limit of the task in the column 
        /// </summary>
        /// <return> return the limit </return>
        public int getlim()
        {
            return this.limit_of_tasks;
        }
        /// <summary>
        /// this function change the limit of the tasks in the column 
        /// </summary>
        /// <param> name="lim ">the new limit </param>
        /// <return>the function does not return anything</return>
        public void setlim(int lim)
        {
            if (lim == -1 || lim >= num_of_tasks())
            {
                this.limit_of_tasks = lim;
            }
            else { throw new Exception("Column contains tasks more than the limit!"); }

        }
        /// <summary>
        /// the function return the number of tasks in the column 
        /// </summary>
        /// <return>the function return the number of tasks </return>
        public int num_of_tasks()
        {
            return this.List_of_tasks.Count;
        }
        /// <summary>
        /// this function return a list of tasks in the column 
        /// </summary>
        /// <return>the function return a list of tasks </return>
        public List<Task> List_Of_Tasks()
        {
            return this.List_of_tasks;
        }
        public void AddTask(string email, string title, string description, DateTime dueDate, int taskId)
        {
            if (!(List_of_tasks.Count == limit_of_tasks))
            {
                List_of_tasks.Add(new Task(taskId, dueDate, title, description, email, board));

            }
            else throw new Exception("no more tasks can be added");
        }

        public string GetColumnName(int Ordinal)
        {
            if (Ordinal == 0) return "backlog";
            if (Ordinal == 1) return "in progress";
            else return "done";
        }
        public void Remove(Task task)
        {
            if (List_of_tasks.Count > 0)
                List_of_tasks.Remove(task);
        }
    }
}
