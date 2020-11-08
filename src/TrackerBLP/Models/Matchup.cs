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

        /// <summary>
        /// The database ID of this matchup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The teams competing in this matchup.
        /// </summary>
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();

        /// <summary>
        /// The team that won this matchup.
        /// </summary>
        public Team Winner { get; set; }

        /// <summary>
        /// The round this matchup will be played in.
        /// </summary>
        public int MatchupRound { get; set; }

        /// <summary>
        /// The teams facing off in this matchup.
        /// </summary>
        public string MatchupDetails { get; set; }

        /// <summary>
        /// Returns this instance' MatchupDetails if valid, otherwise an error message.
        /// </summary>
        /// <returns>MatchupDetails if valid, otherwise an error message.</returns>
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
