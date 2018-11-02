﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon
{
    class PlayerTable
    {
        public static String SQL_SELECT = "SELECT * FROM Player";
        public static String SQL_SELECT_ID = "SELECT * FROM Player WHERE ID=@id";
        public static String SQL_INSERT = "INSERT INTO Player VALUES (@id, @name, @armor, @health, @skore)";
        public static String SQL_DELETE_ID = "DELETE FROM Player WHERE ID=@id";
        public static String SQL_UPDATE = "UPDATE Player SET Name=@name, Armor=@armor, Health=@health, Skore=@skore WHERE ID=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Player player, Database pDb = null)
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
            PrepareCommand(command, player);
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
        public static int Update(Player player, Database pDb = null)
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
            PrepareCommand(command, player);
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
        public static Collection<Player> Select(Database pDb = null)
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

            Collection<Player> Produkts = Read(reader);
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
        /// <param name="id">Player id</param>
        public static Player Select(int id, Database pDb = null)
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

            Collection<Player> Produkts = Read(reader);
            Player Player = null;
            if (Produkts.Count == 1)
            {
                Player = Produkts[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Player;
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
        private static void PrepareCommand(SqlCommand command, Player player)
        {
            command.Parameters.AddWithValue("@ID", player.ID);
            command.Parameters.AddWithValue("@name", player.Name);
            command.Parameters.AddWithValue("@armor", player.Armor);
            command.Parameters.AddWithValue("@health", player.Health);
            command.Parameters.AddWithValue("@skore", player.Score);
        }
        private static Collection<Player> Read(SqlDataReader reader)
        {
            Collection<Player> Produkts = new Collection<Player>();

            while (reader.Read())
            {
                int i = -1;
                Player player = new Player();

                player.ID = reader.GetInt32(++i);
                player.Name = reader.GetString(++i);
                player.Armor = reader.GetInt32(++i);
                player.Health = reader.GetInt32(++i);
                player.Score = reader.GetInt32(++i);

                Produkts.Add(player);
            }
            return Produkts;
        }
    }
}
