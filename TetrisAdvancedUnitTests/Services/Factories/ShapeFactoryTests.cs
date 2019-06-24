using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Interfaces.Factories;
using TetrisAdvanced.Services;
using TetrisAdvanced.Services.Factories;

namespace TetrisAdvancedUnitTests.Services.Factories
{
    public class ShapeFactoryTests
    {
        private IShapeFactory sut;
        private IShapeService shapeService;

        [SetUp]
        public void SetUp()
        {
            shapeService = new ShapeService(null);
            sut = new ShapeFactory();
        }

        [Test]
        public void GetShapeTypes_ReturnsAllStandardTetrisShapes()
        {
            // Arrange

            // ╚╗
            var leftBolt = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 0, Y = 1},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 2},
                    }
            };

            // ╔╝
            var rightBolt = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 1, Y = 0},
                        new Box {X = 1, Y = 1},
                        new Box {X = 0, Y = 1},
                        new Box {X = 0, Y = 2},
                    }
            };

            // ║
            // ╚═
            var leftL = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 0, Y = 1},
                        new Box {X = 0, Y = 2},
                        new Box {X = 1, Y = 2},
                    }
            };

            //  ║
            // ═╝
            var rightL = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 1, Y = 0},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 2},
                        new Box {X = 0, Y = 2},
                    }
            };

            // ╩
            var T = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 1},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 0},
                        new Box {X = 2, Y = 1},
                    }
            };

            // ╔═╗
            // ╚═╝
            var square = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 1, Y = 1},
                        new Box {X = 1, Y = 1},
                        new Box {X = 2, Y = 2},
                    }
            };

            // ║
            // ║
            var line = new Shape
            {
                CenterX = 0,
                CenterY = 0,
                Boxes = new List<Box>
                    {
                        new Box {X = 0, Y = 0},
                        new Box {X = 0, Y = 1},
                        new Box {X = 0, Y = 2},
                        new Box {X = 0, Y = 3},
                    }
            };

            // Act
            var shapes = sut.GetShapeTypes();

            // Assert
            Assert.That(shapes.Count, Is.EqualTo(7));

            Assert.True(shapes.Any(s => s.Equals(leftBolt)));
            Assert.True(shapes.Any(s => s.Equals(rightBolt)));
            Assert.True(shapes.Any(s => s.Equals(leftL)));
            Assert.True(shapes.Any(s => s.Equals(rightL)));
            Assert.True(shapes.Any(s => s.Equals(T)));
            Assert.True(shapes.Any(s => s.Equals(square)));
            Assert.True(shapes.Any(s => s.Equals(line)));
        }
    }
}
