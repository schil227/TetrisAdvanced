using System;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerators;
using TetrisAdvanced.Interfaces;

namespace TetrisAdvanced.Services
{
    public class FieldService : IFieldService
    {
        private readonly IShapeService shapeService;

        public FieldService(IShapeService shapeService)
        {
            this.shapeService = shapeService;
        }

        public bool CanMoveShape(Field field, Shape shape)
        {
            foreach (var box in shape.Boxes)
            {
                if (!field.Grid[box.X + field.ShapeX, box.Y + field.ShapeY].IsOpen)
                {
                    return false;
                }
            }

            return true;
        }

        public RowProcessingResult HandleCompletedRows(Field field)
        {
            int rowsCompleted = 0;

            for (int i = field.Height - 1; i >= 0; i--)
            {
                int boxesFilled = 0;

                for (int j = 0; j < field.Width; j++)
                {
                    if (!field.Grid[i, j].IsOpen)
                    {
                        boxesFilled++;
                    }
                }

                if (boxesFilled == 0)
                {
                    // Empty row, no further processing needed
                    break;
                }
                else if (boxesFilled == field.Width)
                {
                    // Row filled
                    rowsCompleted = 1;
                }
                else if (rowsCompleted > 0)
                {
                    // Shift down
                    for (int j = 0; j < field.Width; j++)
                    {
                        field.Grid[i, j].Y += rowsCompleted;
                    }
                }
            }

            return (RowProcessingResult)Enum.ToObject(typeof(RowProcessingResult), 1);
        }

        public ActiveShapeStatus MoveShape(Field field, MoveDirection direction, bool isForced)
        {
            int xOffset, yOffset;

            CalculateOffset(direction, out xOffset, out yOffset);

            var shadowShape = shapeService.CopyShape(field.ActiveShape);

            shapeService.MoveShape(shadowShape, xOffset, yOffset);

            if (CanMoveShape(field, shadowShape))
            {
                shapeService.MoveShape(field.ActiveShape, xOffset, yOffset);
            }
            else if (isForced || direction == MoveDirection.DOWN)
            {
                foreach (var box in field.ActiveShape.Boxes)
                {
                    field.Grid[box.X + field.ShapeX, box.Y + field.ShapeY].IsOpen = false;
                }

                HandleCompletedRows(field);

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
