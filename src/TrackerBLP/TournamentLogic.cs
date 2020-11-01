using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP
{
    public static class TournamentLogic
    {
        public static void CreateRounds(Tournament tournament)
        {
            // TODO - functionality - implement create rounds & subsequently required methods
            tournament.Rounds = new List<List<Matchup>>()
            {
                new List<Matchup>()
                {
                    new Matchup()
                    {
                        Id = 1
                    },
                    new Matchup()
                    {
                        Id = 2
                    }                    
                },
                new List<Matchup>()
                {
                    new Matchup()
                    {
                        Id = 3
                    },
                    new Matchup()
                    {
                        Id = 4
                    }
                }
            };

        }
    }
}
