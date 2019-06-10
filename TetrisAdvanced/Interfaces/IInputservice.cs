using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;

namespace TetrisAdvanced.Interfaces
{
    public interface IInputService
    {
        void HandleCommand(KeyInput keyType);
    }
}
