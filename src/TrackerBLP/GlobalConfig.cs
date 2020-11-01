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
        public const string TextFileOutputFolderPath = @"C:\TournamentTracker";
        public const string PrizesFile = "PrizeModels.csv";
        public const string PeopleFile = "PersonModels.csv";
        public const string TeamsFile = "TeamModels.csv";
        public const string TournamentsFile = "TournamentModels.csv";
        public const string MatchupsFile = "MatchupModels.csv";
        public const string MatchupEntriesFile = "MatchupEntryModels.csv";

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
