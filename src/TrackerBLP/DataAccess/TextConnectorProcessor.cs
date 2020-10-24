using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnectorExtensions
{
    public static class TextConnectorProcessor
    {
        private struct ColumnFormat
        {
            public const int Id = 0,
            PlaceNumber = 1,
            PlaceName = 2,
            PrizeAmount = 3,
            PrizePercentage = 4;
        }

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

            return File.ReadAllLines(file).ToList();
        }

        public static List<Prize> ConvertToPrizes(this List<string> lines)
        {
            List<Prize> prizes = new List<Prize>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Prize prize = new Prize();
                prize.Id = int.Parse(columns[ColumnFormat.Id]);
                prize.PlaceNumber = int.Parse(columns[ColumnFormat.PlaceNumber]);
                prize.PlaceName = columns[ColumnFormat.PlaceName];
                prize.PrizeAmount = decimal.Parse(columns[ColumnFormat.PrizeAmount]);
                prize.PrizePercentage = double.Parse(columns[ColumnFormat.PrizePercentage]);

                prizes.Add(prize);
            }

            return prizes;
        }

        public static void SaveToPrizeFile(this List<Prize> prizes, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Prize prize in prizes)
            {
                lines.Add($"{ prize.Id },{ prize.PlaceNumber },{ prize.PlaceName },{ prize.PrizeAmount },{ prize.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
