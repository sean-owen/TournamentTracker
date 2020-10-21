namespace TrackerUI
{
    partial class TournamentViewerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.headerValue = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.roundValuesListBox = new System.Windows.Forms.ComboBox();
            this.unplayedFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.matchupListBox = new System.Windows.Forms.ListBox();
            this.teamOneLabel = new System.Windows.Forms.Label();
            this.teamOneScoreLabel = new System.Windows.Forms.Label();
            this.teamOneScoreBox = new System.Windows.Forms.TextBox();
            this.teamTwoScoreLabel = new System.Windows.Forms.Label();
            this.teamTwoScoreBox = new System.Windows.Forms.TextBox();
            this.teamTwoLabel = new System.Windows.Forms.Label();
            this.vsLabel = new System.Windows.Forms.Label();
            this.submitScoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.headerLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.headerLabel.Location = new System.Drawing.Point(12, 14);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(214, 50);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Tournament:";
            // 
            // headerValue
            // 
            this.headerValue.AutoSize = true;
            this.headerValue.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.headerValue.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.headerValue.Location = new System.Drawing.Point(213, 14);
            this.headerValue.Name = "headerValue";
            this.headerValue.Size = new System.Drawing.Size(150, 50);
            this.headerValue.TabIndex = 0;
            this.headerValue.Text = "<none>\r\n";
            // 
            // roundLabel
            // 
            this.roundLabel.AutoSize = true;
            this.roundLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.roundLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.roundLabel.Location = new System.Drawing.Point(12, 81);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(73, 30);
            this.roundLabel.TabIndex = 0;
            this.roundLabel.Text = "Round";
            // 
            // roundValuesListBox
            // 
            this.roundValuesListBox.BackColor = System.Drawing.Color.White;
            this.roundValuesListBox.FormattingEnabled = true;
            this.roundValuesListBox.Location = new System.Drawing.Point(91, 88);
            this.roundValuesListBox.Name = "roundValuesListBox";
            this.roundValuesListBox.Size = new System.Drawing.Size(170, 23);
            this.roundValuesListBox.TabIndex = 1;
            // 
            // unplayedFilterCheckBox
            // 
            this.unplayedFilterCheckBox.AutoSize = true;
            this.unplayedFilterCheckBox.Location = new System.Drawing.Point(91, 117);
            this.unplayedFilterCheckBox.Name = "unplayedFilterCheckBox";
            this.unplayedFilterCheckBox.Size = new System.Drawing.Size(102, 19);
            this.unplayedFilterCheckBox.TabIndex = 2;
            this.unplayedFilterCheckBox.Text = "Unplayed only";
            this.unplayedFilterCheckBox.UseVisualStyleBackColor = true;
            // 
            // matchupListBox
            // 
            this.matchupListBox.FormattingEnabled = true;
            this.matchupListBox.ItemHeight = 15;
            this.matchupListBox.Location = new System.Drawing.Point(12, 153);
            this.matchupListBox.Name = "matchupListBox";
            this.matchupListBox.Size = new System.Drawing.Size(249, 289);
            this.matchupListBox.TabIndex = 3;
            this.matchupListBox.SelectedIndexChanged += new System.EventHandler(this.matchupListBox_SelectedIndexChanged);
            // 
            // teamOneLabel
            // 
            this.teamOneLabel.AutoSize = true;
            this.teamOneLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamOneLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamOneLabel.Location = new System.Drawing.Point(343, 159);
            this.teamOneLabel.Name = "teamOneLabel";
            this.teamOneLabel.Size = new System.Drawing.Size(129, 30);
            this.teamOneLabel.TabIndex = 0;
            this.teamOneLabel.Text = "<team one>";
            // 
            // teamOneScoreLabel
            // 
            this.teamOneScoreLabel.AutoSize = true;
            this.teamOneScoreLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamOneScoreLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamOneScoreLabel.Location = new System.Drawing.Point(328, 189);
            this.teamOneScoreLabel.Name = "teamOneScoreLabel";
            this.teamOneScoreLabel.Size = new System.Drawing.Size(64, 30);
            this.teamOneScoreLabel.TabIndex = 0;
            this.teamOneScoreLabel.Text = "Score";
            // 
            // teamOneScoreBox
            // 
            this.teamOneScoreBox.Location = new System.Drawing.Point(398, 198);
            this.teamOneScoreBox.Name = "teamOneScoreBox";
            this.teamOneScoreBox.Size = new System.Drawing.Size(83, 23);
            this.teamOneScoreBox.TabIndex = 4;
            // 
            // teamTwoScoreLabel
            // 
            this.teamTwoScoreLabel.AutoSize = true;
            this.teamTwoScoreLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamTwoScoreLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamTwoScoreLabel.Location = new System.Drawing.Point(328, 311);
            this.teamTwoScoreLabel.Name = "teamTwoScoreLabel";
            this.teamTwoScoreLabel.Size = new System.Drawing.Size(64, 30);
            this.teamTwoScoreLabel.TabIndex = 0;
            this.teamTwoScoreLabel.Text = "Score";
            // 
            // teamTwoScoreBox
            // 
            this.teamTwoScoreBox.Location = new System.Drawing.Point(398, 320);
            this.teamTwoScoreBox.Name = "teamTwoScoreBox";
            this.teamTwoScoreBox.Size = new System.Drawing.Size(83, 23);
            this.teamTwoScoreBox.TabIndex = 4;
            // 
            // teamTwoLabel
            // 
            this.teamTwoLabel.AutoSize = true;
            this.teamTwoLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamTwoLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamTwoLabel.Location = new System.Drawing.Point(343, 281);
            this.teamTwoLabel.Name = "teamTwoLabel";
            this.teamTwoLabel.Size = new System.Drawing.Size(128, 30);
            this.teamTwoLabel.TabIndex = 0;
            this.teamTwoLabel.Text = "<team two>";
            // 
            // vsLabel
            // 
            this.vsLabel.AutoSize = true;
            this.vsLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.vsLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.vsLabel.Location = new System.Drawing.Point(398, 240);
            this.vsLabel.Name = "vsLabel";
            this.vsLabel.Size = new System.Drawing.Size(53, 30);
            this.vsLabel.TabIndex = 0;
            this.vsLabel.Text = "-VS-";
            // 
            // submitScoreButton
            // 
            this.submitScoreButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.submitScoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitScoreButton.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.submitScoreButton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.submitScoreButton.Location = new System.Drawing.Point(508, 234);
            this.submitScoreButton.Name = "submitScoreButton";
            this.submitScoreButton.Size = new System.Drawing.Size(119, 50);
            this.submitScoreButton.TabIndex = 5;
            this.submitScoreButton.Text = "Submit";
            this.submitScoreButton.UseVisualStyleBackColor = true;
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 450);
            this.Controls.Add(this.submitScoreButton);
            this.Controls.Add(this.vsLabel);
            this.Controls.Add(this.teamTwoLabel);
            this.Controls.Add(this.teamTwoScoreBox);
            this.Controls.Add(this.teamTwoScoreLabel);
            this.Controls.Add(this.teamOneScoreBox);
            this.Controls.Add(this.teamOneScoreLabel);
            this.Controls.Add(this.teamOneLabel);
            this.Controls.Add(this.matchupListBox);
            this.Controls.Add(this.unplayedFilterCheckBox);
            this.Controls.Add(this.roundValuesListBox);
            this.Controls.Add(this.roundLabel);
            this.Controls.Add(this.headerValue);
            this.Controls.Add(this.headerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TournamentViewerForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label headerValue;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.ComboBox roundValuesListBox;
        private System.Windows.Forms.CheckBox unplayedFilterCheckBox;
        private System.Windows.Forms.ListBox matchupListBox;
        private System.Windows.Forms.Label teamOneLabel;
        private System.Windows.Forms.Label teamOneScoreLabel;
        private System.Windows.Forms.TextBox teamOneScoreBox;
        private System.Windows.Forms.Label teamTwoScoreLabel;
        private System.Windows.Forms.TextBox teamTwoScoreBox;
        private System.Windows.Forms.Label teamTwoLabel;
        private System.Windows.Forms.Label vsLabel;
        private System.Windows.Forms.Button submitScoreButton;
    }
}

