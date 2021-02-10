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
        public String Name { get; }

        public Player(Color color, String name)
        {
            this.Color = color;
            this.Name = name;
        }
    }
}
