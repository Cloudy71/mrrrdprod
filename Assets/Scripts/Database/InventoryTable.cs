using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon
{
    class InventoryTable
    {
        public static String SQL_SELECT = "SELECT * FROM Inventory";
        public static String SQL_SELECT_ID = "SELECT * FROM Inventory WHERE ID=@id";
        public static String SQL_INSERT = "INSERT INTO Inventory VALUES (@current, @PlayerID, @WeaponID)";
        public static String SQL_DELETE_ID = "DELETE FROM Inventory WHERE ID=@id";
        public static String SQL_UPDATE = "UPDATE Inventory SET Current=@current, Player_ID=@PlayerID, Weapon_ID=@damage, Cost=@cost, Ammo=@ammo, Accuracy=@accuracy WHERE ID=@id";

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
            command.Parameters.AddWithValue("@ID", inventory.ID);
            command.Parameters.AddWithValue("@name", inventory.Name);
            command.Parameters.AddWithValue("@range", inventory.Range);
            command.Parameters.AddWithValue("@damage", inventory.Damage);
            command.Parameters.AddWithValue("@cost", inventory.Cost);
            command.Parameters.AddWithValue("@ammo", inventory.Ammo);
            command.Parameters.AddWithValue("@accuracy", inventory.Accuracy);
        }
        private static Collection<Inventory> Read(SqlDataReader reader)
        {
            Collection<Inventory> Produkts = new Collection<Inventory>();

            while (reader.Read())
            {
                int i = -1;
                Inventory inventory = new Inventory();

                inventory.ID = reader.GetInt32(++i);
                inventory.Name = reader.GetString(++i);
                inventory.Range = reader.GetInt32(++i);
                inventory.Damage = reader.GetInt32(++i);
                inventory.Cost = reader.GetInt32(++i);
                inventory.Ammo = reader.GetInt32(++i);
                inventory.Accuracy = reader.GetInt32(++i);

                Produkts.Add(inventory);
            }
            return Produkts;
        }
    }
}
