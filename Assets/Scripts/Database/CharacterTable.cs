using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Hackathon
{
    class CharacterTable
    {
        public static String SQL_SELECT = "SELECT * FROM Character";
        public static String SQL_SELECT_ID = "SELECT * FROM Character WHERE ID=@id";
        public static String SQL_INSERT = "INSERT INTO Character VALUES (@id, @name, @points, @attack, @health, @PlayerID)";
        public static String SQL_DELETE_ID = "DELETE FROM Character WHERE ID=@id";
        public static String SQL_UPDATE = "UPDATE Character SET Name=@name, Points=@points, Bonus_Attack=@damage, Cost=@cost, Ammo=@ammo, Accuracy=@accuracy WHERE ID=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Character character, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, character);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Character character, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, character);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Character> Select(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Character> Produkts = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Produkts;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">Character id</param>
        public static Character Select(int id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Character> Produkts = Read(reader);
            Character Character = null;
            if (Produkts.Count == 1)
            {
                Character = Produkts[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Character;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        public static int Delete(int id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// <summary>
        ///  Prepare a command.
        /// </summary>
        private static void PrepareCommand(SqlCommand command, Character character)
        {
            command.Parameters.AddWithValue("@ID", character.ID);
            command.Parameters.AddWithValue("@name", character.Name);
            command.Parameters.AddWithValue("@points", character.Points);
            command.Parameters.AddWithValue("@attack", character.BAttack);
            command.Parameters.AddWithValue("@health", character.BHealth);
            command.Parameters.AddWithValue("@PlayerID", character.Player_ID);
        }
        private static Collection<Character> Read(SqlDataReader reader)
        {
            Collection<Character> Produkts = new Collection<Character>();

            while (reader.Read())
            {
                int i = -1;
                Character character = new Character();

                character.ID = reader.GetInt32(++i);
                character.Name = reader.GetString(++i);
                character.Points = reader.GetInt32(++i);
                character.BAttack = reader.GetInt32(++i);
                character.BHealth = reader.GetInt32(++i);
                character.Player_ID = reader.GetInt32(++i);

                Produkts.Add(character);
            }
            return Produkts;
        }
    }
}
