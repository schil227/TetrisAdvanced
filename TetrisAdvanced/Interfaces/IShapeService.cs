﻿using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;

namespace TetrisAdvanced.Interfaces
{
    public interface IShapeService
    {
        void Rotate(Shape shape, RotationDirection direction);
        Shape CopyShape(Shape shape);
        void MoveShape(Shape shape, int xOffset, int yOffset);
    }
}
