using System;
using System.Collections.Generic;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess
{
    public interface IDataConnection
    {
        Prize CreatePrize(Prize model);
    }
}
