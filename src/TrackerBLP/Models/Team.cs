using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Team
    {
        public int Id { get; set; }
        public List<Person> TeamMembers { get; set; }
        public string TeamName { get; set; }
    }
}
