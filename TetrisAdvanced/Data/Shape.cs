using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisAdvanced.Data
{
    public class Shape : IEquatable<Shape>
    {
        public ICollection<Box> Boxes { get; set; }

        public double CenterX { get; set; }

        public double CenterY { get; set; }

        public bool Equals(Shape other)
        {
            {
                if (other == null)
                {
                    return false;
                }

                if (CenterX != other.CenterX || CenterY != other.CenterY)
                {
                    return false;
                }

                if (Boxes.Count != other.Boxes.Count)
                {
                    return false;
                }

                foreach (var box in Boxes)
                {
                    if (!other.Boxes.Any(x => box.Equals(x)))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
