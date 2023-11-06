namespace Minesweeper;

public interface IGameDisplay
{
    void DrawGame();
}

public class ConsoleGameDisplay : IGameDisplay
{
    Minefield minefield;
    IGameController gameControler;

    bool firstDraw = true;

    public ConsoleGameDisplay(Minefield minefield, IGameController controller)
    {
        this.minefield = minefield;
        this.gameControler = controller;
    }


    static private String GetPixelValue(TileState state)
    {
        if (state.isCovered)
        {
            return "\u2588\u2588";
        }
        if (state.isBomb)
        {
            return "💥";
        }
        if (state.nAdjecentBombs == 0)
        {
            return "  ";
        }
        return PadInteger(state.nAdjecentBombs);
    }

    static private String GetGameStateIcon(GameState gameState)
    {
        return gameState switch
        {
            GameState.Playing => "🙂",
            GameState.Lost => "😭",
            GameState.Won => "😎",
            _ => " ",
        };
    }

    public void DrawGame()
    {
        if (!firstDraw)
        {
            Console.SetCursorPosition(0, Console.CursorTop - minefield.height - 1);
        }
        firstDraw = false;

        Console.SetCursorPosition(minefield.width, Console.CursorTop);
        Console.WriteLine(GetGameStateIcon(minefield.GetGameState()));

        for (int y = minefield.height - 1; y >= 0; y--)
        {
            for (int x = 0; x < minefield.width; x++)
            {
                if (x == gameControler.CursorX && y == gameControler.CursorY)
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
