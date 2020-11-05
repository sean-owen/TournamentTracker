using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TrackerBLP.DataAccess.TextConnectorExtensions;
using TrackerBLP.Models;

namespace TrackerBLP.DataAccess.TextConnection.ExtensionMethods
{
    public static class PrizeExtensions
    {
        public static List<Prize> ConvertToPrizes(this List<string> lines)
        {
            List<Prize> prizes = new List<Prize>();

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                Prize prize = new Prize();
                prize.Id = int.Parse(columns[ColumnFormats.Prize.Id]);
                prize.PlaceNumber = int.Parse(columns[ColumnFormats.Prize.PlaceNumber]);
                prize.PlaceName = columns[ColumnFormats.Prize.PlaceName];
                prize.PrizeAmount = decimal.Parse(columns[ColumnFormats.Prize.PrizeAmount]);
                prize.PrizePercentage = double.Parse(columns[ColumnFormats.Prize.PrizePercentage]);

                prizes.Add(prize);
            }

            return prizes;
        }

        public static void SaveToPrizeFile(this List<Prize> prizes)
        {
            List<string> lines = new List<string>();

            foreach (Prize prize in prizes)
            {
                lines.Add($"{ prize.Id },{ prize.PlaceNumber },{ prize.PlaceName },{ prize.PrizeAmount },{ prize.PrizePercentage }");
            }

            File.WriteAllLines(GlobalConfig.PrizesFile.FullFilePath(), lines);
        }
    }
}
