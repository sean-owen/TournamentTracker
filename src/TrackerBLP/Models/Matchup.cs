using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackerBLP.Models
{
    public class Matchup
    {
        public int Id { get; set; }
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();
        public Team Winner { get; set; }
        public int MatchupRound { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Entries.Any())
            {
                foreach (MatchupEntry entry in Entries)
                {
                    string teamName = entry.TeamCompeting?.TeamName ?? "Error! Team name missing.";
                    sb.Append($"{teamName} vs");
                }
                sb.Remove((sb.Length - 2), 2);
            }

            string matchupDetails = sb.ToString();

            // TODO - PRIORITY - functionality - investigate why Entries only has 1 MatchupEntry per matchup in round 1
            // break point on the return and look at tournament after loading

            return !string.IsNullOrWhiteSpace(matchupDetails) ? matchupDetails : "Warning! Matchup Entries has an error.";
        }
    }
}
