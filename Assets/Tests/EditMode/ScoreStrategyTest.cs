using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ScoreStrategyTest
    {
        private ScoreStrategy strategy = new ScoreStrategy();
        // A Test behaves as an ordinary method
        [Test]
        public void ScoreStrategyTestSimplePasses()
        {
            Assert.AreEqual(strategy.getScore(0), 0);
            Assert.AreEqual(strategy.getScore(1), 10);
            Assert.AreEqual(strategy.getScore(2), 40);
            Assert.AreEqual(strategy.getScore(3), 90);
            Assert.AreEqual(strategy.getScore(4), 200);
            Assert.AreEqual(strategy.getScore(5), 400);
        }
    }
}
