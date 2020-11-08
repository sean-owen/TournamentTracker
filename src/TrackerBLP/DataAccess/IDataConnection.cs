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
        Team CreateTeam(Team model);
        List<Person> LoadPeople();
        List<Team> LoadTeams();
        List<Prize> LoadPrizes();
        List<Tournament> LoadTournaments();
        List<List<Matchup>> LoadRounds();
        Tournament CreateTournament(Tournament model);
        bool DeletePrize(Prize model);
        bool UpdateMatchup(Matchup model);


    }
}
