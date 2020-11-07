using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackerBLP.Models
{
    public class Matchup
    {
        public Matchup()
        {
            MatchupDetails = this.GetMatchupDetails();
        }
        public int Id { get; set; }
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();
        public Team Winner { get; set; }
        public int MatchupRound { get; set; }
        public string MatchupDetails { get; set; }
        public override string ToString()
        {
            // TODO - functionality - consider this solution, not sure it will work long term...
            MatchupDetails = this.GetMatchupDetails();
            return MatchupDetails;
        }

        private string GetMatchupDetails()
        {
            StringBuilder sb = new StringBuilder();
            if (Entries.Any())
            {
                foreach (MatchupEntry entry in Entries)
                {
                    string teamName = entry.TeamCompeting?.TeamName ?? "Error! Team name missing.";
                    sb.Append($"{teamName} vs ");
                }
                sb.Remove((sb.Length - 3), 3);
            }

            MatchupDetails = sb.ToString();

            return !string.IsNullOrWhiteSpace(MatchupDetails) ? MatchupDetails : "Warning! Matchup Entries has an error.";
        }
    }    
}
