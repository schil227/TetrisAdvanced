using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Services;

namespace TetrisAdvancedUnitTests.Services
{
    public class FieldServiceTests
    {
        private MockRepository moq;
        private Mock<IShapeService> mockShapeService;
        private IFieldService sut;

        [SetUp]
        public void SetUp()
        {
            moq = new MockRepository(MockBehavior.Strict);
            mockShapeService = moq.Create<IShapeService>();

            sut = new FieldService(mockShapeService.Object);
        }

        [TearDown]
        public void TearDown() => moq.VerifyAll();

        [Test]
        public void CanMoveShape_WhenShapeHasNoBoxes_ReturnsTrue()
        {
            // Arrange
            var field = new Field();
            var shape = new Shape
            {
                Boxes = new List<Box>()
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeDoesNotOverlapWithTakenSpacesOnField_ReturnsTrue()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            //╚╗ 
            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 0, Y = 0},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeBoxXLessThanZero_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = -1, Y = 0},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeBoxXWiderThanField_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 5, Y = 0},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeBoxXWideAsField_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 4, Y = 0},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeBoxYLessThanZero_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 0, Y = -2},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeBoxYWiderThanField_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 0, Y = 5},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CanMoveShape_WhenShapeBoxYWideAsField_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = true}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 0, Y = 3},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.False(result);
        }


        [Test]
        public void CanMoveShape_WhenShapeOverlapsWithTakenSpacesOnField_ReturnsFalse()
        {
            // Arrange

            //
            // ░░░░
            // ░░░░
            // ░░░░
            // ████
            var field = new Field
            {
                Width = 4,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[4, 4]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true}, new SpaceBox{ X = 1, Y = 0, IsOpen = true}, new SpaceBox{ X = 2, Y = 0, IsOpen = true}, new SpaceBox{ X = 3, Y = 0, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 1, IsOpen = true}, new SpaceBox{ X = 1, Y = 1, IsOpen = true}, new SpaceBox{ X = 2, Y = 1, IsOpen = true}, new SpaceBox{ X = 3, Y = 1, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 2, IsOpen = true}, new SpaceBox{ X = 1, Y = 2, IsOpen = false}, new SpaceBox{ X = 2, Y = 2, IsOpen = true}, new SpaceBox{ X = 3, Y = 2, IsOpen = true} },
                    {new SpaceBox{ X = 0, Y = 3, IsOpen = false}, new SpaceBox{ X = 1, Y = 3, IsOpen = false}, new SpaceBox{ X = 2, Y = 3, IsOpen = false}, new SpaceBox{ X = 3, Y = 2, IsOpen = false} }
                }
            };

            //╚╗ 
            var shape = new Shape
            {
                CenterX = 1,
                CenterY = 1,
                Boxes = new List<Box>
                {
                    new Box{ X = 0, Y = 0},
                    new Box{ X = 0, Y = 1},
                    new Box{ X = 1, Y = 1},
                    new Box{ X = 1, Y = 2},
                }
            };

            // Act
            var result = sut.CanMoveShape(field, shape);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void HandleCompletedRows_WhenAllRowsEmpty_DoesNothing()
        {
            // Arrange

            //
            // ░░░
            // ░░░
            // ░░░
            // ░░░
            // ░░░
            var field = new Field
            {
                Width = 3,
                Height = 5,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[3, 5]
                        {
                            {new SpaceBox{ X = 0, Y = 0, IsOpen = true },new SpaceBox{ X = 0, Y = 1, IsOpen = true },new SpaceBox{ X = 0, Y = 2, IsOpen = true },new SpaceBox{ X = 0, Y = 3, IsOpen = true },new SpaceBox{ X = 0, Y = 4, IsOpen = true }},
                            {new SpaceBox{ X = 1, Y = 0, IsOpen = true },new SpaceBox{ X = 1, Y = 1, IsOpen = true },new SpaceBox{ X = 1, Y = 2, IsOpen = true },new SpaceBox{ X = 1, Y = 3, IsOpen = true },new SpaceBox{ X = 1, Y = 4, IsOpen = true }},
                            {new SpaceBox{ X = 2, Y = 0, IsOpen = true },new SpaceBox{ X = 2, Y = 1, IsOpen = true },new SpaceBox{ X = 2, Y = 2, IsOpen = true },new SpaceBox{ X = 2, Y = 3, IsOpen = true },new SpaceBox{ X = 2, Y = 4, IsOpen = true }}
                        }

            };

            // Act
            var result = sut.HandleCompletedRows(field);

            // Assert
            Assert.That(result, Is.EqualTo(RowProcessingResult.NO_ROWS));
            Assert.False(field.Grid.Cast<SpaceBox>().Any(b => !b.IsOpen));
        }

        [Test]
        public void HandleCompletedRows_WhenRowIncomplete_DoesNothing()
        {
            // Arrange

            //
            // ░░░
            // ░░░
            // ░░░
            // ░░░
            // █░░
            var field = new Field
            {
                Width = 3,
                Height = 5,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[3, 5]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true },new SpaceBox{ X = 0, Y = 1, IsOpen = true },new SpaceBox{ X = 0, Y = 2, IsOpen = true },new SpaceBox{ X = 0, Y = 3, IsOpen = true },new SpaceBox{ X = 0, Y = 4, IsOpen = false }},
                    {new SpaceBox{ X = 1, Y = 0, IsOpen = true },new SpaceBox{ X = 1, Y = 1, IsOpen = true },new SpaceBox{ X = 1, Y = 2, IsOpen = true },new SpaceBox{ X = 1, Y = 3, IsOpen = true },new SpaceBox{ X = 1, Y = 4, IsOpen = true }},
                    {new SpaceBox{ X = 2, Y = 0, IsOpen = true },new SpaceBox{ X = 2, Y = 1, IsOpen = true },new SpaceBox{ X = 2, Y = 2, IsOpen = true },new SpaceBox{ X = 2, Y = 3, IsOpen = true },new SpaceBox{ X = 2, Y = 4, IsOpen = true }}
                }
            };

            // Act
            var result = sut.HandleCompletedRows(field);

            // Assert
            Assert.That(result, Is.EqualTo(RowProcessingResult.NO_ROWS));
            Assert.NotNull(field.Grid.Cast<SpaceBox>().SingleOrDefault(b => !b.IsOpen));
        }


        [Test]
        public void HandleCompletedRows_WhenRowIsCompleted_ClearsRow()
        {
            // Arrange

            //
            // ░░░
            // ░░░
            // ░░░
            // ░░░
            // ███
            var field = new Field
            {
                Width = 3,
                Height = 5,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[3, 5]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = true },new SpaceBox{ X = 0, Y = 1, IsOpen = true },new SpaceBox{ X = 0, Y = 2, IsOpen = true },new SpaceBox{ X = 0, Y = 3, IsOpen = true },new SpaceBox{ X = 0, Y = 4, IsOpen = false }},
                    {new SpaceBox{ X = 1, Y = 0, IsOpen = true },new SpaceBox{ X = 1, Y = 1, IsOpen = true },new SpaceBox{ X = 1, Y = 2, IsOpen = true },new SpaceBox{ X = 1, Y = 3, IsOpen = true },new SpaceBox{ X = 1, Y = 4, IsOpen = false }},
                    {new SpaceBox{ X = 2, Y = 0, IsOpen = true },new SpaceBox{ X = 2, Y = 1, IsOpen = true },new SpaceBox{ X = 2, Y = 2, IsOpen = true },new SpaceBox{ X = 2, Y = 3, IsOpen = true },new SpaceBox{ X = 2, Y = 4, IsOpen = false }}
                }
            };

            // Act
            var result = sut.HandleCompletedRows(field);

            // Assert
            Assert.That(result, Is.EqualTo(RowProcessingResult.ONE_ROW));
            Assert.False(field.Grid.Cast<SpaceBox>().Any(b => !b.IsOpen));
        }

        [Test]
        public void HandleCompletedRows_WhenAllRowsCompleted_ClearsGridRow()
        {
            // Arrange

            // ███
            // ███
            // ███
            // ███
            var field = new Field
            {
                Width = 3,
                Height = 4,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[3, 4]

                        {
                            {new SpaceBox{ X = 0, Y = 0, IsOpen = false },new SpaceBox{ X = 0, Y = 1, IsOpen = false },new SpaceBox{ X = 0, Y = 2, IsOpen = false },new SpaceBox{ X = 0, Y = 3, IsOpen = false }},
                            {new SpaceBox{ X = 1, Y = 0, IsOpen = false },new SpaceBox{ X = 1, Y = 1, IsOpen = false },new SpaceBox{ X = 1, Y = 2, IsOpen = false },new SpaceBox{ X = 1, Y = 3, IsOpen = false }},
                            {new SpaceBox{ X = 2, Y = 0, IsOpen = false },new SpaceBox{ X = 2, Y = 1, IsOpen = false },new SpaceBox{ X = 2, Y = 2, IsOpen = false },new SpaceBox{ X = 2, Y = 3, IsOpen = false }}
                        }

            };

            // Act
            var result = sut.HandleCompletedRows(field);

            //
            // ░░░
            // ░░░
            // ░░░
            // ░░░
            // Assert
            Assert.That(result, Is.EqualTo(RowProcessingResult.TETRIS));
            Assert.False(field.Grid.Cast<SpaceBox>().Any(b => !b.IsOpen));
        }

        [Test]
        public void HandleCompletedRows_WhenSeveralRowsCompleted_ClearsRowsAndShiftsUnclearedRowsDown()
        {
            // Arrange

            //
            // █░░
            // ░█░
            // ███
            // ░█░
            // ███
            var field = new Field
            {
                Width = 3,
                Height = 5,
                ShapeX = 0,
                ShapeY = 1,
                ActiveShape = null,
                Grid = new SpaceBox[3, 5]
                {
                    {new SpaceBox{ X = 0, Y = 0, IsOpen = false },new SpaceBox{ X = 0, Y = 1, IsOpen = true },new SpaceBox{ X = 0, Y = 2, IsOpen = false },new SpaceBox{ X = 0, Y = 3, IsOpen = true },new SpaceBox{ X = 0, Y = 4, IsOpen = false }},
                    {new SpaceBox{ X = 1, Y = 0, IsOpen = true },new SpaceBox{ X = 1, Y = 1, IsOpen = false },new SpaceBox{ X = 1, Y = 2, IsOpen = false },new SpaceBox{ X = 1, Y = 3, IsOpen = false },new SpaceBox{ X = 1, Y = 4, IsOpen = false }},
                    {new SpaceBox{ X = 2, Y = 0, IsOpen = true },new SpaceBox{ X = 2, Y = 1, IsOpen = true },new SpaceBox{ X = 2, Y = 2, IsOpen = false },new SpaceBox{ X = 2, Y = 3, IsOpen = true },new SpaceBox{ X = 2, Y = 4, IsOpen = false }}
                }
            };

            // Act
            var result = sut.HandleCompletedRows(field);

            //
            // ░░░
            // ░░░
            // █░░
            // ░█░
            // ░█░
            // Assert
            Assert.That(result, Is.EqualTo(RowProcessingResult.TWO_ROWS));

            var filledBoxes = field.Grid.Cast<SpaceBox>().Where(b => !b.IsOpen);

            Assert.That(filledBoxes.Count, Is.EqualTo(3));
            Assert.True(filledBoxes.Any(b => b.X == 1 && b.Y == 4));
            Assert.True(filledBoxes.Any(b => b.X == 1 && b.Y == 3));
            Assert.True(filledBoxes.Any(b => b.X == 0 && b.Y == 2));
        }
    }
}
