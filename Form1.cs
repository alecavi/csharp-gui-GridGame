using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{
    public partial class Form1 : Form
    {
        private readonly Button[,] buttons = new Button[7, 6];
        private readonly Player[] players = new Player[2];
        private int activePlayer = 0;
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

        private Panel activePanel;
        private readonly Panel gamePanel = new Panel();
        private readonly Panel menuPanel = new Panel();
        private static readonly int MENU_BUTTON_WIDTH = 100;
        private static readonly int MENU_BUTTON_HEIGHT = 50;

        public Form1()
        {
            InitializeComponent();

            InitializeMenuPanel();
            InitializeGamePanel();

            players[0] = new Player(Color.Red);
            players[1] = new Player(Color.Yellow);

        }

        private void InitializeMenuPanel()
        {
            menuPanel.Location = new Point(0, 0);
            menuPanel.Size = new Size(Width, Height);

            var playButton = new Button();
            playButton.SetBounds((Width / 2) - (MENU_BUTTON_WIDTH / 2), 50, MENU_BUTTON_WIDTH, MENU_BUTTON_HEIGHT);
            playButton.Text = "PLAY";
            playButton.Click += new EventHandler((sender, e) =>
            {
                //These three lines were inspired by https://stackoverflow.com/questions/13584902/change-content-in-a-windows-form
                activePanel.Visible = false;
                activePanel = gamePanel;
                activePanel.Visible = true;
            });

            menuPanel.Controls.Add(playButton);
            Controls.Add(menuPanel);
            activePanel = menuPanel;
        }

        private void InitializeGamePanel()
        {
            gamePanel.Location = new Point(0, 0);
            gamePanel.Size = new Size(Width, Height);
            for(int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    ref var button = ref buttons[x, y];
                    button = new Button();
                    button.SetBounds(45 + (45 * x), 45 + (45 * y), 45, 45);
                    button.BackColor = Color.LightGray;
                    button.Click += new EventHandler(ButtonEvent_Click);
                    button.Tag = new Coordinate() { x = x, y = y };
                    gamePanel.Controls.Add(button);
                }
            }
            Controls.Add(gamePanel);
        }

        void ButtonEvent_Click(object sender, EventArgs e)
        {
            var button = ((Button)sender);
            int x = ((Coordinate)button.Tag).x;


            for(int i = buttons.GetLength(1) - 1; i >= 0; i--)
            {
                if(buttons[x, i].BackColor == Color.LightGray)
                {
                    buttons[x, i].BackColor = players[activePlayer].Color;
                    activePlayer = (activePlayer + 1) % 2;
                    if(DetectVictory(x, i))
                    {
                        //TODO: Actually terminate the game
                        buttons[x, i].BackColor = Color.Blue;
                    }
                    break;
                }
            }
        }

        bool DetectVictory(int coordX, int coordY)
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
                            return Color.Empty;
                        else
                            return buttons[x, y].BackColor;
                    });

                var firstColor = colors.First();
                Debug.Assert(firstColor != Color.Empty); //First color should always be [coord.x, coord.y], which should always fall in bounds
                return colors.All(color => color == firstColor);
            });
        }
    }

    struct Coordinate
    {
        public int x;
        public int y;
    }
}
