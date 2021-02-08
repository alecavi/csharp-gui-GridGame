using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{

    public class VictoryPanel : Panel
    {
        private GameForm parentForm;
        private static readonly int BUTTON_WIDTH = 200;
        private static readonly int BUTTON_HEIGHT = 50;

        public VictoryPanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "VictoryPanel";

            var playAgainButton = new Button();
            playAgainButton.SetBounds((Width / 2) - BUTTON_WIDTH, Height - 100, BUTTON_WIDTH, BUTTON_HEIGHT);
            playAgainButton.Text = "Play Again";
            playAgainButton.Click += new EventHandler((sender, e) => parentForm.GamePanel.SwitchTo(parentForm.GamePanel.Players));
            Controls.Add(playAgainButton);

            var mainMenuButton = new Button();
            mainMenuButton.SetBounds(Width / 2, Height - 100, BUTTON_WIDTH, BUTTON_HEIGHT);
            mainMenuButton.Text = "Main Menu";
            mainMenuButton.Click += new EventHandler((sender, e) => parentForm.MainMenuPanel.SwitchTo());
            
            Controls.Add(mainMenuButton);
        }

        public void SwitchTo()
        {
            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }
    }
}
