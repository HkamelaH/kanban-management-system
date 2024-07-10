using IntroSE.Kanban.Backend.DataAccessLayer.DAO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace IntroSE.Kanban.Backend.DataAccessLayer.DAL
{
    public class DALtaskcontroller : DALcontroller
    {
        private const string TaskTable = "Tasks";
        public DALtaskcontroller() :
            base(TaskTable)
        { }


        protected override DAOs ConvertReaderToObject(SQLiteDataReader reader)
        {
            int taskID;
            if (reader.IsDBNull(0))
            {
                taskID = 0;
            }
            else
            {
                taskID = reader.GetInt32(0);
            }

            DateTime createTime;
            if (reader.IsDBNull(1))
            {
                createTime = DateTime.MinValue;
            }
            else
            {
                createTime = DateTime.Parse(reader.GetString(1));
            }

            DateTime dueDate;
            if (reader.IsDBNull(2))
            {
                dueDate = DateTime.MinValue;
            }
            else
            {
                dueDate = DateTime.Parse(reader.GetString(2));
            }

            string title;
            if (reader.IsDBNull(3))
            {
                title = string.Empty;
            }
            else
            {
                title = reader.GetString(3);
            }

            string description;
            if (reader.IsDBNull(4))
            {
                description = string.Empty;
            }
            else
            {
                description = reader.GetString(4);
            }

            string email;
            if (reader.IsDBNull(5))
            {
                email = string.Empty;
            }
            else
            {
                email = reader.GetString(5);
            }

            int boardId;
            if (reader.IsDBNull(6))
            {
                boardId = 0;
            }
            else
            {
                boardId = reader.GetInt32(6);
            }

            int ordinal;
            if (reader.IsDBNull(7))
            {
                ordinal = 0;
            }
            else
            {
                ordinal = reader.GetInt32(7);
            }

            TaskDAO result = new TaskDAO(taskID, createTime, dueDate, title, description, email, boardId, ordinal);
            return result;
        }



        /// <summary>
		/// an a sql query to delete board's tasks from tasks table
		/// </summary>
		/// <param name="boardID"> boardId to specify the row</param>
		/// <returns></returns>
		public bool Delete(int boardID)
        {
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                int res = -1;
                try
                {
                    connections.Open();
                    command.CommandText = $"DELETE FROM {TaskTable} WHERE BoardID = @id";

                    SQLiteParameter Pram1 = new SQLiteParameter(@"id", boardID);
                    command.Parameters.Add(Pram1);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    log.Error("Error deleting to dataBase");
                    log.Debug(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connections.Close();
                }
                return res > -1;
            }
        }
        /// <summary>
        /// an a sql query to delete all tasks table data
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                int res = -1;
                try
                {
                    connections.Open();
                    command.CommandText = $"DELETE FROM {TaskTable}";

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("Error deleting from dataBase");
                    log.Debug(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connections.Close();
                }
                return res > -1;
            }

        }

        /// <summary>
		/// an a sql query to add a task in the tasks table
		/// </summary>
		/// <param name="tsk"> task dao to add its feilds to the tasks table</param>
		/// <returns></returns>
		public bool Insert(TaskDAO task)
        {
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                int res = -1;
                try
                {
                    connections.Open();
                    command.CommandText = $"INSERT INTO {TaskTable}({TaskDAO.Task_BoardId}, {TaskDAO.Task_BoardOrdinal} ,{TaskDAO.Task_ID}, {TaskDAO.Task_Email}, {TaskDAO.Task_ColumnTitle},{TaskDAO.Task_ColumnDescreption},{TaskDAO.Task_ColumnDueDate},{TaskDAO.Tasks_ColumnCreationTime})"
                        + $"VALUES(@brdid ,@Ordinal, @tskid, @email , @title, @descreption , @duedate, @creationtime)";
                    SQLiteParameter Param1 = new SQLiteParameter(@"brdid", task.BoardID);
                    SQLiteParameter Param2 = new SQLiteParameter(@"Ordinal", task.ORDINAL);
                    SQLiteParameter Param3 = new SQLiteParameter(@"tskid", task.taskID);
                    SQLiteParameter Param4 = new SQLiteParameter(@"email", task.Assignee);
                    SQLiteParameter Param5 = new SQLiteParameter(@"title", task.TITLE);
                    SQLiteParameter Param6 = new SQLiteParameter(@"descreption", task.desciptions);
                    SQLiteParameter Param7 = new SQLiteParameter(@"duedate", task.DUEDATE);
                    SQLiteParameter Param8 = new SQLiteParameter(@"creationtime", task.CreationTime);
                    command.Parameters.Add(Param1);
                    command.Parameters.Add(Param2);
                    command.Parameters.Add(Param3);
                    command.Parameters.Add(Param4);
                    command.Parameters.Add(Param5);
                    command.Parameters.Add(Param6);
                    command.Parameters.Add(Param7);
                    command.Parameters.Add(Param8);
                    command.Prepare();
                    res = command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    log.Error("error occured while updating the database");
                    log.Debug(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connections.Close();
                }
                return res > -1;
            }
        }
        public List<TaskDAO> GetAlltasks()
        {
            return Select().Cast<TaskDAO>().ToList();
        }
        /// <summary>
        /// an a sql query to get all column's tasks to load the data
        /// </summary>
        /// <param name="brdid"> task's boardid</param>
        /// <param name="ord"> column ordinal</param>
        /// <returns></returns>
        public List<TaskDAO> SelectcolsTasks(int boardid, int ordinal)
        {
            List<DAOs> results = new List<DAOs>();
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                command.CommandText = $"select * from {TaskTable} WHERE BoardID = @boardid and Ordinal = @ordinal;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connections.Open();
                    SQLiteParameter Pram1 = new SQLiteParameter(@"brdid", boardid);
                    SQLiteParameter Pram2 = new SQLiteParameter(@"ord", ordinal);
                    command.Parameters.Add(Pram1);
                    command.Parameters.Add(Pram2);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connections.Close();
                }

            }
            List<TaskDAO> res2 = results.Cast<TaskDAO>().ToList();
            return res2;
        }


        public bool Update(int id, int brdid, string attributeName, string attributeValue)
        {

            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                int res = -1;
                try
                {
                    connections.Open();
                    command.CommandText = $"UPDATE {TaskTable} SET [{attributeName}] = @attributevalue WHERE TaskID = @ID AND BoardID = @brdId";
                    SQLiteParameter attrib = new SQLiteParameter(@"attributevalue", attributeValue);
                    SQLiteParameter idPar = new SQLiteParameter(@"ID", id);
                    SQLiteParameter brdPar = new SQLiteParameter(@"brdId", brdid);
                    command.Parameters.Add(idPar);
                    command.Parameters.Add(brdPar);
                    command.Parameters.Add(attrib);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    log.Error("Error in updating dataBase");
                    log.Debug(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connections.Close();
                }
                return res > -1;
            }
        }

    }
}
