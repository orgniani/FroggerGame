using NUnit.Framework;
using UnityEngine;
using Player;
using Interfaces;
using Tests.Mocks;

namespace Tests.Editor.Player
{
    public class PlayerModelTests
    {
        private IGameConfig _config;

        [SetUp]
        public void Setup()
        {
            _config = new MockGameConfig();
        }

        [Test]
        public void Move_Upward_IncreasesY()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(0, 1);
            Assert.AreEqual(1f, model.CurrentY);
        }

        [Test]
        public void Move_Downward_DecreasesY()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(0, 1);
            model.Move(0, -1);
            Assert.AreEqual(0f, model.CurrentY);
        }

        [Test]
        public void Move_Right_IncreasesX()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(1, 0);
            Assert.AreEqual(1f, model.CurrentX);
        }

        [Test]
        public void Move_Left_DecreasesX()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(1, 0);
            model.Move(-1, 0);
            Assert.AreEqual(0f, model.CurrentX);
        }

        [Test]
        public void Move_ClampedToBoundaries()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(100, 100);
            Assert.AreEqual(_config.TopBoundary, model.CurrentY);
            Assert.AreEqual(_config.RightBoundary, model.CurrentX);
        }

        [Test]
        public void Reset_SetsPositionToStart()
        {
            var model = new PlayerModel(new Vector3(1, 1), _config);
            model.Move(-1, -1);
            model.Reset();
            Assert.AreEqual(1f, model.CurrentX);
            Assert.AreEqual(1f, model.CurrentY);
        }

        [Test]
        public void HasReachedGoal_WhenAtTop_ReturnsTrue()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(0, 100);
            Assert.IsTrue(model.HasReachedGoal);
        }

        [Test]
        public void Constructor_SetsInitialValuesCorrectly()
        {
            Vector3 startPos = new Vector3(2f, 3f);
            var model = new PlayerModel(startPos, _config);

            Assert.AreEqual(2f, model.CurrentX);
            Assert.AreEqual(3f, model.CurrentY);
            Assert.AreEqual(_config.LeftBoundary, model.MinX);
            Assert.AreEqual(_config.RightBoundary, model.MaxX);
            Assert.AreEqual(_config.TopBoundary, model.MaxY);
        }

        [Test]
        public void HasReachedGoal_WhenNotAtTop_ReturnsFalse()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            Assert.IsFalse(model.HasReachedGoal);
        }

        [Test]
        public void Move_ClampedToLeftBoundary()
        {
            var model = new PlayerModel(Vector3.zero, _config);
            model.Move(-100, 0);
            Assert.AreEqual(_config.LeftBoundary, model.CurrentX);
        }
    }
}
