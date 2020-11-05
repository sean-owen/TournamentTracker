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
    public partial class TournamentDashboardForm : Form
    {
        public TournamentDashboardForm()
        {
            InitializeComponent();
            this.InitializeExistingTournamentListBox();
        }

        private void InitializeExistingTournamentListBox()
        {
            loadExistingTournamentListBox.Items.Clear();

            var tournies = GlobalConfig.Connection.LoadTournaments();
            foreach (Tournament tournament in tournies)
            {
                loadExistingTournamentListBox.Items.Add(tournament);
            }

        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            new TournamentViewerForm().ShowDialog();
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            new CreateTournamentForm().ShowDialog();
            this.InitializeExistingTournamentListBox();
        }
    }
}
