namespace Minesweeper;

class Minesweeper
{
    static void Main()
    {
        Console.WriteLine("Welcome to minesweeper");
        var field = new Minefield(9, 9);

        field.InitializeRandomBombs(5);

        var controller = new ConsoleGameController(field);
        var display = new ConsoleGameDisplay(field, controller);

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
