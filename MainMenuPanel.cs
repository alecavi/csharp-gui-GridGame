using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{
    public class MainMenuPanel : Panel
    {
        private readonly GameForm parentForm;
        private static readonly int BUTTON_WIDTH = 200;
        private static readonly int BUTTON_HEIGHT = 50;

        public MainMenuPanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "Main Menu";

            var playButton = new Button
            {
                Text = "Play",
                Location = new Point((Width / 2) - (BUTTON_WIDTH / 2), 50),
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
            };
            playButton.Click += new EventHandler((sender, e) => parentForm.PreGamePanel.SwitchTo());
            Controls.Add(playButton);

            var helpButton = new Button
            {
                Text = "Help",
                Location = new Point(playButton.Location.X, playButton.Location.Y + playButton.Height),
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
            };
            helpButton.Click += new EventHandler((sender, e) => parentForm.MainMenuPanel.Help());
            Controls.Add(helpButton);

            var quitButton = new Button
            {
                Text = "Quit",
                Location = new Point(helpButton.Location.X, helpButton.Location.Y + helpButton.Height),
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
            };
            quitButton.Click += new EventHandler((sender, e) => Application.Exit());
            Controls.Add(quitButton);
        }

        public void SwitchTo()
        {
            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }

        public void Help()
        {

            string message = "The connect 4 game rules are pretty straightforward. The game name says it, what you have to do “connect four” to win the game. After choosing the color of your choice, you have to make a row of four checkers of the same color. The rows can be done vertically, horizontally or diagonally." + Environment.NewLine
                               + Environment.NewLine + "Each turn, the player will get the chance to drop and make the row of his checkers, but sometimes instead of making your row, the player has to stop another opponent from making his 4 checkers row by dropping your color checker in that place." + Environment.NewLine
                               + Environment.NewLine + "The game ends when any players make four checkers of his color in a row, or when all the 42 slots are occupied, in that case, the game ends in a tie.";
            string title = "Rules";
            MessageBox.Show(message, title);

        }
    }
}
