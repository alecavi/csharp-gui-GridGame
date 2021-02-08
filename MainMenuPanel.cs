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
        private static readonly int BUTTON_WIDTH = 100;
        private static readonly int BUTTON_HEIGHT = 20;

        public MainMenuPanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "Main Menu";

            var playButton = new Button();
            playButton.SetBounds((Width / 2) - (BUTTON_WIDTH / 2), 50, BUTTON_WIDTH, BUTTON_HEIGHT);
            playButton.Text = "PLAY!";
            playButton.Click += new EventHandler((sender, e) => parentForm.PreGamePanel.SwitchTo());
            Controls.Add(playButton);
        }

        public void SwitchTo()
        {
            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }
    }
}
