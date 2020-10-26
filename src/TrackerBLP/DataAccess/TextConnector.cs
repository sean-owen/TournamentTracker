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
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";

        public Person CreatePerson(Person model)
        {
            List<Person> people = PeopleFile.FullFilePath().LoadFile().ConvertToPeople();

            int newId = 1;
            if(people.Any())
            {
                newId = people.OrderByDescending(x => x.Id).First().Id + 1;
                model.Id = newId;
            }

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

        public Prize CreatePrize(Prize model)
        {
            // Load text file and convert text to list<Prize>            
            List<Prize> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizes();

            int newId = 1;
            if (prizes.Any())
            {
                // find max id and increment it to select the current id
                newId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
                model.Id = newId;
            }           
            
            // add new record with new id
            prizes.Add(model);

            // convert prizes to List<string>
            // save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public Team CreateTeam(Team model)
        {
            List<Team> team = TeamsFile.FullFilePath().LoadFile().ConvertToTeams();

            int newId = 1;
            if (team.Any())
            {
                newId = team.OrderByDescending(x => x.Id).First().Id + 1;
                model.Id = newId;
            }

            team.Add(model);

            team.SaveToTeamsFile(PeopleFile);

            return model;
        }

        public List<Person> LoadPeople()
        {
            List<Person> people = PeopleFile.FullFilePath().LoadFile().ConvertToPeople();
            return people;
        }
    }
}
