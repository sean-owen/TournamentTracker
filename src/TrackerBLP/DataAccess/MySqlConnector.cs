using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    // TODO - add mysql / sql lite database
    class MySqlConnector : IDataConnection
    {
        public Person CreatePerson(Person model)
        {
            throw new NotImplementedException();
        }
        
        public Prize CreatePrize(Prize model)
        {
            throw new NotImplementedException();
        }

        public Team CreateTeam(Team model)
        {
            throw new NotImplementedException();
        }

        public Tournament CreateTournament(Tournament model)
        {
            throw new NotImplementedException();
        }

        public bool DeletePrize(Prize prize)
        {
            throw new NotImplementedException();
        }

        public List<Person> LoadPeople()
        {
            throw new NotImplementedException();
        }

        public List<Prize> LoadPrizes()
        {
            throw new NotImplementedException();
        }

        public List<Team> LoadTeams()
        {
            throw new NotImplementedException();
        }
    }
}
