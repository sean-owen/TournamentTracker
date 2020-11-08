using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Tournament
    {
        /// <summary>
        /// The database ID of this tournament.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of this tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// The entry fee for this tournament.
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// The teams competing in this tournament.
        /// </summary>
        public List<Team> EnteredTeams { get; set; } = new List<Team>();

        /// <summary>
        /// The prizes for this tournament.
        /// </summary>
        public List<Prize> Prizes { get; set; } = new List<Prize>();

        /// <summary>
        /// The rounds in this tournament.
        /// </summary>
        public List<List<Matchup>> Rounds { get; set; } = new List<List<Matchup>>();

        /// <summary>
        /// Returns the TournamentName if it has been set, otherwise an error message.
        /// </summary>
        /// <returns>TournamentName if it has been set, otherwise an error message.</returns>
        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(TournamentName) ? TournamentName : "Error! This Tournaments' name is not set.";
        }
    }
}