using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{
    public class GamePanel : Panel
    {
        private readonly GameForm parentForm;

        private readonly Button[,] buttons = new Button[7, 6];
        private readonly Button activePlayerButton;
        private readonly Label activePlayerLabel;
        public Player[] Players { get; private set; }
        private int activePlayer;
        private static readonly int BUTTON_WIDTH = 45;
        private static readonly int BUTTON_HEIGHT = 45;
        private static readonly (int, int)[] DIRECTIONS = new (int, int)[] 
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1),
            (1, 1),
            (-1, 1),
            (1, -1),
            (-1, -1),
        };

        //All the following audio files are licensed under CC0 (public domain, or as close to it as the law permits)
        private static readonly SoundPlayer[] PIECE_FALLING_SOUNDS = new SoundPlayer[]
        {
            new SoundPlayer(Properties.Resources.PieceFalling1),
            new SoundPlayer(Properties.Resources.PieceFalling2),
            new SoundPlayer(Properties.Resources.PieceFalling3),
        };
        private readonly Random random = new Random();
        private static readonly SoundPlayer VICTORY_SOUND = new SoundPlayer(Properties.Resources.Victory);

        public GamePanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            this.BackColor = Color.Lavender;
                 

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "GamePanel";

            for(int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    ref var button = ref buttons[x, y];
                    button = new Button();
                    button.SetBounds(BUTTON_WIDTH + (BUTTON_WIDTH * x), BUTTON_HEIGHT + (BUTTON_HEIGHT * y), BUTTON_WIDTH, BUTTON_HEIGHT);
                    button.Click += new EventHandler(ButtonsClickHandler);
                    button.Tag = new Coordinate() { x = x, y = y };
                    Controls.Add(button);
                }
            }

            var buttonGridRightEnd = BUTTON_WIDTH + (BUTTON_WIDTH * buttons.GetLength(0));
            activePlayerLabel = new Label
            {
                Location = new Point(buttonGridRightEnd + 2 * BUTTON_WIDTH, BUTTON_HEIGHT),
                Width = 150,
                AutoEllipsis = true,
            };
            Controls.Add(activePlayerLabel);

            activePlayerButton = new Button();
            activePlayerButton.SetBounds(activePlayerLabel.Location.X + activePlayerLabel.Width, activePlayerLabel.Location.Y, BUTTON_WIDTH, activePlayerLabel.Height);
            activePlayerButton.Enabled = false;
            Controls.Add(activePlayerButton);

            var menuStrip = new MenuStrip();
            var restartMenuItem = menuStrip.Items.Add("Restart");
            restartMenuItem.Click += new EventHandler((sender, args) => StartNewGame());
            var exitGameMenuItem = menuStrip.Items.Add("Back to main menu");
            exitGameMenuItem.Click += new EventHandler((sender, args) => parentForm.MainMenuPanel.SwitchTo());
            Controls.Add(menuStrip);
        }

        public void SwitchTo(Player[] players)
        {
            Debug.Assert(players.Length == 2);

            Players = players;
            StartNewGame();

            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }

        private void StartNewGame()
        {
            for(int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    buttons[x, y].BackColor = Color.LightGray;
                }
            }

            activePlayer = 0;
            StartNewTurn();

        }

        private void ButtonsClickHandler(object sender, EventArgs e)
        {
            var button = ((Button)sender);
            var coord = FindBottomButton(((Coordinate)button.Tag).x);

            if (coord == null) return;
            var (x, y) = coord ?? default; //Default is to please the compiler: coord could never be null here

            buttons[x, y].BackColor = Players[activePlayer].Color;

            if(DetectVictory(x, y))
            {
                WinGame();
            } else
            {
                PIECE_FALLING_SOUNDS[random.Next(0, 3)].Play();
                activePlayer = (activePlayer + 1) % 2;
                StartNewTurn();
            }
        }

        private void StartNewTurn()
        {
            activePlayerButton.BackColor = Players[activePlayer].Color;
            activePlayerLabel.Text = "Active Player: " + Players[activePlayer].Name;
        }

        //Returns the (x, y) pair corresponding to the lowest button in the column identified by the x coodinate,
        //or returns null if the column is completely full and there is no such button
        private (int, int)? FindBottomButton(int x)
        {
            for(int y = buttons.GetLength(1) - 1; y >= 0; y--)
            {
                if(buttons[x, y].BackColor == Color.LightGray)
                {
                    return (x, y);
                }
            }
            return null;
        }
        
        private bool DetectVictory(int coordX, int coordY)
        {
            return DIRECTIONS.Any(offset => 
            {
                (var offsetX, var offsetY) = offset;
                var colors = Enumerable.Range(0, 4)
                    .Select(i =>
                    {
                        var x = offsetX * i + coordX;
                        var y = offsetY * i + coordY;
                        if (x < 0 || y < 0 || x >= buttons.GetLength(0) || y >= buttons.GetLength(1))
                            return Color.Empty; //Use empty color as sentinel value for out of bounds
                        else
                            return buttons[x, y].BackColor;
                    });

                var firstColor = colors.First();
                Debug.Assert(firstColor != Color.Empty); //First color should always be [coord.x, coord.y], which should always fall in bounds
                return colors.All(color => color == firstColor);
            });
        }

        private void WinGame()
        {
            VICTORY_SOUND.Play();
            parentForm.VictoryPanel.SwitchTo();
        }

    }

    struct Coordinate
    {
        public int x;
        public int y;
    }
}
