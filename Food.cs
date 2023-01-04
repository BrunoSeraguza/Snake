using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Food
    {
        public Point Location { get;private set; }

        public void CreateFood()
        {
            Random rnd= new Random();
            Location= new Point(rnd.Next(0,17), rnd.Next(0,17));
        }
    }
}
