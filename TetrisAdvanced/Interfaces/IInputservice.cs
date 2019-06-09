using System;
using System.Collections.Generic;
using System.Text;
using Tetris.Data;

namespace TetrisAdvanced.Interfaces
{
    public interface IInputService
    {
        void HandleCommand(KeyInput keyType);
    }
}
