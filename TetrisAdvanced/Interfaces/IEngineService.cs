using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerators;

namespace TetrisAdvanced.Interfaces
{
    public interface IEngineService
    {
        void Run();
        ActiveShapeStatus MoveShape(Field field, MoveDirection direction);
    }
}
