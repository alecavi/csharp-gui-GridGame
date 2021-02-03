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
        Button[,] buttons = new Button[7, 6];
        Player[] players = new Player[2];
        int activePlayer = 0;
        private static readonly (int, int)[] directions = new (int, int)[] 
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

        public Form1()
        {
            InitializeComponent();
            for(int x = 0; x < buttons.GetLength(0); x++)
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    ref var button = ref buttons[x, y];
                    button = new Button();
                    button.SetBounds(45 + (45 * x), 45 + (45 * y), 45, 45);
                    button.BackColor = Color.LightGray;
                    button.Click += new EventHandler(this.ButtonEvent_Click);
                    button.Tag = new Coordinate() { x = x, y = y };
                    Controls.Add(button);
                }
            }

            this.players[0] = new Player(Color.Red);
            this.players[1] = new Player(Color.Yellow);

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
            return directions.Any(offset => 
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
