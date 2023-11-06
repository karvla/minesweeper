namespace Minesweeper;

class Minesweeper
{
    static void Main()
    {
        Console.WriteLine("Welcome to minesweeper");
        Console.WriteLine("Use arrows to control cursor");
        Console.WriteLine("Press Enter to uncover tile");

        var height = 9;
        var width = 9;
        var nBombs = 10;

        Console.WriteLine($"There are {nBombs} mines. Uncover all other tiles to win!");

        var field = new Minefield(width, height);
        var controller = new ConsoleGameController(field);
        var display = new ConsoleGameDisplay(field, controller);
        field.InitializeRandomBombs(nBombs);

        while (field.GetGameState() == GameState.Playing)
        {
            display.DrawGame();
            controller.HandleUserInput();
        }

        display.DrawGame();
        if (field.GetGameState() == GameState.Won)
        {
            Console.WriteLine("congratulations you won!");
        }
        else
        {
            Console.WriteLine("you lost!");
        }


    }
}
