using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using TrackerBLP.DataAccess;

namespace TrackerBLP
{
    public static class GlobalConfig
    {
        public static string TextFileOutputFolderPath = @"C:\TournamentTracker";
        public static string PrizesFile = "PrizeModels.csv";
        public static string PeopleFile = "PersonModels.csv";
        public static string TeamsFile = "TeamModels.csv";
        public static string TournamentsFile = "TournamentModels.csv";
        public static string MatchupsFile = "MatchupModels.csv";
        public static string MatchupEntriesFile = "MatchupEntryModels.csv";

        public static IDataConnection Connection { get; set; }

        public static void InitializeConnections(bool mySql = false, bool textFiles = true)
        {
            if (mySql)
            {
                MySqlConnector mySqlConnection = new MySqlConnector();
                Connection = mySqlConnection;

            }
            else if (textFiles)
            {
                TextConnector textConnection = new TextConnector();
                Connection = textConnection;

                // ensure file path for text files exists
                if (!Directory.Exists(TextFileOutputFolderPath))
                {
                    Directory.CreateDirectory(TextFileOutputFolderPath);
                }
            }
        }

    }
}
