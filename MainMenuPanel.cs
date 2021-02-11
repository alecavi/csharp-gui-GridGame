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
        private GameForm parentForm;
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

            var quitButton = new Button
            {
                Text = "Quit",
                Location = new Point(playButton.Location.X, playButton.Location.Y + playButton.Height),
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
    }
}
