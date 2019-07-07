using System;
using System.Linq;
using System.Text;
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

            for (int y = field.Height - 1; y >= 0; y--)
            {
                int boxesFilled = 0;

                for (int x = 0; x < field.Width; x++)
                {
                    if (!field.Grid[x, y].IsOpen)
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
                    for (int x = 0; x < field.Width; x++)
                    {
                        field.Grid[x, y + rowsCompleted] = field.Grid[x, y];
                        field.Grid[x, y + rowsCompleted].Y = y + rowsCompleted;
                    }
                }
            }

            //clean up rows at the top
            for (int y = 0; y < rowsCompleted; y++)
            {
                for (int x = 0; x < field.Width; x++)
                {
                    field.Grid[x, y] = new SpaceBox
                    {
                        Y = y,
                        X = x,
                        IsOpen = true
                    };
                }
            }

            return (RowProcessingResult)rowsCompleted;
        }

        public void DrawField(Field field)
        {
            Console.Clear();

            var drawnField = new StringBuilder();

            for (int y = 0; y < field.Height; y++)
            {
                drawnField.Append("║");

                for (int x = 0; x < field.Width; x++)
                {
                    if (!field.Grid[x, y].IsOpen || field.ActiveShape.Boxes.Any(b => b.X == x && b.Y == y))
                    {
                        drawnField.Append('█');
                    }
                    else
                    {
                        drawnField.Append(' ');
                    }
                }
                drawnField.Append("║");
                Console.WriteLine(drawnField);

                drawnField.Clear();
            }

            drawnField.Append('╚');

            for (int x = 0; x < field.Width; x++)
            {
                drawnField.Append('═');
            }

            drawnField.Append('╝');

            Console.WriteLine(drawnField);

            Console.WriteLine();

        }
    }
}
