using System.Collections.Generic;

public class EnemiesDestroyStrategy
{
    private int chainLimit = 5;
    private BoardTile[,] tiles;

    public List<BoardTile> getDestroyedEnemies(BoardTile[,] tiles, int targetX, int targetY)
    {
        List<BoardTile> result = new List<BoardTile>();
        BoardTile target = tiles[targetX, targetY];
        this.tiles = tiles;

        addTile(target, result);
        findNeighbors(target, result);
        return result;
    }

    private void findNeighbors(BoardTile target, List<BoardTile> result)
    {
        int[,] sides = { {-1, 0}, {0, 1}, { 1, 0}, { 0, -1} };
        BoardTile tile;
        
        for (int i = 0; i < sides.GetLength(0); i++)
        {
            if (result.Count >= chainLimit) return;

            tile = getNeighbor(target, sides[i, 0], sides[i, 1]);
            if (tile != null && tile.Marked == false && matchValue(tile, target))
            {
                addTile(tile, result);
                findNeighbors(tile, result);
            }
        }
    }

    private BoardTile getNeighbor(BoardTile tile, int shiftX, int shiftY)
    {
        if (checkBoundary(tile.X + shiftX, tile.Y + shiftY) == false)
            return null;

        return tiles[tile.X + shiftX, tile.Y + shiftY];
    }

    private void addTile(BoardTile tile, List<BoardTile> list)
    {
        list.Add(tile);
        tile.Marked = true;
    }

    private bool matchValue(BoardTile tile1, BoardTile tile2)
    {
        return tile1.Value == tile2.Value;
    }

    private bool checkBoundary(int tileX, int tileY)
    {
        return tileX >= 0 && tileX < tiles.GetLength(0) && tileY >= 0 && tileY < tiles.GetLength(1);
    }
}
