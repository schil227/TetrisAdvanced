using System.Threading.Tasks;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;

namespace TetrisAdvanced.Interfaces
{
    public interface IInputService
    {
        Task HandleCommand(Engine engine);
    }
}
