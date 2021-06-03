using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SpeedStrategyTest
    {
        private SpeedCalculationStrategy strategy = new SpeedCalculationStrategy();

        // A Test behaves as an ordinary method
        [Test]
        public void SpeedStrategyTestSimplePasses()
        {
            Assert.AreEqual(strategy.calculateSpeed(2, 10, 2, 6), 5.2f);
            Assert.AreEqual(strategy.calculateSpeed(3, 10, 2, 6), 4.8f);
            Assert.AreEqual(strategy.calculateSpeed(8, 10, 2, 6), 2.8f);
            Assert.AreEqual(strategy.calculateSpeed(10, 10, 2, 6), 2f);
        }
    }
}
