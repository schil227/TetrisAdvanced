using System;
using System.Collections.Generic;
using System.Text;
using Tetris.Data;

namespace TetrisAdvanced.Interfaces
{
    public interface IShapeService
    {
        void Rotate(Shape shape, Direction direction);
    }
}
