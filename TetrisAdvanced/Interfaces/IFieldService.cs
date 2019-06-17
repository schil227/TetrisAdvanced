using TetrisAdvanced.Data;

namespace TetrisAdvanced.Interfaces
{
    public interface IFieldService
    {
        bool MoveShape(Field field, MoveDirection direction, bool isForced);
        bool CanMoveShape(Field field, Shape shape);
        void HandleCompletedRows(Field field);
    }
}
