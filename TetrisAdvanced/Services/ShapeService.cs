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
            foreach (var box in shape.Boxes)
            {
                var radius = mathHelperService.CalculateRadius(box.X, box.Y, shape.CenterX, shape.CenterY);

                if (radius == 0)
                {
                    // The box is at the center of the shape, no rotation
                    continue;
                }

                var adjacentAngle = mathHelperService.CalculateAdjacentAngleInRadians(Math.Abs(box.X - shape.CenterX), radius);

                var angle = mathHelperService.CalculateAngleOfRotation(box, shape, direction, adjacentAngle);

                box.X = Convert.ToInt32(shape.CenterX + radius * Math.Cos(angle));
                box.Y = Convert.ToInt32(shape.CenterY + radius * Math.Sin(angle));
            }
        }
    }
}
