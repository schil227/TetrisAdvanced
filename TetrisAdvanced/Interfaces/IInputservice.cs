using TetrisAdvanced.Data.Enumerations;

namespace TetrisAdvanced.Interfaces
{
    public interface IInputService
    {
        void HandleCommand(KeyInput keyType);
    }
}
