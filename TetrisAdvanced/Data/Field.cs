namespace TetrisAdvanced.Data
{
    public class Field
    {
        int Width { get; set; }
        int Height { get; set; }
        SpaceBox[,] Grid { get; set; }
    }
}
