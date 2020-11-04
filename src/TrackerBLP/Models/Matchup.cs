using System.Collections.Generic;

namespace TrackerBLP.Models
{
    public class Matchup
    {
        public int Id { get; set; }
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();
        public Team Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
