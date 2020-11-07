using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class MatchupEntryExtensions
    {
        /// <summary>
        /// Converts a list of string to a list of MatchupEntry.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>List of MatchupEntry.</returns>
        public static List<MatchupEntry> ConvertToMatchupEntries(this List<string> lines)
        {
            List<MatchupEntry> loadedMatchupEntries = new List<MatchupEntry>();
            List<Team> loadedTeams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();

            foreach (var line in lines)
            {
                string[] columns = line.Split(',');

                MatchupEntry matchupEntry = new MatchupEntry();
                matchupEntry.Id = int.Parse(columns[ColumnFormats.MatchupEntry.Id]);
                matchupEntry.Score = double.Parse(columns[ColumnFormats.MatchupEntry.Score]);

                string teamCompetingId = columns[ColumnFormats.MatchupEntry.TeamCompeting];
                if (!string.IsNullOrWhiteSpace(teamCompetingId))
                {
                    matchupEntry.TeamCompeting = loadedTeams.First(x => x.Id == int.Parse(teamCompetingId));
                }

                string parentMatchupId = columns[ColumnFormats.MatchupEntry.ParentMatchup];
                if (!string.IsNullOrWhiteSpace(parentMatchupId))
                {
                    matchupEntry.ParentMatchup = LookUpMatchupById(int.Parse(parentMatchupId));
                }
                loadedMatchupEntries.Add(matchupEntry);
            }

            return loadedMatchupEntries;
        }

        private static Matchup LookUpMatchupById(int desiredId)
        {
            List<string> loadedMatchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

            foreach (string matchup in loadedMatchups)
            {
                string[] columns = matchup.Split(',');
                if (columns[ColumnFormats.modelId] == desiredId.ToString())
                {
                    List<string> requiredMatchupList = new List<string>()
                    {
                        matchup
                    };
                    return requiredMatchupList.ConvertToMatchups().First();
                }
            }

            return null;
        }
    }
}
