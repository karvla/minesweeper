using System.Text.RegularExpressions;

namespace Minesweeper;

class Minesweeper
{
    static void Main()
    {
        var field = new Minefield(9, 9);

        field.InitializeRandomBombs(10);

        var controller = new ConsoleGameController(field);
        var display = new ConsoleGameDisplay(field, controller);
        while (true)
        {
            display.DrawGame();
            controller.HandleUserInput();
        }

    }
}
