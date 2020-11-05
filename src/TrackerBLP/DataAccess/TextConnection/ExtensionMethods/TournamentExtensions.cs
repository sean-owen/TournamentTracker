using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class TournamentExtensions
    {
        public static List<Tournament> ConvertToTournaments(this List<string> lines)
        {
            List<Tournament> tournamentList = new List<Tournament>();
            List<Team> teamList = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();
            List<Prize> prizesList = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizes();
            List<Matchup> loadedMatchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchups();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Tournament tournament = new Tournament();
                tournament.Id = int.Parse(columns[ColumnFormats.Tournament.Id]);
                tournament.TournamentName = columns[ColumnFormats.Tournament.TournamentName];
                tournament.EntryFee = decimal.Parse(columns[ColumnFormats.Tournament.EntryFee]);

                tournament.EnteredTeams = new List<Team>();
                string[] enteredTeamsIds = columns[ColumnFormats.Tournament.EnteredTeams].Split('|');
                foreach (string teamId in enteredTeamsIds)
                {
                    int parsedId = int.Parse(teamId);
                    tournament.EnteredTeams.Add(teamList.First(x => x.Id == parsedId));

                }

                tournament.Prizes = new List<Prize>();
                string[] tournamentPrizeIds = columns[ColumnFormats.Tournament.Prizes].Split('|');
                foreach (string prizeId in tournamentPrizeIds)
                {
                    int parsedId = int.Parse(prizeId);
                    tournament.Prizes.Add(prizesList.First(x => x.Id == parsedId));
                }

                List<List<Matchup>> roundsList = new List<List<Matchup>>();
                string[] rounds = columns[ColumnFormats.Tournament.Rounds].Split("|");

                foreach (string round in rounds)
                {
                    List<Matchup> matchupIdsList = new List<Matchup>();
                    string[] matchupIds = round.Split('^');
                    foreach (string matchupId in matchupIds)
                    {
                        int parsedMatchupId = int.Parse(matchupId);
                        matchupIdsList.Add(loadedMatchups.First(x => x.Id == parsedMatchupId));
                    }

                    roundsList.Add(matchupIdsList);
                    tournament.Rounds.Add(matchupIdsList);
                }

                tournamentList.Add(tournament);
            }

            return tournamentList;
        }

        public static void SaveToTournamentsFile(this List<Tournament> tournamentList)
        {
            List<string> lines = new List<string>();

            foreach (Tournament tournament in tournamentList)
            {
                string enteredTeams = SaveTournamentEntries(tournament);
                string prizes = SaveTournamentPrizes(tournament);
                string rounds = SaveTournamentRounds(tournament);

                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ tournament.EntryFee},{ enteredTeams },{ prizes },{ rounds }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }

        private static string SaveTournamentEntries(Tournament tournament)
        {
            StringBuilder enteredTeams = new StringBuilder();
            foreach (Team team in tournament.EnteredTeams)
            {
                enteredTeams.Append($"{team.Id}|");
            }
            enteredTeams.Remove((enteredTeams.Length - 1), 1);

            return enteredTeams.ToString();
        }

        private static string SaveTournamentPrizes(Tournament tournament)
        {
            StringBuilder prizesSb = new StringBuilder();
            foreach (Prize prize in tournament.Prizes)
            {
                prizesSb.Append($"{prize.Id}|");
            }
            prizesSb.Remove((prizesSb.Length - 1), 1);

            return prizesSb.ToString();
        }

        private static string SaveTournamentRounds(Tournament tournament)
        {
            StringBuilder roundsSb = new StringBuilder();
            foreach (List<Matchup> round in tournament.Rounds)
            {
                StringBuilder matchupsSb = new StringBuilder();
                foreach (Matchup matchup in round)
                {

                    foreach (MatchupEntry entry in matchup.Entries)
                    {
                        entry.Id = CreateMatchupEntry(entry).Id;
                    }

                    matchup.Id = CreateMatchup(matchup).Id;
                    matchupsSb.Append($"{ matchup.Id }^");
                }
                matchupsSb.Remove((matchupsSb.Length - 1), 1);

                roundsSb.Append($"{matchupsSb}|");
            }
            roundsSb.Remove((roundsSb.Length - 1), 1);

            return roundsSb.ToString();
        }

        private static MatchupEntry CreateMatchupEntry(MatchupEntry model)
        {
            List<MatchupEntry> matchupEntries = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile().ConvertToMatchupEntries();

            int newId = 1;
            if (matchupEntries.Any())
            {
                newId = matchupEntries.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = newId;

            matchupEntries.Add(model);
            matchupEntries.SaveToMatchupEntriesFile();

            return model;
        }

        private static void SaveToMatchupEntriesFile(this List<MatchupEntry> matchupEntries)
        {
            List<string> lines = new List<string>();

            foreach (var entry in matchupEntries)
            {
                string teamCompetingId = entry.TeamCompeting?.Id.ToString() ?? string.Empty;
                string parentMatchupId = entry.ParentMatchup?.Id.ToString() ?? string.Empty;
                lines.Add($"{ entry.Id },{ teamCompetingId },{ entry.Score },{ parentMatchupId }");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), lines);
        }

        private static Matchup CreateMatchup(Matchup model)
        {
            List<Matchup> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchups();

            int newId = 1;
            if (matchups.Any())
            {
                newId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = newId;

            matchups.Add(model);
            matchups.SaveToMatchupsFile();

            return model;
        }

        private static void SaveToMatchupsFile(this List<Matchup> matchups)
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
    }
}
