using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // create rounds here?

            tournaments.Add(model);
            tournaments.SaveToTournamentsFile();

            return model;
        }


        /// <summary>
        /// Loads prizes file, removes the entry passed in to this method and saves the updated file.        
        /// </summary>
        /// <param name="prize">The prize to be removed from the prizes file.</param>
        /// <returns>True if prize was successfully removed from prizes file, false oftherwise.</returns>
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
        /// Loads the list of person saved in the people file.
        /// </summary>
        /// <returns>List of person retrieved from the people file.</returns>
        public List<Person> LoadPeople()
        {
            List<Person> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPeople();
            return people;
        }

        /// <summary>
        /// Loads the list of prizes saved in the prizes file.
        /// </summary>
        /// <returns>List of prize retrieved from the prizes file.</returns>
        public List<Prize> LoadPrizes()
        {
            List<Prize> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizes();
            return prizes;
        }

        /// <summary>
        /// Loads the list of team saved in the reams file.
        /// </summary>
        /// <returns>List of team retrieved from the teams file.</returns>
        public List<Team> LoadTeams()
        {
            List<Team> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeams();
            return teams;
        }
    }
}
