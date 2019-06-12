using System;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Interfaces.Helpers;

namespace TetrisAdvanced.Services
{
    public class ShapeService : IShapeService
    {
        private readonly IMathHelperService mathHelperService;

        public ShapeService(IMathHelperService mathHelperService)
        {
            this.mathHelperService = mathHelperService;
        }

        public void Rotate(Shape shape, RotationDirection direction)
        {
            var angle = Math.PI / 4;

            if (direction == RotationDirection.COUNTER_CLOCKWISE)
            {
                angle = Math.PI * 5 / 4; // Possibly wrong, double check
            }

            foreach (var box in shape.Boxes)
            {
                /*  |
                 *  |       ○(x, y)
                 *  |         \
                 *  |          \  radius
                 *  |          θ\
                 *  |       -----○ (CenterX, CenterY)
                 *  |
                 * -╬----------------
                 *  |
                 * */
                var radius = mathHelperService.CalculateRadius(box.X, box.Y, shape.CenterX, shape.CenterY);

                if (radius == 0)
                {
                    // The box is on the origin, no rotation
                    continue;
                }

                angle = mathHelperService.CalculateAdjacentAngleInRadians(Math.Abs(box.X - shape.CenterX), radius);

                angle = mathHelperService.CalculateAngleOfRotation(box, shape, direction, angle);

                box.X = Convert.ToInt32(shape.CenterX + radius * Math.Cos(angle));
                box.Y = Convert.ToInt32(shape.CenterY + radius * Math.Sin(angle));
            }
        }
    }
}
