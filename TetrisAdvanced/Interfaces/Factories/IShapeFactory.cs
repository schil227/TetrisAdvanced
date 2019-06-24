using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;

namespace TetrisAdvanced.Interfaces.Factories
{
    public interface IShapeFactory
    {
        IReadOnlyCollection<Shape> GetShapeTypes();
    }
}
