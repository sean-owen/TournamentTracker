using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerBLP;
using TrackerBLP.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private List<List<Matchup>> rounds = new List<List<Matchup>>();

        private Tournament openedTournament = null;

        public TournamentViewerForm(Tournament selectedTourn)
        {
            InitializeComponent();

            this.rounds = selectedTourn.Rounds;
            this.openedTournament = selectedTourn;
            this.InitializeRoundsListBox(selectedTourn);
            this.InitializeMatchupListBox();
        }

        private void InitializeMatchupListBox(bool showUnplayedOnly = false)
        {

            matchupListBox.Items.Clear();

            if (roundValuesListBox.SelectedItem != null)
            {
                int.TryParse(roundValuesListBox.SelectedItem.ToString(), out int roundToDisplay);

                foreach (List<Matchup> round in this.rounds)
                {
                    if (round.FirstOrDefault().MatchupRound == roundToDisplay)
                    {
                        foreach (Matchup matchup in round)
                        {
                            if (showUnplayedOnly)
                            {
                                if (matchup.Winner == null)
                                {
                                    matchupListBox.Items.Add(matchup);
                                }
                            }
                            else
                            {
                                if (matchup.Winner != null)
                                {
                                    matchup.MatchupDetails += $"-> Winner = {matchup.Winner.TeamName}";
                                }
                                matchupListBox.Items.Add(matchup);
                            }
                        }
                    }
                }
            }
        }

        private void InitializeRoundsListBox(Tournament tournament)
        {
            roundValuesListBox.Items.Clear();

            if (tournament.Rounds.Any())
            {
                var myList = new List<int>();
                foreach (List<Matchup> round in tournament.Rounds)
                {
                    myList.Add(round.FirstOrDefault().MatchupRound);
                    roundValuesListBox.Items.Add(round.FirstOrDefault().MatchupRound.ToString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (matchupListBox.SelectedItem != null)
            {
                var matchupItem = (Matchup)matchupListBox.SelectedItem;

                teamOneLabel.Text = matchupItem.Entries.First().TeamCompeting.TeamName;
                teamTwoLabel.Text = matchupItem.Entries.Last().TeamCompeting.TeamName;
            }
            else
            {
                teamOneLabel.Text = "<team one>";
                teamTwoLabel.Text = "<team two>";
            }

        }

        private void submitScoreButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                Matchup matchupItem = (Matchup)matchupListBox.SelectedItem;
                matchupItem.Entries.First().Score = double.Parse(teamOneScoreBox.Text);
                matchupItem.Entries.Last().Score = double.Parse(teamTwoScoreBox.Text);

                this.UpdateTournament(matchupItem);
                Tuple<bool, bool> statusUpdate = TournamentLogic.ScoreMatchup(this.openedTournament, matchupItem);
                bool isRoundOver = statusUpdate.Item1;
                bool isTournamentOver = statusUpdate.Item2;


                submitScoreValidationLabel.Text = "Success!";
                submitScoreValidationLabel.ForeColor = Color.Green;

                if (isTournamentOver)
                {
                    Team winner = matchupItem.Entries.First().Score > matchupItem.Entries.Last().Score ? matchupItem.Entries.First().TeamCompeting : matchupItem.Entries.Last().TeamCompeting;
                    string tournamentOverMessage = $"This tournament is now complete! { winner.TeamName } has won the tournament.";
                    MessageBox.Show(tournamentOverMessage);
                    this.Close();
                }
                else if (isRoundOver)
                {
                    MessageBox.Show("This round is now complete!");
                }
            }
            else
            {
                submitScoreValidationLabel.Text = "Error! Please enter valid parameters.";
                submitScoreValidationLabel.ForeColor = Color.Red;
                CommonActions.LabelErrorAnimation(submitScoreValidationLabel);
            }
        }

        private void UpdateTournament(Matchup updatedMatchup)
        {
            foreach (List<Matchup> round in this.openedTournament.Rounds)
            {
                var oldMatchup = round.FirstOrDefault(x => x.Id == updatedMatchup.Id);
                if (oldMatchup != null)
                {
                    round.Remove(oldMatchup);
                    round.Add(updatedMatchup);
                }
            }
        }

        private bool ValidateForm()
        {
            bool validMatchup = matchupListBox.SelectedItem != null;
            bool validT1Score = double.TryParse(teamOneScoreBox.Text, out _);
            bool validT2Score = double.TryParse(teamTwoScoreBox.Text, out _);

            bool matchupAlreadyScored = false;
            if (validMatchup)
            {
                var selectedMatchup = (Matchup)matchupListBox.SelectedItem;
                if (selectedMatchup.Winner != null)
                {
                    DialogResult response = MessageBox.Show("This Matchup has already been scored. Are you sure you want to update the scores?", "Warning", MessageBoxButtons.YesNo);
                    matchupAlreadyScored = response == DialogResult.Yes ? false : true;
                }
            }

            return validMatchup && validT1Score && validT2Score && !matchupAlreadyScored;
        }

        private void roundValuesListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.InitializeMatchupListBox();
        }

        private void unplayedFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (unplayedFilterCheckBox.Checked)
            {
                InitializeMatchupListBox(showUnplayedOnly: true);
            }
            else
            {
                InitializeMatchupListBox();
            }
        }
    }
}
