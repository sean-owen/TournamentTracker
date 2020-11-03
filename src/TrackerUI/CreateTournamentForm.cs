using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TrackerBLP;
using TrackerBLP.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form
    {
        List<Team> selectTeamList = new List<Team>();
        List<Team> viewTeamsList = new List<Team>();

        // TODO - functionality - Test wiring up of this form.
        public CreateTournamentForm()
        {
            InitializeComponent();
            this.InitializeSelectTeamListBox();
            this.InitializeViewPrizesListBox();
            this.viewTeamsList = new List<Team>();
        }

        private void createNewTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new CreateTeamForm().ShowDialog();
            this.InitializeSelectTeamListBox();
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            Team selectedItem = (Team)selectTeamListBox.SelectedItem;

            selectTeamListBox.Items.Remove(selectTeamListBox.SelectedItem);
            viewTeamsListBox.Items.Add(selectedItem);

            viewTeamsList.Add(selectedItem);
            selectTeamList.Remove(selectedItem);
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            new CreatePrizeForm().ShowDialog();
            this.InitializeViewPrizesListBox();
        }

        private void deleteTeamButton_Click(object sender, EventArgs e)
        {
            Team selectedItem = (Team)viewTeamsListBox.SelectedItem;

            viewTeamsListBox.Items.Remove(viewTeamsListBox.SelectedItem);
            selectTeamListBox.Items.Add(selectedItem);

            viewTeamsList.Remove(selectedItem);
            selectTeamList.Add(selectedItem);
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
                createTournamentValidationLabel.Text = "Success!";
                createTournamentValidationLabel.ForeColor = Color.Green;

                Tournament tournament = new Tournament();
                tournament.TournamentName = tournamentNameTextBox.Text;
                tournament.EntryFee = decimal.Parse(entryFeeTextBox.Text);

                var enteredTeams = new List<Team>();
                foreach (var item in viewTeamsListBox.Items)
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


                TournamentLogic.CreateRounds(tournament);
                GlobalConfig.Connection.CreateTournament(tournament);
            }
            else
            {
                createTournamentValidationLabel.Text = "Error! Please enter valid parameters.";
                createTournamentValidationLabel.ForeColor = Color.Red;

                CommonActions.LabelErrorAnimation(createTournamentValidationLabel);
            }
        }

        private bool ValidateForm()
        {
            // TODO - backlog - functionality - implement validation
            bool validTournamentName = !string.IsNullOrWhiteSpace(tournamentNameTextBox.Text);
            // TODO - functionality - add warning if entry fee is 0
            bool validNumberOfTeams = viewTeamsListBox.Items.Count > 1;
            bool atLeastOnePrize = viewPrizesListBox.Items.Count > 0;
            // TODO - functionality - add warning if no prizes

            return validTournamentName && validNumberOfTeams && atLeastOnePrize;
        }

        private void InitializeSelectTeamListBox()
        {
            selectTeamList.Clear();
            selectTeamListBox.Items.Clear();

            var teams = GlobalConfig.Connection.LoadTeams();
            foreach (var team in teams)
            {
                if (!viewTeamsList.Contains(team))
                {
                    selectTeamListBox.Items.Add(team);
                    selectTeamList.Add(team);
                }
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
    }
}
