using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces.Factories;

namespace TetrisAdvanced.Services.Factories
{
    public class EngineFactory : IEngineFactory
    {
        private readonly IShapeFactory shapeFactory;

        public EngineFactory(IShapeFactory shapeFactory)
        {
            this.shapeFactory = shapeFactory;
        }

        public Engine CreateEngine(int fieldWidth, int fieldHeight)
        {
            var shapeTypes = shapeFactory.GetShapeTypes();

            var field = new Field
            {
                ActiveShape = null,
                Width = fieldWidth,
                Height = fieldHeight,
                Grid = new SpaceBox[fieldWidth, fieldHeight]
            };

            for (int i = 0; i < fieldWidth; i++)
            {
                for (int j = 0; j < fieldHeight; j++)
                {
                    field.Grid[i, j] = new SpaceBox
                    {
                        IsOpen = true,
                        X = i,
                        Y = j
                    };
                }
            }

            return new Engine
            {
                field = field,
                ShapeTypes = shapeTypes,
                rowsCompleted = 0,
                speed = 0,
                random = new Random()
            };
        }
    }
}
