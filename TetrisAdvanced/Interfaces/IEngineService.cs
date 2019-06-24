using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;

namespace TetrisAdvanced.Interfaces
{
    public interface IEngineService
    {
        void Run();
        ActiveShapeStatus MoveShape(Field field, MoveDirection direction);
    }
}
