using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scorecity;

namespace PlayerDatabase
{
    class GeneratePlayersDatabase
    {
        public static async void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=players.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS PlayerInfo (" +
                    "personId INTEGER PRIMARY KEY, " +
                    "firstName NVARCHAR(64) NULL," +
                    "lastName NVARCHAR(64) NULL," +
                    "teamId INTEGER NOT NULL," +
                    "jerseynumber INTEGER NOT NULL," +
                    "isActive BIT NOT NULL," +
                    "position NVARCHAR(8) NULL" +
                    ")";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                var players = (await Players.loadPlayers()).league.standard;
                var dleagueplayers = (await Players.loadDLeaguePlayers());

                for (int i = 0; i < players.Length; i++)
                    AddPlayer(players[i]);

                for (int i = 0; i < dleagueplayers.Count; i++)
                    AddDLeaguePlayer(dleagueplayers[i]._data);

                db.Close();
            }
        }

        public static void AddPlayer(Standard player)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=players.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT OR REPLACE INTO PlayerInfo VALUES (" +
                    "@personId," +
                    "@firstName," +
                    "@lastName," +
                    "@teamId," +
                    "@jerseynumber," +
                    "@isActive," +
                    "@position" +
                ");";

                insertCommand.Parameters.AddWithValue("@personId", player.personId);
                insertCommand.Parameters.AddWithValue("@firstName", player.firstName);
                insertCommand.Parameters.AddWithValue("@lastName", player.lastName);
                insertCommand.Parameters.AddWithValue("@teamId", player.teamId);
                insertCommand.Parameters.AddWithValue("@jerseynumber", player.jersey);
                insertCommand.Parameters.AddWithValue("@isActive", player.isActive);
                insertCommand.Parameters.AddWithValue("@position", player.pos);

                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        public static void AddDLeaguePlayer(_Data player)
        {
            if (player.fn != null && player.ln != null && player.num != null && player.pos != null)
            {
                using (SqliteConnection db =
                new SqliteConnection("Filename=players.db"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertCommand.CommandText = "INSERT OR REPLACE INTO PlayerInfo VALUES (" +
                        "@personId," +
                        "@firstName," +
                        "@lastName," +
                        "@teamId," +
                        "@jerseynumber," +
                        "@isActive," +
                        "@position" +
                    ");";


                    insertCommand.Parameters.AddWithValue("@personId", player.pid);
                    insertCommand.Parameters.AddWithValue("@firstName", player.fn);
                    insertCommand.Parameters.AddWithValue("@lastName", player.ln);
                    insertCommand.Parameters.AddWithValue("@teamId", player.tid);
                    insertCommand.Parameters.AddWithValue("@jerseynumber", player.num);
                    insertCommand.Parameters.AddWithValue("@isActive", 1);
                    insertCommand.Parameters.AddWithValue("@position", player.pos);

                    insertCommand.ExecuteReader();

                    db.Close();
                }
            }
        }
    }
}
