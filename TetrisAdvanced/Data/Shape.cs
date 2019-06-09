using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAdvanced.Data
{
    public class Shape
    {
        public ICollection<Box> Boxes { get; set; }

        public double CenterX { get; set; }

        public double CenterY { get; set; }
    }
}
