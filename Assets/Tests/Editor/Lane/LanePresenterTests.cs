using NUnit.Framework;
using UnityEngine;
using Lane;
using Config;
using Tests.Mocks;

namespace Tests.Editor.Lane
{
    public class LanePresenterTests
    {
        private LaneConfig _config;
        private LaneModel _model;
        private GameObject _laneGO;

        private MockLaneView _mockView;
        private LanePresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _config = ScriptableObject.CreateInstance<LaneConfig>();
            typeof(LaneConfig).GetProperty(nameof(LaneConfig.ObstacleWidth))?.SetValue(_config, 1);
            typeof(LaneConfig).GetProperty(nameof(LaneConfig.Speed))?.SetValue(_config, 2f);
            typeof(LaneConfig).GetProperty(nameof(LaneConfig.Distance))?.SetValue(_config, 3);

            _model = new LaneModel(_config, LaneDirection.Right, -10, 10);

            _laneGO = new GameObject("MockLaneView");
            _mockView = _laneGO.AddComponent<MockLaneView>();

            var fakePrefabGO = new GameObject("FakeObstaclePrefab");
            var fakeObstacle = fakePrefabGO.AddComponent<MockObstacleView>();
            _mockView.ObstaclePrefab = fakeObstacle;

            _presenter = new LanePresenter(_model, _mockView);
        }

        [Test]
        public void FirstUpdate_SpawnsInitialObstacle()
        {
            _presenter.Update(0.016f);
            Assert.AreEqual(1, _laneGO.transform.childCount);
        }

        [Test]
        public void Obstacle_Reset_UsesCorrectPosition_WhenSpawned()
        {
            _presenter.Update(0.016f);

            var view = _laneGO.GetComponentInChildren<MockObstacleView>();

            float step = _config.ObstacleWidth + _config.Distance;
            float expectedX = -10f - _config.ObstacleWidth + step;

            Assert.AreEqual(expectedX, view.LastResetPos.x, 0.01f,
                $"ResetPosition was not called with the updated spawn X (expected {expectedX}, got {view.LastResetPos.x})");
        }

        [Test]
        public void SecondUpdate_SpawnsNewObstacle_WhenBeyondBoundary()
        {
            _presenter.Update(0.016f);
            _model.UpdateNextSpawnX();
            _presenter.Update(0.016f);
            Assert.AreEqual(2, _laneGO.transform.childCount);
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_laneGO);
        }
    }
}