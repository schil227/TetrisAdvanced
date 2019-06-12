using TetrisAdvanced.Data;

namespace TetrisAdvanced.Interfaces.Helpers
{
    public interface IMathHelperService
    {
        double CalculateAngleOfRotation(Box box, Shape shape, RotationDirection direction, double angle);

        double CalculateRadius(double x1, double y1, double x2, double y2);

        double CalculateAdjacentAngleInRadians(double adjacent, double hypotenuse);
    }
}
