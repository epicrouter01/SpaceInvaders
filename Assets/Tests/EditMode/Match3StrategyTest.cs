using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Match3StrategyTest
    {
        private EnemiesDestroyStrategy strategy = new EnemiesDestroyStrategy();

        // A Test behaves as an ordinary method
        [Test]
        public void chainLimitTest()
        {
            BoardTile[,] tiles = createBoardTiles(new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, {1, 1, 1} });
            List <BoardTile> list = strategy.getDestroyedEnemies(tiles, 1, 1);
            Assert.AreEqual(list.Count, 5);
        }

        [Test]
        public void chainingValuesTest()
        {
            BoardTile[,] tiles = createBoardTiles(new int[,] { 
                { 1, 1, 1 }, 
                { 1, 2, 1 }, 
                { 2, 2, 2 } 
            });

            List<BoardTile> list = strategy.getDestroyedEnemies(tiles, 2, 1);
            Assert.AreEqual(list.Count, 4);

            foreach (BoardTile tile in list)
                Assert.AreEqual(tile.Value, 2);

            Assert.IsTrue(haveTilePosition(list, 1, 1));
            Assert.IsTrue(haveTilePosition(list, 2, 0));
            Assert.IsTrue(haveTilePosition(list, 2, 1));
            Assert.IsTrue(haveTilePosition(list, 2, 2));
        }

        private bool haveTilePosition(List<BoardTile> list, int x, int y)
        {
            foreach (BoardTile tile in list)
                if (tile.X == x && tile.Y == y)
                    return true;

            return false;
        }

        private BoardTile[,] createBoardTiles(int[,] values)
        {
            BoardTile[,] result = new BoardTile[values.GetLength(0), values.GetLength(1)];

            for (int i = 0; i < values.GetLength(0); i++)
                for (int j = 0; j < values.GetLength(1); j++)
                    result[i, j] = new BoardTile(i, j, values[i, j]);

            return result;
        }
    }
}
