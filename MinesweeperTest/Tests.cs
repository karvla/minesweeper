namespace MinesweeperTest;
using Minesweeper;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestAdjecentBombs()
    {
        var field = new Minefield();

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        //the mine field should look like this now:
        //  01234
        //4|1X1
        //3|11111
        //2|2211X
        //1|XX111
        //0|X31

        var expected = new int[5, 5] {
            { 2, 3, 1, 0, 0 },
            { 2, 2, 1, 1, 1 },
            { 2, 2, 1, 1, 0 },
            { 1, 1, 1, 1, 1 },
            { 1, 0, 1, 0, 0 },
        };

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Assert.AreEqual(expected[y, x], field.GetAdjecentBombs(x, y), $"for x: {x}, y: {y}");
            }
        }
    }
}