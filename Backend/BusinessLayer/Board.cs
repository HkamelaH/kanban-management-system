using IntroSE.Kanban.Backend.BusinessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinessLayer.Board
{
    public class BoardClass
    {
        private string User_Email;
        private string Board_name;
        private int Board_id;
        private List<Column> columns_list;

        public BoardClass(string email, string boardn, int id)
        {
            this.Board_id = id;
            this.Board_name = boardn;
            this.User_Email = email;
            this.columns_list = new List<Column>();
            this.columns_list.Add(new Column(0, boardn));
            this.columns_list.Add(new Column(1, boardn));
            this.columns_list.Add(new Column(2, boardn));
        }

        /// <summary>
        /// this function get the column from board 
        /// <summary>
        /// <baram name = "ord" > the column ordinary we want to retarn 0 1 2 </param>
        /// <returns> a the column in board </return > 

        public Column getCol(int Ordinal)
        {
            if (Ordinal == 0) return columns_list[0];
            if (Ordinal == 1) return columns_list[1];
            else return columns_list[2];
        }

        /// <summary>
        /// this function get the column name from board 
        /// <summary>
        /// <baram name = "ord" > the column name  we want to retarn 0 1 2 </param>
        /// <returns> a the column name  in board </return >

        /// <summary>
        /// this function return the user email that created the board
        /// </summary>
        /// <returns>the email of the user who created the board</returns>
        public string getemail()
        {
            return this.User_Email;
        }

        /// <summary>
        /// this function return the user board name  that created the board
        /// </summary>
        /// <returns>the board name of the user who created the board</returns>
        public string getboardname()
        {
            return this.Board_name;
        }

        public string GetColumnName(int Ordinal)
        {
            //Console.WriteLine(columns_list[Ordinal].GetColumnName(Ordinal));
            return columns_list[Ordinal].GetColumnName(Ordinal);
        }

        /// <summary>
        /// this function return the user email that created the board
        /// </summary>

        /// <param name = "status " > the column name  we want to retarn the limit of </param>
        /// <returns>the email of the user who created the board</returns>
        public int getlimit(int Ordinal)

        {
            if (Ordinal == 0) return columns_list[0].getlim();
            if (Ordinal == 1) return columns_list[1].getlim();
            else return columns_list[2].getlim();
        }

        /// <summary>
        /// this function updait the limit of a status column
        /// /// </summary>

        /// <param name = "status " > the column name  we want to update  the limit of  "lim" the new limit </param>

        public void setlimit(int ordinal, int lim)
        {
            if (ordinal == 0)
            {
                Column c = columns_list[0];
                c.setlim(lim);
            }
            if (ordinal == 1)
            {
                Column c = columns_list[1];
                c.setlim(lim);
            }
            if (ordinal == 2)
            {
                Column c = columns_list[2];
                c.setlim(lim);
            }

        }
        /// <summary>
        /// this function update the limit of a all column
        /// /// </summary>
        /// <baram name =  "lim" the new limit </param>
        public void limit_all(int lim)
        {
            this.columns_list[0].setlim(lim);
            this.columns_list[1].setlim(lim);
            this.columns_list[2].setlim(lim);

        }
        /// <summary>
        /// return a task list of the in progress tasks 
        /// </summary>
        /// <returns>the list of tasks </returns>
        public List<Task> In_progressTasks()
        {
            return this.columns_list[1].List_Of_Tasks();
        }
    }
}
