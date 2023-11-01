namespace Minesweeper;

public class Minefield
{
    const int width = 5;
    const int height = 5;
    bool[,] bombs = new bool[height, width];

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

    public int GetAdjecentBombs(int x, int y)
    {
        return (from xOffset in new List<int> { 1, 0, -1 }
                from yOffset in new List<int> { 1, 0, -1 }
                where (!(xOffset == 0 && yOffset == 0))
                where (IsWithinBoundary(x + xOffset, y + yOffset))
                select (IsBomb(x + xOffset, y + yOffset))).Count(isBomb => isBomb);
    }
}
