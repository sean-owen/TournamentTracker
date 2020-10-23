using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Matchup
    {
        public int Id { get; set; }
        public List<MatchupEntry> Entries { get; set; }
        public Team Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
