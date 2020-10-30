using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackerBLP;
using TrackerBLP.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<Person> newTeam = new List<Person>();
        private List<Person> existingPeople = new List<Person>();

        public CreateTeamForm()
        {
            InitializeComponent();
            // Load people from database
            var people = GlobalConfig.Connection.LoadPeople();
            // Add people to combo box
            foreach(var person in people)
            {
                selectTeamMemberListBox.Items.Add(person);
                existingPeople.Add(person);
            }            
        }        

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                Team team = new Team();
                team.TeamName = teamNameTextBox.Text;
                team.TeamMembers = this.newTeam;

                GlobalConfig.Connection.CreateTeam(team);
            }
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateSubForm())
            {
                Person person = new Person();

                person.FirstName = firstNameTextBox.Text;
                person.LastName = lastNameTextBox.Text;
                person.EmailAddress = emailTextBox.Text;
                person.PhoneNumber = mobileTextBox.Text;

                person.Id = GlobalConfig.Connection.CreatePerson(person).Id;
                selectTeamMemberListBox.Items.Add(person);
                this.existingPeople.Add(person);
            }
            else
            {
                // TODO - functionality - give user feedback about form completion
                // formSuccessFeedbackLabel.Text = "Error! Please enter valid parameters.";
                // formSuccessFeedbackLabel.ForeColor = Color.Red;
            }

        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            viewTeamMembersListBox.Items.Add(selectTeamMemberListBox.SelectedItem);
            this.newTeam.Add(this.existingPeople.FirstOrDefault(x => x.FullName == selectTeamMemberListBox.SelectedItem.ToString()));
            this.existingPeople.Remove(this.existingPeople.FirstOrDefault(x => x.FullName == selectTeamMemberListBox.SelectedItem.ToString()));
            selectTeamMemberListBox.Items.Remove(selectTeamMemberListBox.SelectedItem);
        }

        private void deleteTeamMemberButton_Click(object sender, EventArgs e)
        {
            selectTeamMemberListBox.Items.Add(viewTeamMembersListBox.SelectedItem);
            this.existingPeople.Add(this.newTeam.FirstOrDefault(x => x.FullName == viewTeamMembersListBox.SelectedItem.ToString()));
            this.newTeam.Remove(this.newTeam.FirstOrDefault(x => x.FullName == viewTeamMembersListBox.SelectedItem.ToString()));
            viewTeamMembersListBox.Items.Remove(viewTeamMembersListBox.SelectedItem);
        }

        private bool ValidateForm()
        {
            // TODO - backlog - functionality - add validation

            return true;
        }

        private bool ValidateSubForm()
        {
            // TODO - backlog - functionality - add validation

            // Check team member with the same name does not already exist?

            return true;
        }        
    }
}
