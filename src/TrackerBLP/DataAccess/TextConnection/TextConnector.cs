using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnection.ExtensionMethods;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    public class TextConnector : IDataConnection
    {
        public Person CreatePerson(Person model)
        {
            List<Person> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPeople();

            int newId = 1;
            if (people.Any())
            {
                newId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = newId;

            people.Add(model);
            people.SaveToPeopleFile();

            return model;
        }

        public Prize CreatePrize(Prize model)
        {
            List<Prize> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizes();

            int newId = 1;
            if (prizes.Any())
            {
                newId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = newId;

            prizes.Add(model);
            prizes.SaveToPrizeFile();

            return model;
        }

        public Team CreateTeam(Team model)
        {
            List<Team> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();

            int newId = 1;
            if (teams.Any())
            {
                newId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = newId;

            teams.Add(model);
            teams.SaveToTeamsFile();

            return model;
        }

        public Tournament CreateTournament(Tournament model)
        {
            List<Tournament> tournaments = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournaments();

            int newId = 1;
            if (tournaments.Any())
            {
                newId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = newId;

            tournaments.Add(model);
            tournaments.SaveToTournamentsFile();

            return model;
        }


        /// <summary>
        /// Loads Prizes file, removes the entry passed in to this method and saves the updated file.        
        /// </summary>
        /// <param name="prize">The Prize to be removed from the Prizes file.</param>
        /// <returns>True if Prize was successfully removed from Prizes file, false otherwise.</returns>
        public bool DeletePrize(Prize prize)
        {
            List<Prize> loadedPrizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizes();

            var prizeToDelete = loadedPrizes.FirstOrDefault(x => x.Id == prize.Id);
            if (prizeToDelete != null)
            {
                loadedPrizes.Remove(prizeToDelete);
                loadedPrizes.SaveToPrizeFile();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Loads the list of Person saved in the People file.
        /// </summary>
        /// <returns>List of Person retrieved from the People file.</returns>
        public List<Person> LoadPeople()
        {
            List<Person> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPeople();
            return people;
        }

        /// <summary>
        /// Loads the list of Prize saved in the Prizes file.
        /// </summary>
        /// <returns>List of Prize retrieved from the Prizes file.</returns>
        public List<Prize> LoadPrizes()
        {
            List<Prize> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizes();
            return prizes;
        }

        /// <summary>
        /// Loads the list of Team saved in the Teams file.
        /// </summary>
        /// <returns>List of Team retrieved from the Teams file.</returns>
        public List<Team> LoadTeams()
        {
            List<Team> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();
            return teams;
        }

        /// <summary>
        /// Loads the list of Tournament saved in the Tournaments file.
        /// </summary>
        /// <returns>List of Tournament retrieved from the Tournaments file.</returns>
        public List<Tournament> LoadTournaments()
        {
            List<Tournament> tournaments = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournaments();
            return tournaments;
        }

        // TODO - backlog - consider deleted this method? Its not even used?
        /// <summary>
        /// Loads the list of rounds (or List<List<Matchup>>) indicated by the Ids saved in the Tournaments file.
        /// </summary>
        /// <returns></returns>
        public List<List<Matchup>> LoadRounds()
        {
            List<List<Matchup>> rounds = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournaments().FirstOrDefault().Rounds;
            return rounds;
        }

        /// <summary>
        /// Updates the passed in Matchup in the database.
        /// If the passed in Matchup can be found in the database it is updated, if it cannot be found a new Matchup is saved.
        /// </summary>
        /// <param name="model">The Matchup to update in the database.</param>
        /// <returns>True if the passed Matchup could be found in the database, false if it could not.</returns>
        public bool UpdateMatchup(Matchup model)
        {
            bool foundInDatabase = false;
            List<Matchup> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchups();

            var oldMatchup = matchups.FirstOrDefault(x => x.Id == model.Id);
            if (oldMatchup != null)
            {
                foundInDatabase = true;
                matchups.Remove(oldMatchup);
            }

            matchups.Add(model);
            matchups.SaveToMatchupsFile();

            return foundInDatabase;
        }
    }
}
