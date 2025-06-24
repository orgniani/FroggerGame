using NUnit.Framework;
using Lane;
using Config;
using UnityEngine;

namespace Tests.Editor.Lane
{
    public class LaneModelTests
    {
        private LaneConfig _config;

        [SetUp]
        public void Setup()
        {
            _config = ScriptableObject.CreateInstance<LaneConfig>();
            typeof(LaneConfig).GetProperty("ObstacleWidth").SetValue(_config, 2);
            typeof(LaneConfig).GetProperty("Speed").SetValue(_config, 5f);
            typeof(LaneConfig).GetProperty("Distance").SetValue(_config, 3);
        }

        [Test]
        public void Constructor_RightDirection_SetsInitialSpawnXCorrectly()
        {
            var model = new LaneModel(_config, LaneDirection.Right, -10, 10);
            Assert.AreEqual(-12f, model.LastSpawnedX);
        }

        [Test]
        public void Constructor_LeftDirection_SetsInitialSpawnXCorrectly()
        {
            var model = new LaneModel(_config, LaneDirection.Left, -10, 10);
            Assert.AreEqual(12f, model.LastSpawnedX);
        }

        [Test]
        public void UpdateNextSpawnX_RightDirection_IncreasesX()
        {
            var model = new LaneModel(_config, LaneDirection.Right, -10, 10);
            float prevX = model.LastSpawnedX;
            model.UpdateNextSpawnX();
            Assert.AreEqual(prevX + 5f, model.LastSpawnedX);
        }

        [Test]
        public void UpdateNextSpawnX_LeftDirection_DecreasesX()
        {
            var model = new LaneModel(_config, LaneDirection.Left, -10, 10);
            float prevX = model.LastSpawnedX;
            model.UpdateNextSpawnX();
            Assert.AreEqual(prevX - 5f, model.LastSpawnedX);
        }

        [Test]
        public void GetSpeed_ReturnsPositiveIfRight()
        {
            var model = new LaneModel(_config, LaneDirection.Right, -10, 10);
            Assert.AreEqual(5f, model.GetSpeed());
        }

        [Test]
        public void GetSpeed_ReturnsNegativeIfLeft()
        {
            var model = new LaneModel(_config, LaneDirection.Left, -10, 10);
            Assert.AreEqual(-5f, model.GetSpeed());
        }

        [Test]
        public void GetLeftBoundWithMargin_ReturnsCorrect()
        {
            var model = new LaneModel(_config, LaneDirection.Left, -10, 10);
            Assert.AreEqual(-12f, model.GetLeftBoundWithMargin());
        }

        [Test]
        public void GetRightBoundWithMargin_ReturnsCorrect()
        {
            var model = new LaneModel(_config, LaneDirection.Left, -10, 10);
            Assert.AreEqual(12f, model.GetRightBoundWithMargin());
        }
    }
}
