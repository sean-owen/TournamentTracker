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
            // Dummy data
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

            // TODO - functionality - implement create rounds & subsequently required methods

            // 1 - calculate number of rounds based on teams
            int numberOfRounds = CalculateNumRounds(tournament.EnteredTeams.Count);

            // add round model to make life easier? -----------------

            // 2 - create matchup list for each round
            // 3 - add matchup entries for first round only (randomly select pairings and 'byes')
            tournament.Rounds = new List<List<Matchup>>();
            for (int i = 0; i < numberOfRounds; i++)
            {
                var roundMatchups = new List<Matchup>();
                if (i == 0)
                {
                    // randomly select matchups for first round
                    roundMatchups = CalculateFirstRoundMatchups();
                }
                tournament.Rounds.Add(roundMatchups);
            }
        }

        private static List<Matchup> CalculateFirstRoundMatchups()
        {
            // TODO - functionality - implement first round creator method (this one!)
            throw new NotImplementedException();
        }

        private static int CalculateNumRounds(int numberOfEnteredTeams)
        {
            var roundsIfEvenNumTeams = numberOfEnteredTeams / 2;
            var leftOverTeams = numberOfEnteredTeams % 2;

            int rounds = roundsIfEvenNumTeams;
            if (leftOverTeams != 0)
            {
                rounds += 1;
            }

            return rounds;
        }
    }
}
