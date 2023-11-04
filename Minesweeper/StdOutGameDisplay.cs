namespace Minesweeper;

public interface IGameDisplay
{
    void DrawGame();
}

public class StdOutGameDisplay : IGameDisplay
{
    Minefield minefield;
    GameControler gameControler;

    bool firstDraw = true;

    public StdOutGameDisplay(Minefield minefield, GameControler controller)
    {
        this.minefield = minefield;
        this.gameControler = controller;
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
        if (!firstDraw)
        {
            Console.SetCursorPosition(0, Console.CursorTop - minefield.height);
        }
        firstDraw = false;

        for (int y = minefield.height - 1; y >= 0; y--)
        {
            for (int x = 0; x < minefield.width; x++)
            {
                if (x == gameControler.cursorX && y == gameControler.cursorY)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(GetPixelValue(minefield.GetStateAtPosition(x, y)));
                Console.ResetColor();
            }
            Console.Write(Environment.NewLine);
        }
    }

    private static String PadInteger(int i) => i > 9 ? i.ToString() : " " + i.ToString();
}
