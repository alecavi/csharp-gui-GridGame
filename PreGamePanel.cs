using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{
    public class PreGamePanel : Panel
    {
        private readonly GameForm parentForm;
        private static readonly int BUTTON_WIDTH = 200;
        private static readonly int BUTTON_HEIGHT = 50;

        private TextBox player1NameTextBox;
        private TextBox player2NameTextBox;

        public PreGamePanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "PreGamePanel";

            InitializeComponent();
        }

        public void SwitchTo()
        {
            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.Label Player1SettingsLabel;
            System.Windows.Forms.Label Player2SettingsLabel;
            System.Windows.Forms.Button confirmButton;
            Player1SettingsLabel = new System.Windows.Forms.Label();
            player1NameTextBox = new System.Windows.Forms.TextBox();
            Player2SettingsLabel = new System.Windows.Forms.Label();
            player2NameTextBox = new System.Windows.Forms.TextBox();
            confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Player1SettingsLabel
            // 
            Player1SettingsLabel.AutoSize = true;
            Player1SettingsLabel.Location = new System.Drawing.Point((Width / 2) - 189, 0);
            Player1SettingsLabel.Name = "Player1SettingsLabel";
            Player1SettingsLabel.Size = new System.Drawing.Size(89, 15);
            Player1SettingsLabel.TabIndex = 0;
            Player1SettingsLabel.Text = "Player 1 settings:";
            // 
            // player1NameTextBox
            // 
            player1NameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            player1NameTextBox.Location = new System.Drawing.Point((Width / 2) - 200, 20);
            player1NameTextBox.Name = "player1NameTextBox";
            player1NameTextBox.Size = new System.Drawing.Size(100, 20);
            player1NameTextBox.TabIndex = 0;
            // 
            // Player2SettingsLabel
            // 
            Player2SettingsLabel.AutoSize = true;
            Player2SettingsLabel.Location = new System.Drawing.Point((Width / 2 ) + 100, 0);
            Player2SettingsLabel.Name = "Player2SettingsLabel";
            Player2SettingsLabel.Size = new System.Drawing.Size(89, 25);
            Player2SettingsLabel.TabIndex = 0;
            Player2SettingsLabel.Text = "Player 2 settings:";
            // 
            // player2NameTextBox
            // 
            player2NameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            player2NameTextBox.Location = new System.Drawing.Point((Width / 2) + 100, 20);
            player2NameTextBox.Name = "player2NameTextBox";
            player2NameTextBox.Size = new System.Drawing.Size(100, 20);
            player2NameTextBox.TabIndex = 0;
            // 
            // ConfirmButton
            // 
            confirmButton.Location = new System.Drawing.Point((Width / 2) - (BUTTON_WIDTH / 2), Height - 100);
            confirmButton.Name = "ConfirmButton";
            confirmButton.Size = new System.Drawing.Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            confirmButton.TabIndex = 0;
            confirmButton.Text = "Confirm";
            confirmButton.Click += new EventHandler(ConfirmButtonHandler);
            confirmButton.UseVisualStyleBackColor = true;
            // 
            // PreGamePanel
            // 
            Controls.Add(Player1SettingsLabel);
            Controls.Add(player1NameTextBox);
            Controls.Add(Player2SettingsLabel);
            Controls.Add(player2NameTextBox);
            Controls.Add(confirmButton);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void ConfirmButtonHandler(object sender, EventArgs e)
        {
            parentForm.GamePanel.SwitchTo(new Player[] { new Player(Color.Red, player1NameTextBox.Text), new Player(Color.Yellow, player2NameTextBox.Text)});
        }
    }

}
