namespace Minesweeper;

public class StdOutGameDisplay
{
    private Minefield minefield;

    public StdOutGameDisplay(Minefield minefield)
    {
        this.minefield = minefield;
    }

    public void DrawGame()
    {

    }

    static String GetMapString<T>(T[,] map, Func<T, char> convertFn)
    {
        var mapString = "";
        var height = map.GetLength(0);
        var width = map.GetLength(1);
        mapString += "  " + string.Concat(Enumerable.Range(0, width).Select(i => i.ToString())) + Environment.NewLine;
        for (int y = height - 1; y >= 0; y--)
        {
            mapString += y.ToString() + '|';
            for (int x = 0; x < width; x++)
            {
                mapString += convertFn(map[x, y]);
            }
            mapString += Environment.NewLine;
        }
        return mapString;
    }
}
