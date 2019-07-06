using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Interfaces.Factories;
using TetrisAdvanced.Services;

namespace TetrisAdvancedUnitTests.Services
{
    public class EngineServiceTests
    {
        private MockRepository moq;
        private Mock<IEngineFactory> mockEngineFactory;
        private Mock<IFieldService> mockFieldService;
        private Mock<IShapeService> mockShapeService;
        private Mock<IInputService> mockInputService;
        private IEngineService sut;

        [SetUp]
        public void SetUp()
        {
            moq = new MockRepository(MockBehavior.Strict);
            mockEngineFactory = moq.Create<IEngineFactory>();
            mockFieldService = moq.Create<IFieldService>();
            mockShapeService = moq.Create<IShapeService>();
            mockInputService = moq.Create<IInputService>();

            sut = new EngineService(mockEngineFactory.Object, mockFieldService.Object, mockShapeService.Object, mockInputService.Object);
        }

        [TearDown]
        public void TearDown() => moq.VerifyAll();

        [Test]
        public void MoveShape_WhenShapeCannotMoveRight_DoesNotMoveShape()
        {
            // Arrange
            var shape = new Shape();
            var shadowShape = new Shape();
            var field = new Field
            {
                Width = 2,
                Height = 3,
                ActiveShape = shape
            };

            mockShapeService.Setup(s => s.CopyShape(shape)).Returns(shadowShape);
            mockShapeService.Setup(s => s.MoveShape(shadowShape, 1, 0));
            mockFieldService.Setup(s => s.CanMoveShape(field, shadowShape)).Returns(false);

            // Act
            var result = sut.MoveShape(field, MoveDirection.RIGHT);

            // Assert
            Assert.That(result, Is.EqualTo(ActiveShapeStatus.ACTIVE));
        }

        [Test]
        public void MoveShape_WhenShapeCannotMoveLeft_DoesNotMoveShape()
        {
            // Arrange
            var shape = new Shape();
            var shadowShape = new Shape();
            var field = new Field
            {
                Width = 2,
                Height = 3,
                ActiveShape = shape
            };

            mockShapeService.Setup(s => s.CopyShape(shape)).Returns(shadowShape);
            mockShapeService.Setup(s => s.MoveShape(shadowShape, -1, 0));
            mockFieldService.Setup(s => s.CanMoveShape(field, shadowShape)).Returns(false);

            // Act
            var result = sut.MoveShape(field, MoveDirection.LEFT);

            // Assert
            Assert.That(result, Is.EqualTo(ActiveShapeStatus.ACTIVE));
        }

        [Test]
        public void MoveShape_WhenShapeCanMoveRight_MovesShape()
        {
            // Arrange
            var shape = new Shape();
            var shadowShape = new Shape();
            var field = new Field
            {
                Width = 2,
                Height = 3,
                ActiveShape = shape
            };

            mockShapeService.Setup(s => s.CopyShape(shape)).Returns(shadowShape);
            mockShapeService.Setup(s => s.MoveShape(shadowShape, 1, 0));
            mockFieldService.Setup(s => s.CanMoveShape(field, shadowShape)).Returns(true);
            mockShapeService.Setup(s => s.MoveShape(shape, 1, 0));

            // Act
            var result = sut.MoveShape(field, MoveDirection.RIGHT);

            // Assert
            Assert.That(result, Is.EqualTo(ActiveShapeStatus.ACTIVE));
        }

        [Test]
        public void MoveShape_WhenShapeCanMoveLeft_MovesShape()
        {
            // Arrange
            var shape = new Shape();
            var shadowShape = new Shape();
            var field = new Field
            {
                Width = 2,
                Height = 3,
                ActiveShape = shape
            };

            mockShapeService.Setup(s => s.CopyShape(shape)).Returns(shadowShape);
            mockShapeService.Setup(s => s.MoveShape(shadowShape, -1, 0));
            mockFieldService.Setup(s => s.CanMoveShape(field, shadowShape)).Returns(true);
            mockShapeService.Setup(s => s.MoveShape(shape, -1, 0));

            // Act
            var result = sut.MoveShape(field, MoveDirection.LEFT);

            // Assert
            Assert.That(result, Is.EqualTo(ActiveShapeStatus.ACTIVE));
        }

        [Test]
        public void MoveShape_WhenShapeCanMoveDown_MovesShape()
        {
            // Arrange
            var shape = new Shape
            {
                Boxes = new List<Box>
                {
                    new Box
                    {
                        X = 0,
                        Y = 0
                    }
                }
            };

            var shadowShape = new Shape();

            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = shape,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            mockShapeService.Setup(s => s.CopyShape(shape)).Returns(shadowShape);
            mockShapeService.Setup(s => s.MoveShape(shadowShape, 0, 1));
            mockFieldService.Setup(s => s.CanMoveShape(field, shadowShape)).Returns(false);

            // Act
            var result = sut.MoveShape(field, MoveDirection.DOWN);

            // Assert
            Assert.That(result, Is.EqualTo(ActiveShapeStatus.INACTIVE));
            Assert.False(field.Grid[0, 1].IsOpen);
        }

        [Test]
        public void MoveShape_WhenShapeCanNotMoveDown_LocksShapeToGrid()
        {
            // Arrange
            var shape = new Shape();
            var shadowShape = new Shape();
            var field = new Field
            {
                Width = 2,
                Height = 3,
                ActiveShape = shape,

            };

            mockShapeService.Setup(s => s.CopyShape(shape)).Returns(shadowShape);
            mockShapeService.Setup(s => s.MoveShape(shadowShape, 0, 1));
            mockFieldService.Setup(s => s.CanMoveShape(field, shadowShape)).Returns(true);
            mockShapeService.Setup(s => s.MoveShape(shape, 0, 1));

            // Act
            var result = sut.MoveShape(field, MoveDirection.DOWN);

            // Assert
            Assert.That(result, Is.EqualTo(ActiveShapeStatus.ACTIVE));
        }
    }
}
