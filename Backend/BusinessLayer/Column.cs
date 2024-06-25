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
        private List<Task> TasksList;
        private string ColumnName;
        private string board;
        /// <summary>
        /// constructor for the column class
        /// </summary>
        /// <param name="status_of_column ">the  column status </param>
        public Column(int Ordinal, string board)
        {
            this.col_Ordinal = Ordinal;
            this.TasksList = new List<Task>();
            this.limit_of_tasks = -1;
            switch (ordinal)
            {
                case 0:
                    this.columnName = "backlog";
                    break;
                case 1:
                    this.columnName = "in progress";
                    break;
                default:
                    this.columnName = "done";
                    break;
            };


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
            return this.TasksList.Count;
        }
        /// <summary>
        /// this function return a list of tasks in the column 
        /// </summary>
        /// <return>the function return a list of tasks </return>
        public List<Task> List_Of_Tasks()
        {
            return this.TasksList;
        }
        public void AddTask(string email, string title, string description, DateTime dueDate, int taskId)
        {
            if (!(TasksList.Count == limit_of_tasks))
            {
                TasksList.Add(new Task(taskId, dueDate, title, description, email, board));

            }
            else throw new Exception(" No room for more Tasks to add ");
        }

        public string GetColumnName(int Ordinal)
        {
            switch (ordinal)
            {
                case 0:
                    return "backlog";
                case 1:
                    return "in progress";
                default:
                    return "done";
            }
        }
        public void Remove(Task task)
        {
            if (TasksList.Count > 0)
            {
                TasksList.Remove(task);
            }
            else throw new Exception(" No room for more Tasks to remove ");
        }
    }
}