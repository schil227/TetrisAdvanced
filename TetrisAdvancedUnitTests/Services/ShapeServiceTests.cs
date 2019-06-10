using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Services;

namespace Tests.Services
{
    public class ShapeServiceTests
    {
        private IShapeService sut;

        [SetUp]
        public void Setup()
        {
            sut = new ShapeService();
        }

        [Test]
        public void Rotate_WhenDirectionIsRight_RotatesShapeToTheRight()
        {
            // Arrange
            var shape = new Shape
            {
                //╚╗ 
                Boxes = new List<Box>
                {
                    new Box
                    {
                        X = 0,
                        Y = 0
                    },
                    new Box
                    {
                        X = 0,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 2
                    }
                },
                CenterX = 1,
                CenterY = 1
            };

            // Act
            sut.Rotate(shape, Direction.RIGHT);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 2) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 2 && b.Y == 1) != null);
        }

        [Test]
        public void Rotate_WhenShapeRotatedRightFourTimes_IsTheSameAsTheOriginalOrientation()
        {
            // Arrange
            var shape = new Shape
            {
                //╚╗ 
                Boxes = new List<Box>
                {
                    new Box
                    {
                        X = 0,
                        Y = 0
                    },
                    new Box
                    {
                        X = 0,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 2
                    }
                },
                CenterX = 1,
                CenterY = 1
            };

            // Act
            sut.Rotate(shape, Direction.RIGHT);
            sut.Rotate(shape, Direction.RIGHT);
            sut.Rotate(shape, Direction.RIGHT);
            sut.Rotate(shape, Direction.RIGHT);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
        }

        [Test]
        public void Rotate_WhenShapeRotatedLeftFourTimes_IsTheSameAsTheOriginalOrientation()
        {
            // Arrange
            var shape = new Shape
            {
                //╚╗ 
                Boxes = new List<Box>
                {
                    new Box
                    {
                        X = 0,
                        Y = 0
                    },
                    new Box
                    {
                        X = 0,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 2
                    }
                },
                CenterX = 1,
                CenterY = 1
            };

            // Act
            sut.Rotate(shape, Direction.LEFT);
            sut.Rotate(shape, Direction.LEFT);
            sut.Rotate(shape, Direction.LEFT);
            sut.Rotate(shape, Direction.LEFT);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
        }

        [Test]
        public void Rotate_WhenShapeRotatedOneWayThenBack_IsTheSameAsTheOriginalOrientation()
        {
            // Arrange
            var shape = new Shape
            {
                //╚╗ 
                Boxes = new List<Box>
                {
                    new Box
                    {
                        X = 0,
                        Y = 0
                    },
                    new Box
                    {
                        X = 0,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 1
                    },
                    new Box
                    {
                        X = 1,
                        Y = 2
                    }
                },
                CenterX = 1,
                CenterY = 1
            };

            // Act
            sut.Rotate(shape, Direction.LEFT);
            sut.Rotate(shape, Direction.RIGHT);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
        }
    }
}