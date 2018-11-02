using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Hackathon
{
    class WeaponTable
    {
        public static String SQL_SELECT = "SELECT * FROM Weapon";
        public static String SQL_SELECT_ID = "SELECT * FROM Weapon WHERE ID=@id";
        public static String SQL_INSERT = "INSERT INTO Weapon VALUES (@id, @name, @range, @damage, @cost, @ammo, @accuracy)";
        public static String SQL_DELETE_ID = "DELETE FROM Weapon WHERE ID=@id";
        public static String SQL_UPDATE = "UPDATE Weapon SET Name=@name, Range=@range, Damage=@damage, Cost=@cost, Ammo=@ammo, Accuracy=@accuracy WHERE ID=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Weapon weapon, Database pDb = null)
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
            PrepareCommand(command, weapon);
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
        public static int Update(Weapon weapon, Database pDb = null)
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
            PrepareCommand(command, weapon);
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
        public static Collection<Weapon> Select(Database pDb = null)
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

            Collection<Weapon> Produkts = Read(reader);
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
        /// <param name="id">Weapon id</param>
        public static Weapon Select(int id, Database pDb = null)
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

            Collection<Weapon> Produkts = Read(reader);
            Weapon Weapon = null;
            if (Produkts.Count == 1)
            {
                Weapon = Produkts[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Weapon;
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
        private static void PrepareCommand(SqlCommand command, Weapon weapon)
        {
            command.Parameters.AddWithValue("@ID", weapon.ID);
            command.Parameters.AddWithValue("@name", weapon.Name);
            command.Parameters.AddWithValue("@range", weapon.Range);
            command.Parameters.AddWithValue("@damage", weapon.Damage);
            command.Parameters.AddWithValue("@cost", weapon.Cost);
            command.Parameters.AddWithValue("@ammo", weapon.Ammo);
            command.Parameters.AddWithValue("@accuracy", weapon.Accuracy);
        }
        private static Collection<Weapon> Read(SqlDataReader reader)
        {
            Collection<Weapon> Produkts = new Collection<Weapon>();

            while (reader.Read())
            {
                int i = -1;
                Weapon weapon = new Weapon();

                weapon.ID = reader.GetInt32(++i);
                weapon.Name = reader.GetString(++i);
                weapon.Range = reader.GetInt32(++i);
                weapon.Damage = reader.GetInt32(++i);
                weapon.Cost = reader.GetInt32(++i);
                weapon.Ammo = reader.GetInt32(++i);
                weapon.Accuracy = reader.GetInt32(++i);

                Produkts.Add(weapon);
            }
            return Produkts;
        }
    }
}
