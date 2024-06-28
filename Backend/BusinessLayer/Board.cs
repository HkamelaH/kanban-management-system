using System;
using System.Collections.Generic;

namespace Backend.BusinessLayer
{
    public class Board
    {
        private string Name { get; private set; }
        private List<Column> Columns { get; private set; }
        private string Email;
        private int Id;
        private string Board_name;

        public Board(string email,string Boradname,  int id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Board name cannot be empty.");
            }
            Board_name= Boradname;
            Name = name;
            Id = id;
            Email = email;
            Columns = new List<Column>
            {
                  new Column(0, Boradname),
                  new Column(1, Boradname),
                  new Column(2, Boradname)
             };
        }
        public void setlimit(int ordinal, int lim)
        {
            if (ordinal == 0)
            {
                Column c = Columns[0];
                c.setlim(lim);
            }
            if (ordinal == 1)
            {
                Column c = Columns[1];
                c.setlim(lim);
            }
            if (ordinal == 2)
            {
                Column c = Columns[2];
                c.setlim(lim);
            }
        }
        public int getName(string name)
        {
            return Name;
        }
        public string getEmail(string email)
        {
            return Email;
        }
        public int getId(int Id)
        {
            return Id;
        }
        public list<Task> In_ProgressTasks()
        {
            return this.Columns[1].list_Of_Tasks;
        }
        public int GetLimit(int ordinal)
        {
            switch (ordinal)
            {
                case 0:
                    return Columns[0].getlim();
                case 1:
                    return Columns[1].getlim();
                default:
                    return Columns[2].getlim();
            }
        }

        public Column GetColumn(int ordinal)
        {
            switch (ordinal)
            {
                case 0:
                    return Columns[0];
                case 1:
                    return Columns[1];
                default:
                    return Columns[2];
            }
        }
        public string GetColumnName(int Ordinal)
        {

            return Columns[Ordinal].GetColumnName(Ordinal);
        }
        public string GetBoardName(int Ordinal)
        {

            return this.Board_name;
        }
    }
}
