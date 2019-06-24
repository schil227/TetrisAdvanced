using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAdvanced.Data
{
    public class Box : IEquatable<Box>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Box other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
