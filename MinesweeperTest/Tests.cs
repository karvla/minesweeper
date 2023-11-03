namespace MinesweeperTest;
using Minesweeper;

[TestClass]
public class Tests
{
    const int width = 5;
    const int height = 5;

    [TestMethod]
    public void TestAdjecentBombs()
    {
        var field = new Minefield(width, height);

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

        var expected = new int[width, height] {
            { 2, 3, 1, 0, 0 },
            { 2, 2, 1, 1, 1 },
            { 2, 2, 1, 1, 0 },
            { 1, 1, 1, 1, 1 },
            { 1, 0, 1, 0, 0 },
        };

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                Assert.AreEqual(expected[y, x], field.GetAdjecentBombs(x, y), $"for x: {x}, y: {y}");
            }
        }
    }

    [TestMethod]
    public void TestUncoverTiles()
    {
        var field = new Minefield(5, 5);

        field.SetBomb(0, 1);
        field.SetBomb(1, 0);
        field.SetBomb(1, 1);
        field.UncoverTiles(2, 2);

        //the mine field should look like this now:
        //  012
        //2|???  
        //1|???  
        //0|??   

        Assert.IsFalse(field.IsUncovered(0, 0));
        Assert.IsFalse(field.IsUncovered(1, 0));
        Assert.IsFalse(field.IsUncovered(2, 0));

        Assert.IsFalse(field.IsUncovered(0, 1));
        Assert.IsFalse(field.IsUncovered(1, 1));
        Assert.IsFalse(field.IsUncovered(2, 1));

        Assert.IsFalse(field.IsUncovered(0, 2));
        Assert.IsFalse(field.IsUncovered(1, 2));
        Assert.IsTrue(field.IsUncovered(2, 2));
    }
}
