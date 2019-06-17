using TetrisAdvanced.Data.Enumerators;

namespace TetrisAdvanced.Interfaces
{
    public interface IInputService
    {
        void HandleCommand(KeyInput keyType);
    }
}
