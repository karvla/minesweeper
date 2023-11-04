namespace Minesweeper;

public class GameControler
{
    private Minefield minefield;

    public int cursorX { get; set; }
    public int cursorY { get; set; }

    public GameControler(Minefield minefield)
    {
        this.minefield = minefield;
        cursorX = 0;
        cursorY = 0;
    }

    public void GetUserInput()
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow:
                cursorY = Math.Min(minefield.height, cursorY + 1);
                break;
            case ConsoleKey.DownArrow:
                cursorY = Math.Max(0, cursorY - 1);
                break;
            case ConsoleKey.LeftArrow:
                cursorX = Math.Max(0, cursorX - 1);
                break;
            case ConsoleKey.RightArrow:
                cursorX = Math.Min(minefield.width, cursorX + 1);
                break;
            case ConsoleKey.Enter:
                minefield.UncoverTiles(cursorX, cursorY);
                break;
        };
    }


}
