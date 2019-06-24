using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;

namespace TetrisAdvanced.Interfaces
{
    public interface IFieldService
    {
        bool CanMoveShape(Field field, Shape shape);
        RowProcessingResult HandleCompletedRows(Field field);
    }
}
