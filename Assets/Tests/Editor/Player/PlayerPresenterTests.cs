using NUnit.Framework;
using UnityEngine;
using Interfaces;
using Player;
using Health;
using Tests.Editor.Mocks;

namespace Tests.Editor.Player
{
    public class PlayerPresenterTests
    {
        private MockGameConfig _config;
        private PlayerModel _model;
        private HealthModel _health;

        private MockPlayerView _mockView;
        private MockInputManager _mockInput;

        private PlayerPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _config = new MockGameConfig();
            _model = new PlayerModel(Vector3.zero, _config);
            _health = new HealthModel(_config.MaxLives);

            _mockView = new MockPlayerView();
            _mockInput = new MockInputManager();

            _presenter = new PlayerPresenter(_model, _mockView, _health, _mockInput, _config.InputThreshold);
        }

        [Test]
        public void HandleMoveInput_UpdatesModelAndView()
        {
            _mockInput.SimulateInput(Vector2.up);

            Assert.AreEqual(1f, _model.CurrentY);
            Assert.AreEqual(1f, _mockView.LastY);
        }

        [Test]
        public void HandleMoveInput_SetsFacingDirection()
        {
            _mockInput.SimulateInput(Vector2.left);

            Assert.AreEqual(-1, _mockView.LastFacing);
        }

        [Test]
        public void ObstacleHit_ReducesHealth()
        {
            _mockView.OnObstacleHit.Invoke();

            Assert.AreEqual(_config.MaxLives - 1, _health.CurrentLives);
        }

        [Test]
        public void ObstacleHit_TriggersGameOverAtZeroHealth()
        {
            _health.TakeDamage(_config.MaxLives - 1);

            bool triggered = false;
            _presenter.OnGameOverTriggered.AddListener(() => triggered = true);

            _mockView.OnObstacleHit.Invoke();

            Assert.IsTrue(triggered);
            Assert.IsTrue(_mockView.PlayedDead);
        }

        [Test]
        public void ResetPlayer_ResetsState()
        {
            _health.TakeDamage(1);
            _presenter.ResetPlayer();

            Assert.AreEqual(_health.MaxLives, _health.CurrentLives);
            Assert.AreEqual(0f, _model.CurrentX);
            Assert.AreEqual(0f, _model.CurrentY);
            Assert.AreEqual(0f, _mockView.LastX);
            Assert.AreEqual(0f, _mockView.LastY);
        }
    }
}