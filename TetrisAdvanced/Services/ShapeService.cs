using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces;

namespace TetrisAdvanced.Services
{
    public class ShapeService : IShapeService
    {
        public void Rotate(Shape shape, Direction direction)
        {
            var angle = Math.PI / 2;

            if (direction == Direction.LEFT)
            {
                angle *= -1;
            }

            foreach (var box in shape.Boxes)
            {
                // calculate R (radius between shape center and box coordinates),
                // y = r sin (theta)
                // x = r cos (theta)
            }
        }
    }
}
