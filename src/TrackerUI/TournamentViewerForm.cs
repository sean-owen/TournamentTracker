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
        private List<List<Matchup>> loadedMatchups = new List<List<Matchup>>();
        public TournamentViewerForm(Tournament selectedTourn)
        {
            InitializeComponent();

            loadedMatchups = selectedTourn.Rounds;

            this.InitializeRoundsListBox(selectedTourn);
            this.InitializeMatchupListBox();
        }

        private void InitializeMatchupListBox(bool showUnplayedOnly = false)
        {

            matchupListBox.Items.Clear();

            if (roundValuesListBox.SelectedItem != null)
            {
                int.TryParse(roundValuesListBox.SelectedItem.ToString(), out int roundToDisplay);
                
                foreach (List<Matchup> matchups in loadedMatchups)
                {
                    if (matchups.FirstOrDefault().MatchupRound == roundToDisplay)
                    {
                        foreach (Matchup matchup in matchups)
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

            var x = roundValuesListBox.Items;
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
            MessageBox.Show("test");
            // TODO - functionality - get submit score button working

            // Validate scores & matchupListBoxSelectedItem
            if (matchupListBox.SelectedItem != null)
            {
                var matchupItem = (Matchup)matchupListBox.SelectedItem;
                matchupItem.Entries.First().Score = double.Parse(teamOneScoreBox.Text);
                matchupItem.Entries.Last().Score = double.Parse(teamTwoScoreBox.Text);

                matchupItem.Winner = (matchupItem.Entries.First().Score > matchupItem.Entries.Last().Score) ? matchupItem.Entries.First().TeamCompeting : matchupItem.Entries.Last().TeamCompeting;

                GlobalConfig.Connection.UpdateMatchup(matchupItem);
            }
            else
            {
                // user feedback
            }
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
