using System;
using NUnit.Framework;
using TetrisAdvanced.Data;
using TetrisAdvanced.Interfaces.Helpers;
using TetrisAdvanced.Services.Helpers;

namespace TetrisAdvancedUnitTests.Services.Helpers
{
    public class MathHelperServiceTests
    {
        private IMathHelperService sut;

        private const double piOverFour = Math.PI / 4;
        private const double piOverTwo = Math.PI / 2;
        private const double threePiOverTwo = Math.PI * 3 / 2;

        [SetUp]
        public void SetUp()
        {
            sut = new MathHelperService();
        }

        [Test]
        public void CalculateRadius_WhenPointsAreAllZero_ReturnsZero()
        {
            Assert.That(sut.CalculateRadius(0, 0, 0, 0), Is.EqualTo(0));
        }

        [Test]
        public void CalculateRadius_WhenPointsAreAllTheSame_ReturnsZero()
        {
            Assert.That(sut.CalculateRadius(1, 4, 1, 4), Is.EqualTo(0));
        }

        [Test]
        public void CalculateRadius_WhenPointsAreOffByOneOnXAxis_Returns1()
        {
            Assert.That(sut.CalculateRadius(0, 4, 1, 4), Is.EqualTo(1));
        }

        [Test]
        public void CalculateRadius_WhenPointsAreOffByOneOnYAxis_Returns1()
        {
            Assert.That(sut.CalculateRadius(1, 5, 1, 4), Is.EqualTo(1));
        }

        [Test]
        public void CalculateRadius_WhenPointsAreOffByOneOnXAndYAxis_ReturnsSquareRootOfTwo()
        {
            // Arrange
            var hypotenuse = Math.Sqrt(2);

            // Act
            var result = sut.CalculateRadius(0, 5, 1, 4);

            // Assert
            Assert.That(result, Is.EqualTo(hypotenuse));
        }

        [Test]
        public void CalculateRadius_WhenPointsAreNegative_StillCalculatesRadius()
        {
            // Arrange
            var hypotenuse = Math.Sqrt(18);

            // Act
            var result = sut.CalculateRadius(-2, -2, 1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(hypotenuse));
        }

        [Test]
        public void CalculateAdjacentAngleInRadians_WhenHypotenuseIsZero_ThrowsException()
        {
            Assert.Throws<DivideByZeroException>(() => sut.CalculateAdjacentAngleInRadians(1, 0));
        }

        [Test]
        public void CalculateAdjacentAngleInRadians_WhenAdjcentIsZero_ReturnsπOver2()
        {
            Assert.That(sut.CalculateAdjacentAngleInRadians(0, 1), Is.EqualTo(Math.PI / 2));
        }

        [Test]
        public void CalculateAdjacentAngleInRadians_WhenAngleIs45Degrees_ReturnsπOver4()
        {
            // Act & Assert
            Assert.That(sut.CalculateAdjacentAngleInRadians(1, Math.Sqrt(2)), Is.InRange(piOverFour - 0.000001, piOverFour + 0.000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsOverCenterPointAndClockwise_ReturnsFirstQuadrantData()
        {
            // Arrange
            var box = new Box
            {
                X = 0,
                Y = 0
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.CLOCKWISE, 0);

            // Assert
            Assert.That(result, Is.InRange(threePiOverTwo - 0.0000001, threePiOverTwo + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsOverCenterPointAndCounterClockwise_ReturnsFirstQuadrantData()
        {
            // Arrange
            var box = new Box
            {
                X = 0,
                Y = 0
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.COUNTER_CLOCKWISE, 0);

            // Assert
            Assert.That(result, Is.InRange(piOverTwo - 0.0000001, piOverTwo + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInFirstQuadrantAndClockwise_ReturnsFirstQuadrantDataPlusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = 1,
                Y = 1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(threePiOverTwo + piOverFour - 0.0000001, threePiOverTwo + piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInFirstQuadrantAndCounterClockwise_ReturnsFirstQuadrantDataPlusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = 1,
                Y = 1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.COUNTER_CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(piOverTwo + piOverFour - 0.0000001, piOverTwo + piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInSecondQuadrantAndClockwise_ReturnsSecondQuadrantDataMinusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = -1,
                Y = 1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(piOverTwo - piOverFour - 0.0000001, piOverTwo - piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInSecondQuadrantAndCounterClockwise_ReturnsSecondQuadrantDataMinusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = -1,
                Y = 1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.COUNTER_CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(threePiOverTwo - piOverFour - 0.0000001, threePiOverTwo - piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInThirdQuadrantAndClockwise_ReturnsThirdQuadrantDataPlusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = -1,
                Y = -1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(piOverTwo + piOverFour - 0.0000001, piOverTwo + piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInThirdQuadrantAndCounterClockwise_ReturnsThirdQuadrantDataPlusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = -1,
                Y = -1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.COUNTER_CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(threePiOverTwo + piOverFour - 0.0000001, threePiOverTwo + piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInForthQuadrantAndClockwise_ReturnsFourthQuadrantDataMinusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = 1,
                Y = -1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(threePiOverTwo - piOverFour - 0.0000001, threePiOverTwo - piOverFour + 0.0000001));
        }

        [Test]
        public void CalculateAngleOfRotation_WhenBoxIsInFourthQuadrantAndCounterClockwise_ReturnsFourthQuadrantDataMinusAngle()
        {
            // Arrange
            var box = new Box
            {
                X = 1,
                Y = -1
            };

            var shape = new Shape
            {
                CenterX = 0,
                CenterY = 0
            };

            // Act
            var result = sut.CalculateAngleOfRotation(box, shape, RotationDirection.COUNTER_CLOCKWISE, piOverFour);

            // Assert
            Assert.That(result, Is.InRange(piOverTwo - piOverFour - 0.0000001, piOverTwo - piOverFour + 0.0000001));
        }
    }
}
