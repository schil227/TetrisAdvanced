using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;
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
                var boxX = box.X + field.ShapeX;
                var boxY = box.Y + field.ShapeY;

                if (boxX < 0 || boxX >= field.Width ||
                    boxY < 0 || boxY >= field.Height ||
                    !field.Grid[box.X + field.ShapeX, box.Y + field.ShapeY].IsOpen)
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

                if (boxesFilled == field.Width)
                {
                    // Row filled
                    rowsCompleted++;
                }
                else if (rowsCompleted > 0)
                {
                    // Shift down
                    for (int j = 0; j < field.Width; j++)
                    {
                        field.Grid[i + rowsCompleted, j] = field.Grid[i, j];
                        field.Grid[i + rowsCompleted, j].Y = i + rowsCompleted;
                    }
                }
            }

            //clean up rows at the top
            for (int i = 0; i < rowsCompleted; i++)
            {
                for (int j = 0; j < field.Width; j++)
                {
                    field.Grid[i, j] = new SpaceBox
                    {
                        Y = i,
                        X = j,
                        IsOpen = true
                    };
                }
            }

            return (RowProcessingResult)rowsCompleted;
        }
    }
}
