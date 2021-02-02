using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    button.Tag = new ButtonPosition() { x = x, y = y };
                    Controls.Add(button);
                }
            }

            this.players[0] = new Player(Color.Red);
            this.players[1] = new Player(Color.Yellow);

        }

        void ButtonEvent_Click(object sender, EventArgs e)
        {
            var button = ((Button)sender);
            int x = ((ButtonPosition)button.Tag).x;


            for(int i = buttons.GetLength(1) - 1; i >= 0; i--)
            {
                if(buttons[x, i].BackColor == Color.LightGray)
                {
                    buttons[x, i].BackColor = players[activePlayer].Color;
                    activePlayer = (activePlayer + 1) % 2;
                    break;
                }
            }

        }

        //TODO: draw the color of the player that is currently active.
        override protected void OnPaint(PaintEventArgs e)
        {
            //Method body adapted from https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-draw-a-filled-rectangle-on-a-windows-form?view=netframeworkdesktop-4.8
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(600, 55, 45, 45));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

    }

    struct ButtonPosition
    {
        public int x;
        public int y;
    }
}
