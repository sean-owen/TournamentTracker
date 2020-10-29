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
    public partial class CreateTournamentForm : Form
    {
        List<Team> existingTeams = new List<Team>();
        
        // TODO - Test wiring up of this form.
        public CreateTournamentForm()
        {
            InitializeComponent();
            this.InitializeSelectTeamListBox();
            this.InitializeViewPrizesListBox();
        }

        private void InitializeSelectTeamListBox()
        {
            existingTeams.Clear();
            selectTeamListBox.Items.Clear();

            var teams = GlobalConfig.Connection.LoadTeams();
            foreach (var team in teams)
            {
                selectTeamListBox.Items.Add(team);
                existingTeams.Add(team);
            }
        }

        private void InitializeViewPrizesListBox()
        {
            viewPrizesListBox.Items.Clear();

            var prizes = GlobalConfig.Connection.LoadPrizes();
            foreach (var prize in prizes)
            {
                viewPrizesListBox.Items.Add(prize);
            }
        }

        private void createNewTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new CreateTeamForm().Show();
            this.InitializeSelectTeamListBox();
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            viewTeamsListBox.Items.Add(selectTeamListBox.SelectedItem);
            selectTeamListBox.Items.Remove(selectTeamListBox.SelectedItem);
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            new CreatePrizeForm().Show();
            this.InitializeViewPrizesListBox();
        }

        private void deleteTeamButton_Click(object sender, EventArgs e)
        {
            selectTeamListBox.Items.Add(viewTeamsListBox.SelectedItem);
            viewTeamsListBox.Items.Remove(viewTeamsListBox.SelectedItem);
        }

        private void deletePrizeButton_Click(object sender, EventArgs e)
        {
            Prize prize = (Prize)viewPrizesListBox.SelectedItem;
            GlobalConfig.Connection.DeletePrize(prize);
            viewPrizesListBox.Items.Remove(viewPrizesListBox.SelectedItem);
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                Tournament tournament = new Tournament();
                tournament.TournamentName = tournamentNameTextBox.Text;
                tournament.EntryFee = decimal.Parse(entryFeeTextBox.Text);

                var enteredTeams = new List<Team>();
                foreach(var item in viewTeamsListBox.Items)
                {
                    enteredTeams.Add((Team)item);
                }
                tournament.EnteredTeams = enteredTeams;

                var prizes = new List<Prize>();
                foreach (var item in viewPrizesListBox.Items)
                {
                    prizes.Add((Prize)item);
                }
                tournament.Prizes = prizes;

                GlobalConfig.Connection.CreateTournament(tournament);
            }
        }

        private bool ValidateForm()
        {
            // TODO - implement validation
            return true;
        }
    }
}
