using System.Text.RegularExpressions;

namespace Minesweeper;

class Minesweeper
{
    static void Main()
    {
        var field = new Minefield(9, 9);

        field.InitializeRandomBombs(10);


        var coordsRegex = new Regex(@"(\d+)\D+(\d+)");
        var display = new StdOutGameDisplay(field);
        while (true)
        {
            display.DrawGame();
            Console.Write("Enter position:");
            var input = Console.ReadLine();
            if (input is null)
            {
                continue;
            }
            var match = coordsRegex.Match(input);
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            field.UncoverTiles(x, y);
        }


        //the mine field should look like this now:
        //  01234
        //4|1X1
        //3|11111
        //2|2211X
        //1|XX111
        //0|X31

        // Game code...
    }
}
