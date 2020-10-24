using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    class MySqlConnector : IDataConnection
    {
        public Prize CreatePerson(Person model)
        {
            throw new NotImplementedException();
        }

        // TODO - create method that actually saves to the database
        public Prize CreatePrize(Prize model)
        {
            throw new NotImplementedException();
        }
    }
}
