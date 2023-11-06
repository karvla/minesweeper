namespace Minesweeper;

public interface IGameController
{
    int CursorX { get; }
    int CursorY { get; }
    void HandleUserInput();
}


public class ConsoleGameController : IGameController
{
    private Minefield minefield;

    public int CursorX { get; set; }
    public int CursorY { get; set; }

    public ConsoleGameController(Minefield minefield)
    {
        this.minefield = minefield;
        CursorX = 0;
        CursorY = 0;
    }

    public void HandleUserInput()
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow:
                CursorY = Math.Min(minefield.height, CursorY + 1);
                break;
            case ConsoleKey.DownArrow:
                CursorY = Math.Max(0, CursorY - 1);
                break;
            case ConsoleKey.LeftArrow:
                CursorX = Math.Max(0, CursorX - 1);
                break;
            case ConsoleKey.RightArrow:
                CursorX = Math.Min(minefield.width, CursorX + 1);
                break;
            case ConsoleKey.Enter:
                minefield.UncoverTiles(CursorX, CursorY);
                break;
        };
    }


}
