using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Team
    {
        /// <summary>
        /// The database ID of this team.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The people in this team.
        /// </summary>
        public List<Person> TeamMembers { get; set; }

        /// <summary>
        /// The name of this team.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Returns the TeamName if it has been set, otherwise an error message.
        /// </summary>
        /// <returns>TeamName if it has been set, otherwise an error message.</returns>
        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(TeamName) ? TeamName : "Error! This Teams' name is not set.";
        }
    }
}
