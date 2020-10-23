using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    public class TextConnection : IDataConnection
    {
        // TODO - create method that actually saves to text file
        public Prize CreatePrize(Prize model)
        {
            model.Id = 1;
            return model;
        }
    }
}
