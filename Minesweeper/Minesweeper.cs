namespace Minesweeper;

class Minesweeper
{
    static void Main()
    {
        Console.WriteLine("Welcome to minesweeper");
        Console.WriteLine("Use arrows to control cursor");
        Console.WriteLine("Press Enter to uncover tile");

        var field = new Minefield(9, 9);
        var controller = new ConsoleGameController(field);
        var display = new ConsoleGameDisplay(field, controller);
        field.InitializeRandomBombs(10);

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
