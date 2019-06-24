using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TetrisAdvanced.Data;

namespace TetrisAdvancedUnitTests.Data
{
    public class ShapeTests
    {
        [Test]
        public void ShapesAreEqual_WhenOtherShapeNull_ReturnsFalse()
        {
            // Arrange
            Shape sut = new Shape();
            Shape other = null;

            // Act
            var result = sut.Equals(other);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void ShapesAreEqual_WhenCentersDoNotMatch_ReturnsFalse()
        {
            // Arrange
            Shape sut = new Shape
            {
                CenterX = 1,
                CenterY = 1
            };

            Shape other = new Shape
            {
                CenterX = 1,
                CenterY = 0
            };

            // Act
            var result = sut.Equals(other);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void ShapesAreEqual_WhenBoxCountUnequal_ReturnsFalse()
        {
            // Arrange
            Shape sut = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 1, Y = 0}
                }
            };

            Shape other = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 1, Y = 0},
                    new Box {X = 1, Y = 1}
                }
            };

            // Act
            var result = sut.Equals(other);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void ShapesAreEqual_WhenAllBoxesDifferent_ReturnsFalse()
        {
            // Arrange
            Shape sut = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 1, Y = 0},
                    new Box {X = 1, Y = 2}
                }
            };

            Shape other = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 0, Y = 0},
                    new Box {X = 1, Y = 1}
                }
            };

            // Act
            var result = sut.Equals(other);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void ShapesAreEqual_WhenSomeBoxesDifferent_ReturnsFalse()
        {
            // Arrange
            Shape sut = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 0, Y = 0},
                    new Box {X = 1, Y = 2}
                }
            };

            Shape other = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 0, Y = 0},
                    new Box {X = 1, Y = 1}
                }
            };

            // Act
            var result = sut.Equals(other);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void ShapesAreEqual_WhenAllBoxesTheSame_ReturnsTrue()
        {
            // Arrange
            Shape sut = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 0, Y = 0},
                    new Box {X = 1, Y = 2}
                }
            };

            Shape other = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box {X = 0, Y = 0},
                    new Box {X = 1, Y = 2}
                }
            };

            // Act
            var result = sut.Equals(other);

            // Assert
            Assert.True(result);
        }
    }
}
