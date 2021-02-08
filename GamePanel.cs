using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{
    public class GamePanel : Panel
    {
        private readonly GameForm parentForm;

        private readonly Button[,] buttons = new Button[7, 6];
        public Player[] Players { get; private set; }
        private int activePlayer;
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

        public GamePanel(GameForm parentForm)
        {
            this.parentForm = parentForm;

            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "Game Panel";

            for(int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    ref var button = ref buttons[x, y];
                    button = new Button();
                    button.SetBounds(45 + (45 * x), 45 + (45 * y), 45, 45);
                    button.Click += new EventHandler(ButtonsClickHandler);
                    button.Tag = new Coordinate() { x = x, y = y };
                    Controls.Add(button);
                }
            }
        }

        public void SwitchTo(Player[] players)
        {
            Debug.Assert(players.Length == 2);

            Players = players;
            activePlayer = 0;

            for(int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    buttons[x, y].BackColor = Color.LightGray;
                }
            }

            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }

        private void ButtonsClickHandler(object sender, EventArgs e)
        {
            var button = ((Button)sender);
            int x = ((Coordinate)button.Tag).x;

            for(int i = buttons.GetLength(1) - 1; i >= 0; i--)
            {
                if(buttons[x, i].BackColor == Color.LightGray)
                {
                    buttons[x, i].BackColor = Players[activePlayer].Color;
                    activePlayer = (activePlayer + 1) % 2;
                    if(DetectVictory(x, i))
                    {
                        WinGame();
                    }
                    break;
                }
            }
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
            parentForm.VictoryPanel.SwitchTo();
        }
    }

    struct Coordinate
    {
        public int x;
        public int y;
    }
}
