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


        // Getters (these can be removed as properties can be directly accessed)
        public int GetId() => id;
        public DateTime GetCreationTime() => creationTime;
        public DateTime GetDueDate() => dueDate;
        public string GetTitle() => title;
        public string GetDescription() => description;
        public int GetColumnOrdinal() => ColumnOrdinal;
        public string GetEmail() => Email;
        public string GetBoard() => myboard;

        // Setters (these can be removed as properties can be directly set)
        public void SetDueDate(DateTime newDueDate) => dueDate = newDueDate;
        public void SetTitle(string newTitle) => title = newTitle;
        public void SetDescription(string newDescription) => description = newDescription;
        public void SetColumnOrdinal(int newOrdinal) => ColumnOrdinal = newOrdinal;
        public void SetEmail(string newEmail) => Email = newEmail;

        public Task getTask(int id, string email, string board, int Ordinal)
        {
            return new Task(id, dueDate, title, description, email, board);
        }
    }
}
