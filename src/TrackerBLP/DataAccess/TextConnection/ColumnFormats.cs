using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("UnitTest.TackerBLP")]
namespace TrackerBLP.DataAccess.TextConnectorExtensions
{
    internal struct ColumnFormats
    {
        public const int modelId = 0;

        internal struct Prize
        {
            public const int Id = modelId,
            PlaceNumber = 1,
            PlaceName = 2,
            PrizeAmount = 3,
            PrizePercentage = 4;
        }

        internal struct Person
        {
            public const int Id = modelId,
            FirstName = 1,
            LastName = 2,
            EmailAddress = 3,
            PhoneNumber = 4;
        }

        internal struct Team
        {
            public const int Id = modelId,
            TeamMembersIds = 1,
            TeamName = 2;
        }

        internal struct Tournament
        {
            public const int Id = modelId,
            TournamentName = 1,
            EntryFee = 2,
            EnteredTeams = 3,
            Prizes = 4,
            Rounds = 5;
        }

        internal struct Matchup
        {
            public const int Id = modelId,
            Entries = 1,
            Winner = 2,
            MatchupRound = 3;
        }

        internal struct MatchupEntry
        {
            public const int Id = modelId,
            TeamCompeting = 1,
            Score = 2,
            ParentMatchup = 3;
        }
    }
}
