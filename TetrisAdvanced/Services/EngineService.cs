using System;
using System.Diagnostics;
using System.Linq;
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

            var moveStep = new Stopwatch();
            moveStep.Start();

            while (!gameOver)
            {
                if (moveStep.ElapsedMilliseconds > 1500)
                {
                    moveStep.Restart();

                    var activeShapeStatus = MoveShape(engine.field, MoveDirection.DOWN);
                    fieldService.DrawField(engine.field);

                    if (activeShapeStatus == ActiveShapeStatus.INACTIVE)
                    {
                        SetNextActiveShape(engine);
                    }
                }
                else
                {

                }
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

        private void SetNextActiveShape(Engine engine)
        {
            engine.field.ActiveShape = engine.ShapeTypes.ElementAt(engine.random.Next() % engine.ShapeTypes.Count);
        }
    }
}
