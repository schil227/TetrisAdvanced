using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerators;

namespace TetrisAdvanced.Interfaces
{
    public interface IFieldService
    {
        ActiveShapeStatus MoveShape(Field field, MoveDirection direction, bool isForced);
        bool CanMoveShape(Field field, Shape shape);
        void HandleCompletedRows(Field field);
    }
}
