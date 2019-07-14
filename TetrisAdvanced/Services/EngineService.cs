using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;
using TetrisAdvanced.Interfaces;
using TetrisAdvanced.Interfaces.Factories;

namespace TetrisAdvanced.Services
{
    public class EngineService : IEngineService
    {
        private readonly IEngineFactory engineFactory;
        private readonly IFieldService fieldService;
        private readonly IShapeService shapeService;
        private readonly IInputService inputService;

        public EngineService(
            IEngineFactory engineFactory,
            IFieldService fieldService,
            IShapeService shapeService,
            IInputService inputService
            )
        {
            this.engineFactory = engineFactory;
            this.fieldService = fieldService;
            this.shapeService = shapeService;
            this.inputService = inputService;
        }

        public void Run()
        {
            var gameOver = false;

            var engine = engineFactory.CreateEngine(8, 20);
            SetNextActiveShape(engine);

            var inputTask = Task.Factory.StartNew(() => inputService.HandleCommand(engine));

            var moveStep = new Stopwatch();
            moveStep.Start();

            while (!gameOver)
            {
                if (moveStep.ElapsedMilliseconds > Math.Max((int)(1500 - (engine.rowsCompleted / 10) * 100), 250))
                {
                    var activeShapeStatus = MoveShape(engine.field, MoveDirection.DOWN);
                    fieldService.DrawField(engine.field, engine.rowsCompleted);

                    moveStep.Restart();

                    gameOver = HandleMovedShape(activeShapeStatus, engine);
                }
                else if (engine.keyPressed != KeyInput.NO_COMMAND)
                {
                    Console.WriteLine(engine.keyPressed);

                    //hook up all the commands
                    switch (engine.keyPressed)
                    {
                        case KeyInput.QUIT:
                            gameOver = true;
                            break;
                        case KeyInput.MOVE_LEFT:
                            {
                                var activeShapeStatus = MoveShape(engine.field, MoveDirection.LEFT);
                                fieldService.DrawField(engine.field, engine.rowsCompleted);

                                gameOver = HandleMovedShape(activeShapeStatus, engine);
                                break;
                            }
                        case KeyInput.MOVE_RIGHT:
                            {
                                var activeShapeStatus = MoveShape(engine.field, MoveDirection.RIGHT);
                                fieldService.DrawField(engine.field, engine.rowsCompleted);

                                gameOver = HandleMovedShape(activeShapeStatus, engine);
                                break;
                            }
                        case KeyInput.MOVE_DOWN:
                            {
                                var activeShapeStatus = MoveShape(engine.field, MoveDirection.DOWN);
                                fieldService.DrawField(engine.field, engine.rowsCompleted);
                                moveStep.Restart();

                                gameOver = HandleMovedShape(activeShapeStatus, engine);
                                break;
                            }
                        case KeyInput.ROTATE_LEFT:
                            RotateShape(engine.field, RotationDirection.COUNTER_CLOCKWISE);
                            fieldService.DrawField(engine.field, engine.rowsCompleted);
                            break;
                        case KeyInput.ROTATE_RIGHT:
                            RotateShape(engine.field, RotationDirection.CLOCKWISE);
                            fieldService.DrawField(engine.field, engine.rowsCompleted);
                            break;
                        default:
                            break;
                    }

                    engine.keyPressed = KeyInput.NO_COMMAND;
                }
            }

            Console.WriteLine("Game over.");
        }

        public void RotateShape(Field field, RotationDirection direction)
        {
            var shadowShape = shapeService.CopyShape(field.ActiveShape);

            shapeService.Rotate(shadowShape, direction);

            if (fieldService.CanMoveShape(field, shadowShape))
            {
                shapeService.Rotate(field.ActiveShape, direction);
            }
        }

        public ActiveShapeStatus MoveShape(Field field, MoveDirection direction)
        {
            int xOffset, yOffset;

            CalculateOffset(direction, out xOffset, out yOffset);

            var shadowShape = shapeService.CopyShape(field.ActiveShape);

            shapeService.MoveShape(shadowShape, xOffset, yOffset);

            if (fieldService.CanMoveShape(field, shadowShape))
            {
                shapeService.MoveShape(field.ActiveShape, xOffset, yOffset);
            }
            else if (direction == MoveDirection.DOWN)
            {
                foreach (var box in field.ActiveShape.Boxes)
                {
                    field.Grid[box.X + field.ShapeX, box.Y + field.ShapeY].IsOpen = false;
                }

                return ActiveShapeStatus.INACTIVE;
            }

            return ActiveShapeStatus.ACTIVE;
        }

        private void CalculateOffset(MoveDirection direction, out int xOffset, out int yOffset)
        {
            xOffset = 0;
            yOffset = 0;

            if (direction == MoveDirection.DOWN)
            {
                yOffset = 1;
            }
            else if (direction == MoveDirection.LEFT)
            {
                xOffset = -1;
            }
            else
            {
                xOffset = 1;
            }
        }

        private bool HandleMovedShape(ActiveShapeStatus shapeStatus, Engine engine)
        {
            if (shapeStatus == ActiveShapeStatus.ACTIVE)
            {
                return false;
            }

            var rowsCompleted = fieldService.HandleCompletedRows(engine.field);

            engine.rowsCompleted += (int)rowsCompleted;

            SetNextActiveShape(engine);

            return !fieldService.CanMoveShape(engine.field, engine.field.ActiveShape);
        }

        private void SetNextActiveShape(Engine engine)
        {
            engine.field.ActiveShape = shapeService.CopyShape(engine.ShapeTypes.ElementAt(engine.random.Next() % engine.ShapeTypes.Count));
        }
    }
}
