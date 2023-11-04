namespace Minesweeper;

public interface IGameDisplay
{
    void DrawGame();
}

public class StdOutGameDisplay : IGameDisplay
{
    private Minefield minefield;
    private bool firstDraw = true;

    public StdOutGameDisplay(Minefield minefield)
    {
        this.minefield = minefield;
    }


    static private String GetPixelValue(TileState state)
    {
        if (state.isCovered)
        {
            return "⬛";
        }
        if (state.isBomb)
        {
            return "💣";
        }
        if (state.nAdjecentBombs == 0)
        {
            return "  ";
        }
        return PadInteger(state.nAdjecentBombs);
    }

    public void DrawGame()
    {
        var mapString = "";
        mapString += "  " + string.Concat(Enumerable.Range(0, minefield.width).Select(PadInteger)) + Environment.NewLine;

        if (!firstDraw)
        {
            firstDraw = false;
            Console.SetCursorPosition(0, Console.CursorTop - minefield.height - 2);
        }

        for (int y = minefield.height - 1; y >= 0; y--)
        {
            mapString += PadInteger(y) + '|';
            for (int x = 0; x < minefield.width; x++)
            {
                mapString += GetPixelValue(minefield.GetStateAtPosition(x, y));
            }
            mapString += Environment.NewLine;
        }
        Console.Write(mapString);
    }

    private static String PadInteger(int i) => i > 9 ? i.ToString() : " " + i.ToString();
}
