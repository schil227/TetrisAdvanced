using System;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerators;
using TetrisAdvanced.Interfaces;

namespace TetrisAdvanced.Services
{
    public class EngineService : IEngineService
    {
        private readonly IFieldService fieldService;
        private readonly IShapeService shapeService;

        public EngineService(
            IFieldService fieldService,
            IShapeService shapeService
            )
        {
            this.fieldService = fieldService;
            this.shapeService = shapeService;
        }

        public void Run()
        {
            throw new NotImplementedException();
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
    }
}
