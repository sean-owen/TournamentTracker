using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.DataAccess.TextConnection.ExtensionMethods;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnectorExtensions
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        {
            //return @$"{GlobalConfig.TextFileOutputFolderPath}\{fileName}";
            return Path.Combine(GlobalConfig.TextFileOutputFolderPath, fileName);
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            string[] x = File.ReadAllLines(file);


            return x.ToList();
        }
    }
}
