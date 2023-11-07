namespace Minesweeper;

public struct TileState
{
    public readonly bool isBomb;
    public readonly bool isCovered;
    public readonly int nAdjecentBombs;

    public TileState(bool isBomb, bool isCovered, int nAdjecentBombs)
    {
        this.isBomb = isBomb;
        this.isCovered = isCovered;
        this.nAdjecentBombs = nAdjecentBombs;
    }
}

public enum GameState
{
    Playing,
    Lost,
    Won,
}

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
        if (!IsWithinBoundary(x, y))
        {
            return;
        }
        bombs[y, x] = true;
    }

    public bool IsBomb(int x, int y)
    {
        if (!IsWithinBoundary(x, y))
        {
            return false;
        }
        return bombs[y, x];
    }

    public bool IsUncovered(int x, int y)
    {
        if (!IsWithinBoundary(x, y))
        {
            return true;
        }
        return uncoveredTiles[y, x];
    }

    private bool IsWithinBoundary(int x, int y) => (0 <= x && x < width && 0 <= y && y < height);

    public TileState GetStateAtPosition(int x, int y)
    {
        return new TileState(isCovered: !IsUncovered(x, y), isBomb: IsBomb(x, y), nAdjecentBombs: GetAdjecentBombs(x, y));
    }

    public GameState GetGameState()
    {
        int nUncovered = 0;
        int nBombs = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var isBomb = IsBomb(x, y);
                var isUncovered = IsUncovered(x, y);
                if (isBomb)
                {
                    nBombs++;
                }
                if (isUncovered)
                {
                    nUncovered++;
                }
                if (isUncovered && isBomb)
                {
                    return GameState.Lost;
                }
            }
        }
        if (nUncovered == width * height - nBombs)
        {
            return GameState.Won;
        }
        return GameState.Playing;
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
