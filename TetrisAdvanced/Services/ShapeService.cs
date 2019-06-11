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
                /*  |
                 *  |       ○(x, y)
                 *  |       | \
                 *  |       |  \  radius
                 *  |       |   \
                 *  |       ╚----○ (CenterX, CenterY)
                 *  |
                 * -╬----------------
                 *  |
                 * */
                var radius = Math.Sqrt(Math.Pow(box.X - shape.CenterX, 2) + Math.Pow(box.Y - shape.CenterY, 2));

                box.X = Convert.ToInt32(shape.CenterX + radius * Math.Cos(angle));
                box.Y = Convert.ToInt32(shape.CenterY + radius * Math.Sin(angle));
            }
        }
    }
}
