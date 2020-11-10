using System;
using System.Collections.Generic;
using System.Linq;
using TrackerBLP.Models;

namespace TrackerBLP
{
    public static class TournamentLogic
    {
        /// <summary>
        /// Updates the matchup winner based on the matchup entry with the highest score.
        /// Checks if the round is complete and if it is, triggers emails to tournament teams to inform them.
        /// Checks if tournament is completed and if it is, triggers emails to tournament teams to inform them.
        /// </summary>
        /// <param name="tournament"></param>
        /// <param name="matchup"></param>
        /// <returns>A Tuple<bool, bool> where the first indicates if the round is complete, the second indicates if the tournament is complete.</returns>
        public static Tuple<bool, bool> ScoreMatchup(Tournament tournament, Matchup matchup)
        {
            double team1Score = matchup.Entries.First().Score;
            double team2Score = matchup.Entries.Last().Score;
            matchup.Winner = team1Score > team2Score ? matchup.Entries.First().TeamCompeting : matchup.Entries.Last().TeamCompeting;

            // TODO - backlog - feedback warning to user / log if item was not found in database  
            // using itemFoundInDatabase
            bool itemFoundInDatabase = GlobalConfig.Connection.UpdateMatchup(matchup);

            bool isRoundOver = IsRoundOver(tournament, matchup.MatchupRound);
            bool isTournamentOver = IsTournamentOver(tournament);

            if (isRoundOver && !isTournamentOver)
            {
                StartNewRound(tournament, matchup.MatchupRound + 1);
            }

            return new Tuple<bool, bool>(isRoundOver, isTournamentOver);
        }

        private static void StartNewRound(Tournament tournament, int roundToStart)
        {
            // Get Previous round winners
            List<Team> previousRoundWinners = new List<Team>();
            foreach (List<Matchup> round in tournament.Rounds)
            {
                if (round.FirstOrDefault().MatchupRound == (roundToStart - 1))
                {
                    foreach (Matchup matchup in round)
                    {
                        previousRoundWinners.Add(matchup.Winner);
                    }
                }
            }            

            // populate matchups for 'roundToStart'
            foreach (List<Matchup> round in tournament.Rounds)
            {
                if (round.FirstOrDefault().MatchupRound == roundToStart)
                {
                    foreach (Matchup matchup in round)
                    {
                        Team firstNewEntry = previousRoundWinners.FirstOrDefault();
                        previousRoundWinners.Remove(firstNewEntry);

                        Team secondNewEntry = previousRoundWinners.FirstOrDefault();
                        previousRoundWinners.Remove(secondNewEntry);

                        if (firstNewEntry != null && secondNewEntry != null)
                        {
                            matchup.Entries.Last().TeamCompeting = secondNewEntry;
                            matchup.Entries.First().TeamCompeting = firstNewEntry;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }

            // TODO - functionality - email to confirm new round has started & the matchups that will occur in this round
        }

        public static void CreateRounds(Tournament tournament)
        {
            List<Team> randomizedTeams = RandomizeTeamOrder(tournament.EnteredTeams);
            int numberOfRounds = CalculateNumRounds(randomizedTeams.Count);
            int numberOfByes = CalculateNumByes(numberOfRounds, randomizedTeams.Count);

            tournament.Rounds.Add(CreateFirstRound(numberOfByes, randomizedTeams));
            CreateOtherRounds(tournament, numberOfRounds);
        }

        private static bool IsTournamentOver(Tournament tournament)
        {
            bool isTournamentOver = true;
            foreach (List<Matchup> round in tournament.Rounds)
            {
                IEnumerable<Matchup> incompleteMatchup = round.Where(x => x.Winner == null);
                if (incompleteMatchup.Any())
                {
                    // round complete
                    isTournamentOver = false;
                }
            }

            if (isTournamentOver)
            {
                // TODO - functionality - Send email / text
            }

            return isTournamentOver;
        }

        private static bool IsRoundOver(Tournament tournament, int matchupRound)
        {
            foreach (List<Matchup> round in tournament.Rounds)
            {
                if (round.FirstOrDefault().MatchupRound == matchupRound)
                {
                    IEnumerable<Matchup> incompleteMatchup = round.Where(x => x.Winner == null);
                    if (!incompleteMatchup.Any())
                    {
                        // TODO - functionality - Send email / text

                        // round complete
                        return true;
                    }
                }
            }

            return false;
        }

        private static void CreateOtherRounds(Tournament tournament, int numberOfRounds)
        {
            int round = 2;
            List<Matchup> previousRound = tournament.Rounds[0];
            List<Matchup> currentRound = new List<Matchup>();
            Matchup currentMatchup = new Matchup();

            while (round <= numberOfRounds)
            {
                foreach (Matchup match in previousRound)
                {
                    currentMatchup.MatchupRound = round;
                    currentMatchup.Entries.Add(new MatchupEntry()
                    {
                        ParentMatchup = match
                    });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentRound.Add(currentMatchup);
                        currentMatchup = new Matchup();
                    }
                }

                tournament.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<Matchup>();
                round++;
            }

        }

        private static List<Matchup> CreateFirstRound(int byes, List<Team> teams)
        {
            var output = new List<Matchup>();
            Matchup current = new Matchup();

            foreach (Team team in teams)
            {
                current.MatchupRound = 1;
                current.Entries.Add(new MatchupEntry()
                {
                    TeamCompeting = team
                });


                if (byes > 0)
                {
                    output.Add(current);
                    current.Winner = team;
                    current = new Matchup();
                    byes--;
                }
                else if (current.Entries.Count > 1)
                {
                    output.Add(current);
                    current = new Matchup();
                }
            }

            return output;
        }

        private static int CalculateNumByes(int numberOfRounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= numberOfRounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;

            return output;

        }

        private static int CalculateNumRounds(int numberOfEnteredTeams)
        {
            int numRounds = 1;
            int reqNumTeams = 2;

            while (reqNumTeams < numberOfEnteredTeams)
            {
                numRounds += 1;

                reqNumTeams *= 2;
            }

            return numRounds;
        }

        private static List<Team> RandomizeTeamOrder(List<Team> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
