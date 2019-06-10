using TetrisAdvanced.Data;

namespace TetrisAdvanced.Interfaces
{
    public interface IShapeService
    {
        void Rotate(Shape shape, Direction direction);
    }
}
