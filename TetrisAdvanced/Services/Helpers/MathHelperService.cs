using System;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerators;
using TetrisAdvanced.Interfaces.Helpers;

namespace TetrisAdvanced.Services.Helpers
{
    public class MathHelperService : IMathHelperService
    {
        public double CalculateAdjacentAngleInRadians(double adjacent, double hypotenuse)
        {
            /*    |      /
             *    |     /
             *    |    /
             *    |   / θ
             *    |  ○-----
             *   -╬--------- 
             *    |
             *    
             *    Calculates θ in radians
             */

            if (hypotenuse == 0)
            {
                throw new DivideByZeroException("Hypotenuse cannot be zero");
            }

            return Math.Acos(adjacent / hypotenuse);
        }

        public double CalculateAngleOfRotation(Box box, Shape shape, RotationDirection direction, double angle)
        {
            /*   Radians are added to θ relative to the quadrant
             *   and direction of the point relative to the center
             *   
             *                        π/2
             *   CW: A = π/2 - θ       |   CW: A = 3π/2 + θ
             *   CCW: A = 3π/2 - θ     |   CCW: A = π/2 + θ 
             *                         |    
             *   Second Quadrant       |   First Quadrant
             *                         |
             *              π ---------╬--------- 0
             *                         |
             *   Third Quadrant        |   Fourth Quadrant
             *                         |
             *   CW: A = π/2 + θ       |    CW: A = 3π/2 - θ
             *   CCW: A = 3π/2 + θ     |    CCW: A = π/2 - θ 
             *                         |
             *                       3π/2
             */

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape));
            }

            bool xIsPositive = box.X - shape.CenterX >= 0;
            bool yIsPositive = box.Y - shape.CenterY >= 0;

            // First Quadrant
            if (xIsPositive && yIsPositive)
            {
                if (direction == RotationDirection.CLOCKWISE)
                {
                    return Math.PI * 3 / 2 + angle;
                }
                else
                {
                    return Math.PI / 2 + angle;
                }
            }

            // Second Quadrant
            if (!xIsPositive && yIsPositive)
            {
                if (direction == RotationDirection.CLOCKWISE)
                {
                    return Math.PI / 2 - angle;
                }
                else
                {
                    return Math.PI * 3 / 2 - angle;
                }
            }

            // Third Quadrant
            if (!xIsPositive && !yIsPositive)
            {
                if (direction == RotationDirection.CLOCKWISE)
                {
                    return Math.PI / 2 + angle;
                }
                else
                {
                    return Math.PI * 3 / 2 + angle;
                }
            }

            // Forth Quadrant (default: xIsPositive && !yIsPositive)
            if (direction == RotationDirection.CLOCKWISE)
            {
                return Math.PI * 3 / 2 - angle;
            }
            else
            {
                return Math.PI / 2 - angle;
            }
        }

        public double CalculateRadius(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
}
