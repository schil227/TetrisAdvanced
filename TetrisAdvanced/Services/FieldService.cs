using TetrisAdvanced.Data;
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

        public void HandleCompletedRows(Field field)
        {
            throw new System.NotImplementedException();
        }

        public bool MoveShape(Field field, MoveDirection direction, bool isForced)
        {
            var xOffset = 0;
            var yOffset = 0;

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

            var shadowShape = shapeService.CopyShape(field.ActiveShape);

            shapeService.MoveShape(shadowShape, xOffset, yOffset);

            if (CanMoveShape(field, shadowShape))
            {
                shapeService.MoveShape(field.ActiveShape, xOffset, yOffset);
            }
            else if (isForced)
            {
                foreach (var box in field.ActiveShape.Boxes)
                {
                    field.Grid[box.X + field.ShapeX, box.Y + field.ShapeY].IsOpen = false;
                }

                HandleCompletedRows(field);

                return true;
            }

            return false;
        }
    }
}
