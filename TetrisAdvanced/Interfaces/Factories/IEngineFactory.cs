using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;

namespace TetrisAdvanced.Interfaces.Factories
{
    public interface IEngineFactory
    {
        Engine CreateEngine(int fieldWidth, int fieldHeight);
    }
}
