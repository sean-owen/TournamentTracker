using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnectorExtensions
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            //return @$"{GlobalConfig.TextFileOutputFolderPath}\{fileName}";
            return Path.Combine(GlobalConfig.TextFileOutputFolderPath, fileName);
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<Prize> ConvertToPrizes(this List<string> lines)
        {
            List<Prize> prizes = new List<Prize>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Prize prize = new Prize();
                prize.Id = int.Parse(columns[ColumnFormats.Prize.Id]);
                prize.PlaceNumber = int.Parse(columns[ColumnFormats.Prize.PlaceNumber]);
                prize.PlaceName = columns[ColumnFormats.Prize.PlaceName];
                prize.PrizeAmount = decimal.Parse(columns[ColumnFormats.Prize.PrizeAmount]);
                prize.PrizePercentage = double.Parse(columns[ColumnFormats.Prize.PrizePercentage]);

                prizes.Add(prize);
            }

            return prizes;
        }

        public static List<Person> ConvertToPeople(this List<string> lines)
        {
            List<Person> people = new List<Person>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Person person = new Person();
                person.Id = int.Parse(columns[ColumnFormats.Person.Id]);
                person.FirstName = columns[ColumnFormats.Person.FirstName];
                person.LastName = columns[ColumnFormats.Person.LastName];
                person.EmailAddress = columns[ColumnFormats.Person.EmailAddress];
                person.PhoneNumber = columns[ColumnFormats.Person.PhoneNumber];

                people.Add(person);
            }

            return people;
        }

        // TODO - backlog - refactor - consider writing generic convert method (?)
        public static List<Team> ConvertToTeams(this List<string> lines)
        {
            List<Team> teamList = new List<Team>();
            List<Person> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPeople();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Team team = new Team();
                team.Id = int.Parse(columns[ColumnFormats.Team.Id]);
                team.TeamName = columns[ColumnFormats.Team.TeamName];

                team.TeamMembers = new List<Person>();
                string[] personIds = columns[ColumnFormats.Team.TeamMembersIds].Split('|');
                foreach (var id in personIds)
                {
                    int parsedId = int.Parse(id);
                    team.TeamMembers.Add(people.First(x => x.Id == parsedId));
                }

                teamList.Add(team);
            }

            return teamList;
        }

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
                matchupEntry.TeamCompeting = loadedTeams.First(x => x.Id == int.Parse(columns[ColumnFormats.MatchupEntry.TeamCompeting]));

                matchupEntry.ParentMatchup = LookUpMatchupByID(int.Parse(columns[ColumnFormats.MatchupEntry.ParentMatchup]));
                loadedMatchupEntries.Add(matchupEntry);
            }

            return loadedMatchupEntries;
        }

        public static Matchup LookUpMatchupByID(int desiredId)
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

        public static List<Matchup> ConvertToMatchups(this List<string> lines)
        {
            // TODO - functionality - test this method
            List<Matchup> matchups = new List<Matchup>();
            List<Team> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();
            List<MatchupEntry> loadedMatchupEntries = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile().ConvertToMatchupEntries();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Matchup matchup = new Matchup();
                matchup.Id = int.Parse(columns[ColumnFormats.Matchup.Id]);
                matchup.Winner = teams.First(x => x.Id == int.Parse(columns[ColumnFormats.Matchup.Winner]));
                matchup.MatchupRound = int.Parse(columns[ColumnFormats.Matchup.MatchupRound]);

                matchup.Entries = new List<MatchupEntry>();
                var matchupEntryIds = columns[ColumnFormats.Matchup.Entries].Split('|');
                foreach (string id in matchupEntryIds)
                {
                    int parsedId = int.Parse(id);
                    matchup.Entries.Add(loadedMatchupEntries.First(x => x.Id == parsedId));
                }

                matchups.Add(matchup);
            }

            return matchups;
        }

        public static List<Tournament> ConvertToTournaments(this List<string> lines)
        {
            // TODO - functionality - test this method
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
                string[] rounds = columns[ColumnFormats.Tournament.Rounds].Split("||");
                foreach (string round in rounds)
                {
                    List<Matchup> matchupIdsList = new List<Matchup>();
                    string[] matchupIds = round.Split('|');
                    foreach (string matchupId in matchupIds)
                    {
                        int parsedMatchupId = int.Parse(matchupId);
                        matchupIdsList.Add(loadedMatchups.First(x => x.Id == parsedMatchupId));
                    }

                    roundsList.Add(matchupIdsList);
                }

                tournamentList.Add(tournament);
            }

            return tournamentList;
        }


        public static void SaveToPrizeFile(this List<Prize> prizes)
        {
            List<string> lines = new List<string>();

            foreach (Prize prize in prizes)
            {
                lines.Add($"{ prize.Id },{ prize.PlaceNumber },{ prize.PlaceName },{ prize.PrizeAmount },{ prize.PrizePercentage }");
            }

            File.WriteAllLines(GlobalConfig.PrizesFile.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<Person> people)
        {
            List<string> lines = new List<string>();

            foreach (Person person in people)
            {
                lines.Add($"{ person.Id },{ person.FirstName },{ person.LastName },{ person.EmailAddress },{ person.PhoneNumber }");
            }

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), lines);
        }

        public static void SaveToTeamsFile(this List<Team> teamList)
        {
            List<string> lines = new List<string>();

            foreach (Team team in teamList)
            {
                StringBuilder personIds = new StringBuilder();
                foreach (Person person in team.TeamMembers)
                {
                    personIds.Append($"{person.Id}|");
                }
                personIds.Remove((personIds.Length - 1), 1);

                lines.Add($"{ team.Id },{ personIds },{ team.TeamName }");
            }

            File.WriteAllLines(GlobalConfig.TeamsFile.FullFilePath(), lines);
        }

        public static void SaveToTournamentsFile(this List<Tournament> tournamentList)
        {
            List<string> lines = new List<string>();

            foreach (Tournament tournament in tournamentList)
            {
                StringBuilder enteredTeams = new StringBuilder();
                foreach (Team team in tournament.EnteredTeams)
                {
                    enteredTeams.Append($"{team.Id}|");
                }
                enteredTeams.Remove((enteredTeams.Length - 1), 1);


                StringBuilder prizesSb = new StringBuilder();
                foreach (Prize prize in tournament.Prizes)
                {
                    prizesSb.Append($"{prize.Id}|");
                }
                prizesSb.Remove((prizesSb.Length - 1), 1);


                StringBuilder roundsSb = new StringBuilder();
                foreach (List<Matchup> round in tournament.Rounds)
                {
                    StringBuilder matchupsSb = new StringBuilder();
                    foreach (Matchup matchup in round)
                    {
                        matchupsSb.Append($"{matchup.Id}||");
                    }
                    matchupsSb.Remove((matchupsSb.Length - 2), 2);

                    roundsSb.Append($"{matchupsSb}|");
                }
                roundsSb.Remove((roundsSb.Length - 1), 1);

                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ tournament.EntryFee},{ enteredTeams },{ prizesSb },{ roundsSb }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }
    }    
}
