using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Player;
using System.Reflection;

namespace Tests.PlayMode
{
    public class PlayerCollisionTests
    {
        private GameObject _playerGO;
        private GameObject _obstacleGO;
        private PlayerView _view;
        private Rigidbody2D _playerRb;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            Physics2D.simulationMode = SimulationMode2D.Script;

            _playerGO = new GameObject("Player");
            _view = _playerGO.AddComponent<PlayerView>();
            _playerRb = _playerGO.AddComponent<Rigidbody2D>();
            _playerRb.gravityScale = 0f; // prevent gravity interference

            var playerCol = _playerGO.AddComponent<BoxCollider2D>();
            playerCol.isTrigger = true;

            _obstacleGO = new GameObject("Obstacle");
            var obstacleCol = _obstacleGO.AddComponent<BoxCollider2D>();
            obstacleCol.isTrigger = true;

            _obstacleGO.layer = LayerMask.NameToLayer("Obstacle");

            LayerMask mask = new LayerMask { value = LayerMask.GetMask("Obstacle") };
            typeof(PlayerView)
                .GetField("obstacleLayerMask", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(_view, mask);

            _playerGO.transform.position = new Vector3(-1f, 0f, 0f);
            _obstacleGO.transform.position = Vector3.zero;

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerHitsObstacle_InvokesEvent()
        {
            bool hitDetected = false;
            _view.OnObstacleHit.AddListener(() => hitDetected = true);

            _playerRb.linearVelocity = Vector2.right;

            for (int i = 0; i < 5; i++)
            {
                Physics2D.Simulate(Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            Assert.IsTrue(hitDetected, "Expected OnObstacleHit to be triggered, but it wasn't.");
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_playerGO);
            Object.Destroy(_obstacleGO);
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
            yield return null;
        }
    }
}