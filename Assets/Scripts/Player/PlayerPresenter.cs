using UnityEngine;
using Input;
using Health;
using UnityEngine.Events;
using Interfaces;

namespace Player
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private readonly PlayerModel _model;
        private readonly IPlayerView _view;
        private readonly HealthModel _healthModel;

        private readonly IInputManager _inputManager;
        private readonly PlayerInputHandler _inputHandler;

        private bool _isGameOver;
        public UnityEvent OnGameOverTriggered { get; } = new UnityEvent();

        public PlayerPresenter(PlayerModel model, IPlayerView view, HealthModel healthModel, IInputManager inputManager, float inputThreshold)

        {
            _model = model;
            _view = view;
            _healthModel = healthModel;

            _inputManager = inputManager;
            _inputHandler = new PlayerInputHandler(inputThreshold);

            _inputManager.OnMoveInput?.AddListener(HandleMoveInput);
            _view.OnObstacleHit?.AddListener(HandleObstacleHit);
        }

        private void HandleMoveInput(Vector2 moveInput)
        {
            if (_isGameOver) return;

            Vector2Int moveDir = _inputHandler.GetMoveDirection(moveInput);

            if (moveDir != Vector2Int.zero)
                Move(moveDir.x, moveDir.y);
        }

        private void Move(int xDir, int yDir)
        {
            _model.Move(xDir, yDir);
            _view.UpdatePosition(_model.CurrentX, _model.CurrentY);
            _view.SetFacingDirection(xDir);

            if (_model.HasReachedGoal)
                TriggerGameOver();
        }

        private void ResetPlayerPosition()
        {
            _model.Reset();
            _view.ResetPosition();
        }

        public void ResetPlayer()
        {
            ResetPlayerPosition();
            _healthModel.Reset();

            _isGameOver = false;
        }

        private void HandleObstacleHit()
        {
            if (_isGameOver)
                return;

            _healthModel.TakeDamage(1);

            if (_healthModel.IsDepleted)
            {
                TriggerGameOver();
                return;
            }

            ResetPlayerPosition();
        }

        private void TriggerGameOver()
        {
            _view.PlayGameOverAnimation(_healthModel.IsDepleted);
            OnGameOverTriggered?.Invoke();
            _isGameOver = true;
        }
    }
}