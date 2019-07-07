using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data.Enumerations;

namespace TetrisAdvanced.Data
{
    public class Engine
    {
        public KeyInput keyPressed;
        public Field field { get; set; }
        public IReadOnlyCollection<Shape> ShapeTypes { get; set; }
        public int rowsCompleted;
        public int speed;
        public Random random;
    }
}
