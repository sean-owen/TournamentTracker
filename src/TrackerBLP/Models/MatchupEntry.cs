using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class MatchupEntry
    {
        public int Id { get; set; }
        public Team TeamCompeting { get; set; }
        public double Score { get; set; }
        public Matchup ParentMatchup { get; set; }
    }
}
