namespace TetrisAdvanced.Data
{
    public class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ShapeX { get; set; }
        public int ShapeY { get; set; }

        public Shape ActiveShape { get; set; }
        public SpaceBox[,] Grid { get; set; }
    }
}
