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
        /// <summary>
        /// Converts the input string into the full file path used 
        /// for the Tournament Tracker application.
        /// </summary>
        /// <param name="fileName">The string to be converted to a full file path.</param>
        /// <returns>Full file path for the fileName.</returns>
        public static string FullFilePath(this string fileName)
        {
            //return @$"{GlobalConfig.TextFileOutputFolderPath}\{fileName}";
            return Path.Combine(GlobalConfig.TextFileOutputFolderPath, fileName);
        }

        /// <summary>
        /// Takes a full file path passed in as a string parameter and loads the file into a List<string>.
        /// </summary>
        /// <param name="file">The full file path string of a file to Load.</param>
        /// <returns>The file at location string passed in or a new List<string> if that file does not exist.</string></returns>
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
