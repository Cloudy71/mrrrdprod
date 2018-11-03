using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using UnityEngine;

namespace Hackathon
{

     class ShootTable
    {
        public static String SQL_SELECT = "SELECT * FROM Shoot";
        public static String SQL_SELECT_ID = "SELECT * FROM Shoot WHERE ID=@id";
        public static String SQL_COUNT = "SELECT COUNT(*) FROM Shoot";
        public static String SQL_INSERT = "INSERT INTO Shoot VALUES (@id, @bullets, @damage, @accuracy, @lost)";
        public static String SQL_DELETE_ID = "DELETE FROM Shoot WHERE ID=@id";
        public static String SQL_DELETEALL = "DELETE FROM Shoot";
        public static String SQL_UPDATE = "UPDATE Shoot SET Bullets=@bullets, Damage=@damage, Accuracy=@accuracy, LostHP=@losthp WHERE ID=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Shoot shoot, Database pDb = null)
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
            PrepareCommand(command, shoot);
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
        public static int Update(Shoot shoot, Database pDb = null)
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
            PrepareCommand(command, shoot);
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
        public static Collection<Shoot> Select(Database pDb = null)
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

            Collection<Shoot> Produkts = Read(reader);
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
        /// <param name="id">Shoot id</param>
        public static Shoot Select(int id, Database pDb = null)
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

            Collection<Shoot> Produkts = Read(reader);
            Shoot Shoot = null;
            if (Produkts.Count == 1)
            {
                Shoot = Produkts[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Shoot;
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

        public static int DeleteAll(Database pDb = null)
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
            SqlCommand command = db.CreateCommand(SQL_DELETEALL);
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
        private static void PrepareCommand(SqlCommand command, Shoot shoot)
        {
            command.Parameters.AddWithValue("@ID", shoot.ID);
            command.Parameters.AddWithValue("@bullets", shoot.Bullets);
            command.Parameters.AddWithValue("@damage", shoot.Damage);
            command.Parameters.AddWithValue("@accuracy", shoot.Accuracy);
            command.Parameters.AddWithValue("@lost", shoot.LostHP);
        }
        private static Collection<Shoot> Read(SqlDataReader reader)
        {
            Collection<Shoot> Produkts = new Collection<Shoot>();

            while (reader.Read())
            {
                int i = -1;
                Shoot shoot = new Shoot();

                shoot.ID = reader.GetInt32(++i);
                shoot.Bullets = reader.GetInt32(++i);
                shoot.Damage = reader.GetInt32(++i);
                shoot.Accuracy = reader.GetInt32(++i);
                shoot.LostHP = reader.GetInt32(++i);

                Produkts.Add(shoot);
            }
            return Produkts;
        }

    }
}