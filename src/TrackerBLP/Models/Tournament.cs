using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Tournament
    {
        public string TournamentName { get; set; }
        public decimal EntryFee { get; set; }
        public List<Team> EnteredTeams { get; set; }
        public List<Prize> Prizes { get; set; }
        public List<List<Matchup>> Rounds { get; set; }
    }
}
