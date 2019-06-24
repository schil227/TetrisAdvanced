using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAdvanced.Data
{
    public class Engine
    {
        public Field field { get; set; }
        public IReadOnlyCollection<Shape> ShapeTypes { get; set; }
        public int rowsCompleted;
        public int speed;
    }
}
