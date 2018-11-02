using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Hackathon
{
    class InventoryTable
    {
        public static String SQL_SELECT = "SELECT * FROM Inventory";
        public static String SQL_COUNT = "SELECT COUNT(*) FROM Inventory";
        public static String SQL_SELECT_ID = "SELECT * FROM Inventory WHERE Slot=@slot";
        public static String SQL_INSERT = "INSERT INTO Inventory VALUES (@current, @PlayerID, @WeaponID, @slot)";
        public static String SQL_DELETE_ID = "DELETE FROM Inventory WHERE ID=@id";
        public static String SQL_UPDATE = "UPDATE Inventory SET Current=@current, Player_ID=@PlayerID, Weapon_ID=@WeaponID, Slot=@slot WHERE Slot=@slot";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Inventory inventory, Database pDb = null)
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
            PrepareCommand(command, inventory);
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
        public static int Update(Inventory inventory, Database pDb = null)
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
            PrepareCommand(command, inventory);
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
        public static Collection<Inventory> Select(Database pDb = null)
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

            Collection<Inventory> Produkts = Read(reader);
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
        /// <param name="id">Inventory id</param>
        public static Inventory Select(int id, Database pDb = null)
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

            Collection<Inventory> Produkts = Read(reader);
            Inventory Inventory = null;
            if (Produkts.Count == 1)
            {
                Inventory = Produkts[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Inventory;
        }
        public static int Select_Count(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_COUNT);
            int count = (int)command.ExecuteScalar();

            if (pDb == null)
            {
                db.Close();
            }

            return count;
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
        private static void PrepareCommand(SqlCommand command, Inventory inventory)
        {
            command.Parameters.AddWithValue("@current", inventory.Current);
            command.Parameters.AddWithValue("@PlayerID", inventory.Player_ID);
            command.Parameters.AddWithValue("@WeaponID", inventory.Weapon_ID);
            command.Parameters.AddWithValue("@slot", inventory.Slot);
        }
        private static Collection<Inventory> Read(SqlDataReader reader)
        {
            Collection<Inventory> Produkts = new Collection<Inventory>();

            while (reader.Read())
            {
                int i = -1;
                Inventory inventory = new Inventory();

                inventory.Current = reader.GetInt32(++i);
                inventory.Player_ID = reader.GetInt32(++i);
                inventory.Weapon_ID = reader.GetInt32(++i);
                inventory.Slot = reader.GetInt32(++i);

                Produkts.Add(inventory);
            }
            return Produkts;
        }
    }
}
