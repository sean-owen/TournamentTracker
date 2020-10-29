using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    public interface IDataConnection
    {
        Prize CreatePrize(Prize model);
        Person CreatePerson(Person model);
        List<Person> LoadPeople();
        List<Team> LoadTeams();
        List<Prize> LoadPrizes();
        Team CreateTeam(Team model);
        Tournament CreateTournament(Tournament model);
        bool DeletePrize(Prize prize);

    }
}
