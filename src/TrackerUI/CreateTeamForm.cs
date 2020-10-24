using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerBLP.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        public CreateTeamForm()
        {
            InitializeComponent();
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if(this.ValidateForm()) 
            {
                Person person = new Person();

                person.FirstName = firstNameTextBox.Text;
                person.LastName = lastNameTextBox.Text;
                person.EmailAddress = emailTextBox.Text;
                person.PhoneNumber = mobileTextBox.Text;
            }

        }

        private bool ValidateForm()
        {
            // TODO - add validation

            return true;
        }
    }
}
