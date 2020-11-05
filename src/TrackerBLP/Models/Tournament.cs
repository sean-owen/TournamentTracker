﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public decimal EntryFee { get; set; }
        public List<Team> EnteredTeams { get; set; } = new List<Team>();
        public List<Prize> Prizes { get; set; } = new List<Prize>();
        public List<List<Matchup>> Rounds { get; set; } = new List<List<Matchup>>();
        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(TournamentName) ? TournamentName : "Error! This Tournaments' name is not set.";
        }
    }
}