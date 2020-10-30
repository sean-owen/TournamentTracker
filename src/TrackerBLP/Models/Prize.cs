using System;

namespace TrackerBLP.Models
{
    public class Prize
    {
        public Prize()
        {
        }

        /// <summary>
        /// Converts input parameters from strings to their required type and initializes prize model class properties.
        /// </summary>
        /// <param name="placeName">The place name for this prize.</param>
        /// <param name="placeNumber">The place number for this prize.</param>
        /// <param name="prizeAmount">The amount of currency provided by winning this prize.</param>
        /// <param name="prizePercentage">The percent of the tournament cashflow to be provided by winning this prize.</param>
        public Prize(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            this.PlaceName = placeName;

            int.TryParse(placeNumber, out int placeNumberValue);
            this.PlaceNumber = placeNumberValue;

            decimal.TryParse(prizeAmount, out decimal prizeAmountValue);
            this.PrizeAmount = prizeAmountValue;

            double.TryParse(prizePercentage, out double prizePercentageValue);
            this.PrizePercentage = prizePercentageValue;
        }

        /// <summary>
        /// The Id for this prize.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The place number for this prize.
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// The place name for this prize.
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// The amount of currency provided by winning this prize.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// The percent of the tournament cashflow to be provided by winning this prize.
        /// </summary>
        public double PrizePercentage { get; set; }

        /// <summary>
        /// Checks the required properties are set and returns a string containing the place name and the prize value.
        /// The prize value is either a fixed amount or a percentage, depending on which property is set.
        /// Prize amount and prize percentage cannot both be greater than zero or an error will be thrown.
        /// </summary>
        /// <returns>Place name and value for this prize as a string.</returns>
        public override string ToString()
        {
            // TODO - consider moving validation to a private method to make this method more easily readable
            if(this.PlaceName == string.Empty)
            {
                throw new Exception("Error! PlaceName is not set.");
            }

            bool validPrizeAmount = this.PrizeAmount != default;
            bool validPrizePercentage = this.PrizePercentage != default;

            if(validPrizeAmount && validPrizePercentage)
            {
                throw new Exception("Error! PrizeAmount and PrizePercentage both have a value greater than 0.");
            }
            else if (validPrizeAmount)
            {
                return $"{this.PlaceName} = {this.PrizeAmount} GBP";
            }
            else if (validPrizePercentage)
            {
                return $"{this.PlaceName} = {this.PrizePercentage}%";
            }
            else
            {
                throw new Exception("Error! PrizeAmount and PrizePercentage both have a value of 0.");
            }

        }
    }
}
