using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    class MySqlConnection : IDataConnection
    {
        // TODO - create method that actually saves to the database
        public Prize CreatePrize(Prize model)
        {
            model.Id = 1;
            return model;
        }
    }
}
