using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerBLP;
using TrackerBLP.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                formSuccessFeedbackLabel.Text = "Success!";
                formSuccessFeedbackLabel.ForeColor = Color.Green;

                Prize prizeModel = new Prize(
                    placeNameTextBox.Text,
                    placeNumberTextBox.Text,
                    prizeAmountTextBox.Text,
                    prizePercentageTextBox.Text);
               
                GlobalConfig.Connection.CreatePrize(prizeModel);
              
            }
            else
            {
                formSuccessFeedbackLabel.Text = "Error! Please enter valid parameters.";
                formSuccessFeedbackLabel.ForeColor = Color.Red;
            }
        }

        private bool ValidateForm()
        {
            bool placeNumberValid = this.ValidatePlaceNumber();
            bool placeNameValid = this.ValidatePlaceName();
            bool prizeAmountValid = this.ValidatePrizeAmount();
            bool prizePercentageValid = this.ValidatePrizePercentage();

            return placeNumberValid && placeNameValid && (prizeAmountValid ^ prizePercentageValid);          
        }

        private bool ValidatePlaceName()
        {
            bool output = true;
            if (placeNameTextBox.Text == string.Empty)
            {
                output = false;
            }

            return output;
        }

        private bool ValidatePrizePercentage()
        {
            bool output = true;
            bool prizePercentageValid = double.TryParse(prizePercentageTextBox.Text, out double prizePercentage);
            if (prizePercentageValid)
            {
                if (prizePercentage < 0 || prizePercentage > 100)
                {
                    output = false;
                }
            }
            else 
            {
                output = false;
            }

            return output;
        }

        private bool ValidatePrizeAmount()
        {
            bool output = true;
            decimal.TryParse(prizeAmountTextBox.Text, out decimal prizeAmount);      
            if (prizeAmount <= 0)
            {
                output = false;
            }

            return output;
        }

        private bool ValidatePlaceNumber()
        {
            bool output = true;
            bool placeNumberValidNumber = int.TryParse(placeNumberTextBox.Text, out int placeNumber);

            if (!placeNumberValidNumber)
            {
                output = false;
            }
            if (placeNumber < 1)
            {
                output = false;
            }
            if (placeNameTextBox.Text.Length < 1)
            {
                output = false;
            }

            return output;
        }
    }
}
