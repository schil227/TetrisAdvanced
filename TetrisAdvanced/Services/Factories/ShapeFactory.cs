using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces.Factories;

namespace TetrisAdvanced.Services.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        public IReadOnlyCollection<Shape> GetShapeTypes()
        {
            return new List<Shape>
            {
                LeftBolt(),
                RightBolt(),
                LeftL(),
                RightL(),
                T(),
                Box(),
                TheHolyLinePiece()
            };
        }

        // ╚╗ 
        private Shape LeftBolt()
        {
            return new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 0, Y = 1},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 2}
                    }
            };
        }

        // ╔╝
        private Shape RightBolt()
        {
            return new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 1, Y = 0},
                        new Box {X = 1, Y = 1},
                        new Box {X = 0, Y = 1},
                        new Box {X = 0, Y = 2}
                    }
            };
        }

        // ║
        // ╚═
        private Shape LeftL()
        {
            return new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 0, Y = 1},
                        new Box {X = 0, Y = 2},
                        new Box {X = 1, Y = 2}
                    }
            };
        }

        //  ║
        // ═╝
        private Shape RightL()
        {
            return new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 1, Y = 0},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 2},
                        new Box {X = 0, Y = 2}
                    }
            };
        }

        // ╩
        private Shape T()
        {
            return new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 1},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 0},
                        new Box {X = 2, Y = 1}
                    }
            };
        }

        // ╔═╗
        // ╚═╝
        private Shape Box()
        {
            return new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 1, Y = 0},
                        new Box {X = 1, Y = 1},
                        new Box {X = 0, Y = 1}
                    }
            };
        }

        // ║
        // ║
        private Shape TheHolyLinePiece()
        {
            return new Shape
            {
                CenterX = 0,
                CenterY = 0,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 0, Y = 1},
                        new Box {X = 0, Y = 2},
                        new Box {X = 0, Y = 3}
                    }
            };
        }
    }
}
