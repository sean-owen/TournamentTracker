using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TrackerBLP.DataAccess;

namespace TrackerBLP
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; set; } = new List<IDataConnection>();

        public static void InitializeConnections(bool database, bool textFiles)
        {
            if (database)
            {
                // TODO
                MySqlConnection mySql = new MySqlConnection();
                Connections.Add(mySql);

            }
            if (textFiles)
            {
                // TODO
                TextConnection textConnection = new TextConnection();
                Connections.Add(textConnection);
            }
        }

    }
}
