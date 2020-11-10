using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            this.InitializeSelectTeamMemberListBox();

        }

        private void InitializeSelectTeamMemberListBox()
        {
            selectTeamMemberListBox.Items.Clear();

            // Load people from database
            var people = GlobalConfig.Connection.LoadPeople();
            // Add people to combo box
            foreach (var person in people)
            {
                selectTeamMemberListBox.Items.Add(person);
                existingPeople.Add(person);
            }
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                createTeamValidationLabel.Text = "Success!";
                createTeamValidationLabel.ForeColor = Color.Green;

                Team team = new Team();
                team.TeamName = teamNameTextBox.Text;
                team.TeamMembers = this.newTeam;

                GlobalConfig.Connection.CreateTeam(team);

                teamNameTextBox.Text = default;
                viewTeamMembersListBox.Items.Clear();
                this.InitializeSelectTeamMemberListBox();
            }
            else
            {
                createTeamValidationLabel.Text = "Error! Please enter valid parameters.";
                createTeamValidationLabel.ForeColor = Color.Red;

                CommonActions.LabelErrorAnimation(createTeamValidationLabel);
            }
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateSubForm())
            {
                createMemberValidationLabel.Text = "Success!";
                createMemberValidationLabel.ForeColor = Color.Green;

                Person person = new Person();
                person.FirstName = firstNameTextBox.Text;
                person.LastName = lastNameTextBox.Text;
                person.EmailAddress = emailTextBox.Text;
                person.PhoneNumber = mobileTextBox.Text;

                person.Id = GlobalConfig.Connection.CreatePerson(person).Id;
                selectTeamMemberListBox.Items.Add(person);
                this.existingPeople.Add(person);

                firstNameTextBox.Text = default;
                lastNameTextBox.Text = default;
                emailTextBox.Text = default;
                mobileTextBox.Text = default;
            }
            else
            {
                createMemberValidationLabel.Text = "Error! Please enter valid parameters.";
                createMemberValidationLabel.ForeColor = Color.Red;
            }

        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            if (selectTeamMemberListBox.SelectedItem != null)
            {
                viewTeamMembersListBox.Items.Add(selectTeamMemberListBox.SelectedItem);
                this.newTeam.Add(this.existingPeople.FirstOrDefault(x => x.FullName == selectTeamMemberListBox.SelectedItem.ToString()));
                this.existingPeople.Remove(this.existingPeople.FirstOrDefault(x => x.FullName == selectTeamMemberListBox.SelectedItem.ToString()));
                selectTeamMemberListBox.Items.Remove(selectTeamMemberListBox.SelectedItem);
                selectTeamMemberListBox.Text = default;
            }
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
            bool validTeamName = !string.IsNullOrWhiteSpace(teamNameTextBox.Text);
            bool atLeastOneTeamMember = viewTeamMembersListBox.Items.Count > 0;

            // Check if team members are in another team and warn before allowing to create

            return validTeamName && atLeastOneTeamMember;
        }

        private bool ValidateSubForm()
        {
            bool validFirstName = !string.IsNullOrWhiteSpace(firstNameTextBox.Text);
            bool validLastName = !string.IsNullOrWhiteSpace(lastNameTextBox.Text);

            bool validEmail = this.ValidateEmail();
            bool validateMobileNumber = this.ValidateMobileNumber();

            return validFirstName && validLastName && validEmail && validateMobileNumber;
        }

        private bool ValidateMobileNumber()
        {
            // TODO - backlog - add more rigorous validation of mobile numbers
            return !string.IsNullOrWhiteSpace(mobileTextBox.Text);
        }

        private bool ValidateEmail()
        {
            // TODO - backlog - add more rigorous validation of emails
            return !string.IsNullOrWhiteSpace(emailTextBox.Text);
        }
    }
}
