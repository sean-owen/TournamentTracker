using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class MatchupExtensions
    {
        /// <summary>
        /// Saves a list of Matchup to a text file.
        /// </summary>
        /// <param name="matchups">List of Matchup to be saved to a text file.</param>
        public static void SaveToMatchupsFile(this List<Matchup> matchups)
        {
            List<string> lines = new List<string>();            

            foreach (var entry in matchups)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MatchupEntry matchupEntry in entry.Entries)
                {
                    sb.Append($"{ matchupEntry.Id }|");
                }
                sb.Remove((sb.Length - 1), 1);

                string winnerId = entry.Winner?.Id.ToString() ?? string.Empty;

                lines.Add($"{ entry.Id },{ sb },{ winnerId },{ entry.MatchupRound }");
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }

        /// <summary>
        /// Converts a list of string to a list of Matchup.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>List of Matchup.</returns>
        public static List<Matchup> ConvertToMatchups(this List<string> lines)
        {
            List<Matchup> matchups = new List<Matchup>();
            List<Team> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Matchup matchup = new Matchup();
                matchup.Id = int.Parse(columns[ColumnFormats.Matchup.Id]);
                if (!string.IsNullOrWhiteSpace(columns[ColumnFormats.Matchup.Winner]))
                {
                    matchup.Winner = teams.First(x => x.Id == int.Parse(columns[ColumnFormats.Matchup.Winner]));
                }
                matchup.MatchupRound = int.Parse(columns[ColumnFormats.Matchup.MatchupRound]);


                var matchupEntryIds = columns[ColumnFormats.Matchup.Entries].Split('|');
                foreach (string id in matchupEntryIds)
                {
                    int parsedId = int.Parse(id);
                    MatchupEntry entry = LookUpMatchupEntryById(parsedId);
                    matchup.Entries.Add(entry);
                }

                matchups.Add(matchup);
            }

            return matchups;
        }

        private static MatchupEntry LookUpMatchupEntryById(int id)
        {
            List<string> matchupEntriesFile = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();
            foreach (string line in matchupEntriesFile)
            {
                string[] columns = line.Split(',');

                if (columns[ColumnFormats.modelId] == id.ToString())
                {
                    return new List<string>() { line }.ConvertToMatchupEntries().First();
                }
            }

            return null;
        }
    }
}
