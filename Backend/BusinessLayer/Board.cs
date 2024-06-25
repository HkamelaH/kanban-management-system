using System;
using System.Collections.Generic;

namespace Backend.BusinessLayer
{
    public class Board
    {
        public string Name { get; private set; }
        public List<Column> Columns { get; private set; }
        public string Email;
        public int Id;


        public Board(string name, string email, int id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Board name cannot be empty.");
            }
            Name = name;
            Id = id;
            Email = email;
            Columns = new List<Column>
            {
                new Column("Backlog"),
                new Column("In Progress"),
                new Column("Done")
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
        public int getlimit(int Ordinal)

        {
                if (Ordinal == 0) return Columns[0].getlim();
                if (Ordinal == 1) return Columns[1].getlim();
                else return Columns[2].getlim();
        }
         public Column getCol(int Ordinal)
        {
            if (Ordinal == 0) return Columns[0];
            if (Ordinal == 1) return Columns[1];
            else return Columns[2];
        }
        public string GetColumnName(int Ordinal)
        {
            
            return Columns[Ordinal].GetColumnName(Ordinal);
        }
