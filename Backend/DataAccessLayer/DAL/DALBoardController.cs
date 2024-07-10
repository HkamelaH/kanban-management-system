using System;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DAL
{
    public class DALBoardController : DALcontroller
    {
        private const string BoardTable = "Boards";

        public DALBoardController() : base(BoardTable) { }

        /// <summary>
        /// Converts a data reader into a BoardDTO object.
        /// </summary>
        /// <param name="reader">SQLiteDataReader object</param>
        /// <returns>Converted BoardDTO object</returns>
        protected override DTOs ConvertReaderToObject(SQLiteDataReader reader)
        {
            int boardId = reader.GetInt32(0);
            string boardName = reader.GetString(1);
            string userEmail = reader.GetString(2);

            BoardDTO result = new BoardDTO(boardId, boardName, userEmail);
            return result;
        }

        /// <summary>
        /// Retrieves all boards from the Boards table.
        /// </summary>
        /// <returns>List of BoardDTO objects</returns>
        public List<BoardDTO> GetAllBoards()
        {
            return Select().Cast<BoardDTO>().ToList();
        }

        /// <summary>
        /// Retrieves all boards associated with a specific user.
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>List of BoardDTO objects</returns>
        public List<BoardDTO> GetUserBoards(string email)
        {
            List<BoardDTO> result = new List<BoardDTO>();
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                try
                {
                    connections.Open();
                    command.CommandText = $"SELECT * FROM {BoardTable} WHERE UserEmail = @Email";
                    SQLiteParameter emailParam = new SQLiteParameter(@"Email", email);
                    command.Parameters.Add(emailParam);
                    command.Prepare();
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new BoardDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                catch (Exception e)
                {
                    log.Error("Error retrieving user boards from database");
                    log.Debug(e.Message);
                }
                finally
                {
                    command.Dispose();
                    connections.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Inserts a new board into the Boards table.
        /// </summary>
        /// <param name="board">BoardDTO object</param>
        /// <returns>True if insertion was successful, false otherwise</returns>
        public bool InsertBoard(BoardDTO board)
        {
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                int res = -1;
                try
                {
                    connections.Open();
                    command.CommandText = $"INSERT INTO {BoardTable}(BoardID, BoardName, UserEmail) VALUES(@BoardID, @BoardName, @UserEmail)";
                    SQLiteParameter P1 = new SQLiteParameter(@"BoardID", board.BoardID);
                    SQLiteParameter P2 = new SQLiteParameter(@"BoardName", board.BoardName);
                    SQLiteParameter P3 = new SQLiteParameter(@"UserEmail", board.UserEmail);
                    command.Parameters.Add(P1);
                    command.Parameters.Add(P2);
                    command.Parameters.Add(P3);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("Error inserting to database");
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
        /// Deletes a board from the Boards table by its ID.
        /// </summary>
        /// <param name="boardId">Board ID</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        public bool DeleteBoard(int boardId)
        {
            using (var connections = new SQLiteConnection(connection))
            {
                SQLiteCommand command = new SQLiteCommand(null, connections);
                int res = -1;
                try
                {
                    connections.Open();
                    command.CommandText = $"DELETE FROM {BoardTable} WHERE BoardID = @BoardID";
                    SQLiteParameter idParam = new SQLiteParameter(@"BoardID", boardId);
                    command.Parameters.Add(idParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("Error deleting from database");
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




    /// <summary>
    /// Transfers the ownership of a board to a new owner.
    /// </summary>
    /// <param name="newOwnerEmail">The email of the new owner</param>
    /// <param name="boardName">The name of the board</param>
    public void TransferOwnerShip(string newOwnerEmail, string boardName)
    {
        using (var connections = new SQLiteConnection(connection))
        {
            SQLiteCommand command = new SQLiteCommand(null, connections);
            try
            {
                connections.Open();
                command.CommandText = $"UPDATE {BoardTable} SET UserEmail = @NewOwnerEmail WHERE BoardName = @BoardName";
                SQLiteParameter newOwnerEmailParam = new SQLiteParameter(@"NewOwnerEmail", newOwnerEmail);
                SQLiteParameter boardNameParam = new SQLiteParameter(@"BoardName", boardName);
                command.Parameters.Add(newOwnerEmailParam);
                command.Parameters.Add(boardNameParam);
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                log.Error("Error transferring board ownership in database");
                log.Debug(e.Message);
            }
            finally
            {
                command.Dispose();
                connections.Close();
            }
        }
    }


}
