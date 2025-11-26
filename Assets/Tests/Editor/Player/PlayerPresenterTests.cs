using NUnit.Framework;
using UnityEngine;
using Interfaces;
using Player;
using Health;
using Tests.Mocks;

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
            _presenter.AllowMovement();
            _mockInput.SimulateInput(Vector2.up);
            Assert.AreEqual(1f, _model.CurrentY);
            Assert.AreEqual(1f, _mockView.LastY);
        }

        [Test]
        public void HandleMoveInput_SetsFacingDirection()
        {
            _presenter.AllowMovement();
            _mockInput.SimulateInput(Vector2.left);
            Assert.AreEqual(-1, _mockView.LastFacing);
        }

        [Test]
        public void HandleMoveInput_InvokesJumpEvent()
        {
            _presenter.AllowMovement();
            bool jumped = false;
            _presenter.OnJump.AddListener(() => jumped = true);
            _mockInput.SimulateInput(Vector2.right);
            Assert.IsTrue(jumped);
        }

        [Test]
        public void HandleMoveInput_DoesNotMove_WhenMovementBlocked()
        {
            _presenter.BlockMovement();
            _mockInput.SimulateInput(Vector2.up);
            Assert.AreEqual(0f, _model.CurrentY);
            Assert.AreEqual(0f, _mockView.LastY);
        }

        [Test]
        public void ObstacleHit_ReducesHealth()
        {
            int before = _health.CurrentLives;
            _mockView.OnObstacleHit.Invoke();
            Assert.AreEqual(before - 1, _health.CurrentLives);
        }

        [Test]
        public void ObstacleHit_InvokesOnHit_WhenNotDead()
        {
            bool hit = false;
            _presenter.OnHit.AddListener(() => hit = true);
            _mockView.OnObstacleHit.Invoke();
            Assert.IsTrue(hit);
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
        public void ObstacleHit_DoesNotReduceHealth_WhenGameOver()
        {
            for (int i = 0; i < _config.MaxLives; i++)
                _mockView.OnObstacleHit.Invoke();

            int before = _health.CurrentLives;
            _mockView.OnObstacleHit.Invoke();
            Assert.AreEqual(before, _health.CurrentLives);
        }

        [Test]
        public void ObstacleHit_Death_InvokesGameFinishedFalse()
        {
            bool result = true;
            _presenter.OnGameFinished.AddListener(v => result = v);
            for (int i = 0; i < _config.MaxLives; i++)
                _mockView.OnObstacleHit.Invoke();
            Assert.IsFalse(result);
        }

        [Test]
        public void MoveToGoal_TriggersGameOver()
        {
            _presenter.AllowMovement();
            bool triggered = false;
            _presenter.OnGameOverTriggered.AddListener(() => triggered = true);
            while (!_model.HasReachedGoal)
                _mockInput.SimulateInput(Vector2.up);
            Assert.IsTrue(triggered);
        }

        [Test]
        public void MoveToGoal_InvokesGameFinishedTrue()
        {
            _presenter.AllowMovement();
            bool result = false;
            _presenter.OnGameFinished.AddListener(v => result = v);
            while (!_model.HasReachedGoal)
                _mockInput.SimulateInput(Vector2.up);
            Assert.IsTrue(result);
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

        [Test]
        public void ResetPlayer_ClearsGameOverFlag()
        {
            for (int i = 0; i < _config.MaxLives; i++)
                _mockView.OnObstacleHit.Invoke();

            _presenter.ResetPlayer();
            _presenter.AllowMovement();
            _mockInput.SimulateInput(Vector2.up);
            Assert.AreEqual(1f, _model.CurrentY);
        }

        [Test]
        public void HandleMoveInput_DoesNotMove_WhenGameOver()
        {
            for (int i = 0; i < _config.MaxLives; i++)
                _mockView.OnObstacleHit.Invoke();

            _mockInput.SimulateInput(Vector2.up);
            Assert.AreEqual(0f, _model.CurrentY);
            Assert.AreEqual(0f, _mockView.LastY);
        }
    }
}
