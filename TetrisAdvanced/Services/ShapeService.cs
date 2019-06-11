using System;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces;

namespace TetrisAdvanced.Services
{
    public class ShapeService : IShapeService
    {
        public void Rotate(Shape shape, Direction direction)
        {
            var angle = Math.PI / 4;

            if (direction == Direction.LEFT)
            {
                angle = Math.PI * 5 / 4; // Possibly wrong, double check
            }

            foreach (var box in shape.Boxes)
            {
                /*  |
                 *  |       ○(x, y)
                 *  |       | \
                 *  |       |  \  radius
                 *  |       |  θ\
                 *  |       ╚----○ (CenterX, CenterY)
                 *  |
                 * -╬----------------
                 *  |
                 * */
                var radius = Math.Sqrt(Math.Pow(box.X - shape.CenterX, 2) + Math.Pow(box.Y - shape.CenterY, 2));

                if (radius == 0)
                {
                    // The box is on the origin, no rotation
                    continue;
                }

                // adds θ offset: Cos^-1(Adjcent / Hyp)
                angle += Math.Acos(Math.Abs(box.X - shape.CenterX) / radius);

                box.X = Convert.ToInt32(shape.CenterX + radius * Math.Cos(angle));
                box.Y = Convert.ToInt32(shape.CenterY + radius * Math.Sin(angle));
            }
        }
    }
}
