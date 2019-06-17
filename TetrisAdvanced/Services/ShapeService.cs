using System;
using System.Collections.Generic;
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

        public void MoveShape(Shape shape, int xOffset, int yOffset)
        {
            foreach (var box in shape.Boxes)
            {
                box.X += xOffset;
                box.Y += yOffset;
            }
        }

        public Shape CopyShape(Shape shape)
        {
            var movedBoxes = new List<Box>();

            foreach (var box in shape.Boxes)
            {
                movedBoxes.Add(new Box
                {
                    X = box.X,
                    Y = box.Y
                });
            }

            return new Shape
            {
                CenterX = shape.CenterX,
                CenterY = shape.CenterY,
                Boxes = movedBoxes
            };
        }
    }
}
