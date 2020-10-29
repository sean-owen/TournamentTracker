using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("UnitTest.TackerBLP")]
namespace TrackerBLP.DataAccess.TextConnectorExtensions
{
    internal struct ColumnFormats
    {
        internal struct Prize
        {
            public const int Id = 0,
            PlaceNumber = 1,
            PlaceName = 2,
            PrizeAmount = 3,
            PrizePercentage = 4;
        }

        internal struct Person
        {
            public const int Id = 0,
            FirstName = 1,
            LastName = 2,
            EmailAddress = 3,
            PhoneNumber = 4;
        }

        internal struct Team
        {
            public const int Id = 0,
            TeamMembersIds = 1,
            TeamName = 2;
        }
    }
}
