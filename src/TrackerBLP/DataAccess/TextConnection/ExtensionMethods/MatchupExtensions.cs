﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class MatchupExtensions
    {
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
