using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GridGame
{

    public class VictoryPanel : Panel
    {
        private GameForm parentForm;
        private static readonly int BUTTON_WIDTH = 200;
        private static readonly int BUTTON_HEIGHT = 50;

        [Serializable()]
        public class HighScore
        {
            public int Score { get; set; }
            public string Initials { get; set; }
        }

        public List<HighScore> _highScores = new List<HighScore>();

        public VictoryPanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "VictoryPanel";

            var score = new HighScore() { Score = 100, Initials = "MAJ" };
            _highScores.Add(score);
            var serializer = new XmlSerializer(_highScores.GetType(), "HighScores.Scores");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, _highScores);
            }


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

            var showScoreButton = new Button();
            showScoreButton.SetBounds(Width / 2, Height - 100, BUTTON_WIDTH, BUTTON_HEIGHT);
            showScoreButton.Text = "Show Score";
            showScoreButton.Click += new EventHandler((sender, e) => parentForm.MainMenuPanel.SwitchTo());
            Controls.Add(showScoreButton);
        }

        public void SwitchTo()
        {
            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }
    }
}
