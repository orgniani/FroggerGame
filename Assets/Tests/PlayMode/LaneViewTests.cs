using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Lane;
using Config;
using Obstacle;

namespace Tests.PlayMode
{
    public class LaneViewTests
    {
        private GameObject _laneGO;
        private LaneView _laneView;
        private LanePresenter _presenter;
        private ObstacleView _obstaclePrefab;
        private LaneConfig _laneConfig;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _laneGO = new GameObject("LaneTestObject");
            _laneView = _laneGO.AddComponent<LaneView>();
            _obstaclePrefab = CreateObstaclePrefab();
            _laneConfig = CreateLaneConfig();

            var obstacleField = typeof(LaneView).GetField("obstaclePrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            obstacleField?.SetValue(_laneView, _obstaclePrefab);

            _laneView.Invoke("Awake", 0f);

            var model = new LaneModel(_laneConfig, LaneDirection.Right, -10f, 10f);
            _presenter = new LanePresenter(model, _laneView);
            _laneView.SetPresenter(_presenter);

            yield return null;
        }

        [UnityTest]
        public IEnumerator LaneView_SpawnsInitialObstaclesOnStart()
        {
            yield return null;

            _laneView.enabled = false;
            Assert.GreaterOrEqual(_laneGO.transform.childCount, 1);
        }

        [UnityTest]
        public IEnumerator LaneView_SpawnsObstaclesAfterTime()
        {
            yield return new WaitForSeconds(0.5f);
            Assert.GreaterOrEqual(_laneGO.transform.childCount, 2);
        }

        [UnityTest]
        public IEnumerator Obstacle_MovesInCorrectDirection()
        {
            var obstacle = _laneGO.GetComponentInChildren<ObstacleView>();
            float startX = obstacle.transform.position.x;

            yield return new WaitForSeconds(0.5f);

            float endX = obstacle.transform.position.x;
            Assert.Greater(endX, startX);
        }

        [UnityTest]
        public IEnumerator Obstacle_IsFlipped_WhenLaneIsLeft()
        {
            Object.DestroyImmediate(_laneGO);

            _laneGO = new GameObject("LeftLaneTestObject");
            _laneView = _laneGO.AddComponent<LaneView>();

            var obstacleField = typeof(LaneView).GetField("obstaclePrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            obstacleField?.SetValue(_laneView, _obstaclePrefab);
            _laneView.Invoke("Awake", 0f);

            var model = new LaneModel(_laneConfig, LaneDirection.Left, -10f, 10f);
            _presenter = new LanePresenter(model, _laneView);
            _laneView.SetPresenter(_presenter);

            yield return null;

            var obstacle = _laneGO.GetComponentInChildren<ObstacleView>();
            Assert.IsTrue(obstacle.GetComponent<SpriteRenderer>().flipX);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (_laneGO != null)
                Object.Destroy(_laneGO);
            yield return null;
        }

        private ObstacleView CreateObstaclePrefab()
        {
            var go = new GameObject("ObstaclePrefab");
            go.AddComponent<SpriteRenderer>();
            var view = go.AddComponent<ObstacleView>();
            view.Invoke("Awake", 0f);
            return view;
        }

        private LaneConfig CreateLaneConfig()
        {
            var config = ScriptableObject.CreateInstance<LaneConfig>();

            typeof(LaneConfig).GetProperty(nameof(LaneConfig.ObstacleWidth))
                ?.SetValue(config, 1);

            typeof(LaneConfig).GetProperty(nameof(LaneConfig.Distance))
                ?.SetValue(config, 2);

            typeof(LaneConfig).GetProperty(nameof(LaneConfig.Speed))
                ?.SetValue(config, 4f);

            typeof(LaneConfig).GetProperty(nameof(LaneConfig.Sprite))
                ?.SetValue(config, Sprite.Create(new Texture2D(32, 32), new Rect(0, 0, 32, 32), Vector2.zero));

            return config;
        }

    }
}