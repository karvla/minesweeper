namespace Minesweeper;


public class Minefield
{
    public int width { get; }

    public int height { get; }

    public int NumberOfBombs
    {
        get => bombs.Cast<bool>().Count(b => b);
    }

    private readonly bool[,] bombs;

    private bool[,] uncoveredTiles;

    public Minefield(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.bombs = new bool[height, width];
        this.uncoveredTiles = new bool[height, width];
    }

    public void SetBomb(int x, int y)
    {
        bombs[y, x] = true;
    }

    public bool IsBomb(int x, int y)
    {
        return bombs[y, x];
    }

    private bool IsWithinBoundary(int x, int y)
    {
        return (0 <= x && x < width && 0 <= y && y < height);

    }

    public bool IsUncovered(int x, int y)
    {
        return uncoveredTiles[y, x];
    }

    public void UncoverTiles(int x, int y)
    {
        if (IsUncovered(x, y))
        {
            return;
        }

        uncoveredTiles[y, x] = true;
        if (GetAdjecentBombs(x, y) > 0)
        {
            return;
        }


        GetAdjecentTilePositions(x, y)
            .ForEach(posistion => UncoverTiles(posistion.Item1, posistion.Item2));

    }

    public int GetAdjecentBombs(int x, int y)
    {
        return GetAdjecentTilePositions(x, y).Where((posistion) => IsBomb(posistion.Item1, posistion.Item2)).Count();
    }


    private List<(int, int)> GetAdjecentTilePositions(int x, int y)
    {
        return (from xOffset in new List<int> { 1, 0, -1 }
                from yOffset in new List<int> { 1, 0, -1 }
                where (!(xOffset == 0 && yOffset == 0))
                where (IsWithinBoundary(x + xOffset, y + yOffset))
                select (x + xOffset, y + yOffset)).ToList();
    }

    public void InitializeRandomBombs(int numberOfBombs)
    {
        numberOfBombs = Math.Min(numberOfBombs, width * height);
        var random = new Random();
        var allPositions =
            (from x in Enumerable.Range(0, width)
             from y in Enumerable.Range(0, height)
             select (x, y)).ToList();

        while (numberOfBombs > 0)
        {
            var bombPos = random.Next(allPositions.Count);
            var randomPos = allPositions[bombPos];
            SetBomb(randomPos.x, randomPos.y);
            allPositions.RemoveAt(bombPos);
            numberOfBombs--;
        }
    }

}
