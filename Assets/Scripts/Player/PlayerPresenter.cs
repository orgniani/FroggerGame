using UnityEngine;
using Game;
using Input;
using Health;

namespace Player
{
    public class PlayerPresenter
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly HealthModel _healthModel;
        private readonly GamePresenter _game;
        private readonly InputManager _inputManager;
        private readonly PlayerInputHandler _inputHandler;

        public PlayerPresenter(PlayerModel model, PlayerView view, HealthModel healthModel, GamePresenter game, InputManager inputManager, float inputThreshold)
        {
            _model = model;
            _view = view;
            _healthModel = healthModel;

            _game = game;
            _inputManager = inputManager;

            _inputHandler = new PlayerInputHandler(inputThreshold);

            _inputManager.OnMoveInput.AddListener(HandleMoveInput);
            _view.OnObstacleHit.AddListener(HandleObstacleHit);
        }

        private void HandleMoveInput(Vector2 moveInput)
        {
            if (_game.IsGameOver) return;

            Vector2Int moveDir = _inputHandler.GetMoveDirection(moveInput);

            if (moveDir != Vector2Int.zero)
                Move(moveDir.x, moveDir.y);
        }

        private void Move(int xDir, int yDir)
        {
            _model.Move(xDir, yDir);
            _view.UpdatePosition(_model.CurrentX, _model.CurrentY);

            if (_model.HasReachedGoal)
                _game.TriggerGameOver();
        }

        private void ResetPlayer()
        {
            _model.Reset();
            _view.ResetPosition();
        }

        private void HandleObstacleHit()
        {
            _healthModel.TakeDamage(1);

            if (_model.HasReachedGoal || _healthModel.IsDepleted)
            {
                _game.TriggerGameOver();
                return;
            }

            ResetPlayer();
        }
    }
}