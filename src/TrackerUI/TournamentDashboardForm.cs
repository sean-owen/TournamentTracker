using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerBLP;

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

        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {

        }
    }
}
