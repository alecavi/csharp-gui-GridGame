using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridGame
{
    public class Player
    {
        public Color Color { get; }

        public Player(Color color)
        {
            this.Color = color;
        }
    }
}
