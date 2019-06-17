using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerators;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Services;
using TetrisAdvanced.Services.Helpers;

namespace Tests.Services
{
    public class ShapeServiceTests
    {
        private IShapeService sut;

        [SetUp]
        public void Setup()
        {
            sut = new ShapeService(new MathHelperService());
        }

        [Test]
        public void Rotate_WhenDirectionIsClockwise_RotatesShapeClockwise()
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
            sut.Rotate(shape, RotationDirection.CLOCKWISE);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 2) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 2 && b.Y == 1) != null);
        }

        [Test]
        public void Rotate_WhenDirectionIsCounterClockwise_RotatesShapeCounterClockwise()
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
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 2 && b.Y == 0) != null);
        }

        [Test]
        public void Rotate_WhenRotatedTwice_RotatesShapeUpsideDown()
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
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 2 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 2 && b.Y == 2) != null);
        }

        [Test]
        public void Rotate_WhenShapeRotatedClockwiseFourTimes_IsTheSameAsTheOriginalOrientation()
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
            sut.Rotate(shape, RotationDirection.CLOCKWISE);
            sut.Rotate(shape, RotationDirection.CLOCKWISE);
            sut.Rotate(shape, RotationDirection.CLOCKWISE);
            sut.Rotate(shape, RotationDirection.CLOCKWISE);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
        }

        [Test]
        public void Rotate_WhenShapeRotatedCounterClockwiseFourTimes_IsTheSameAsTheOriginalOrientation()
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
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);

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
            sut.Rotate(shape, RotationDirection.COUNTER_CLOCKWISE);
            sut.Rotate(shape, RotationDirection.CLOCKWISE);

            // Assert
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 0) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 0 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 1) != null);
            Assert.True(shape.Boxes.SingleOrDefault(b => b.X == 1 && b.Y == 2) != null);
        }
    }
}
