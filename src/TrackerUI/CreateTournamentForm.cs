﻿using System;
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
            // TODO - backlog - should form actually remember what teams have been added rather than resetting?
            this.viewTeamsList = new List<Team>();
            this.InitializeSelectTeamListBox();
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            if (selectTeamListBox.SelectedItem != null)
            {
                Team selectedItem = (Team)selectTeamListBox.SelectedItem;

                selectTeamListBox.Items.Remove(selectTeamListBox.SelectedItem);
                selectTeamListBox.Text = default;

                viewTeamsListBox.Items.Add(selectedItem);

                viewTeamsList.Add(selectedItem);
                selectTeamList.Remove(selectedItem);
            }
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

                // TODO - backlog - make async to stop UI lagging? use event to update validation text when complete
                TournamentLogic.CreateRounds(tournament);
                GlobalConfig.Connection.CreateTournament(tournament);

                // clear down text boxes after creating
                tournamentNameTextBox.Text = default;
                entryFeeTextBox.Text = "0";
                this.InitializeSelectTeamListBox();
                viewTeamsListBox.Items.Clear();
                viewPrizesListBox.SelectedItem = default;

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
            bool validTournamentName = !string.IsNullOrWhiteSpace(tournamentNameTextBox.Text);
            // TODO - backlog - add warning if entry fee is 0 - requires an additional form
            bool validNumberOfTeams = viewTeamsListBox.Items.Count > 1;
            bool atLeastOnePrize = viewPrizesListBox.Items.Count > 0;
            // TODO - backlog - add warning if no prizes - requires an additional form? Or MessageBox.Show("Are you sure you want to blah?")

            return validTournamentName && validNumberOfTeams && atLeastOnePrize;
        }

        private void InitializeSelectTeamListBox()
        {
            selectTeamList.Clear();
            selectTeamListBox.Items.Clear();

            List<Team> teams = GlobalConfig.Connection.LoadTeams();
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
