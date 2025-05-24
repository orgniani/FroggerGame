using UnityEngine;
using Game;
using Input;

namespace Player
{
    public class PlayerPresenter
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly GamePresenter _game;
        private readonly InputManager _inputManager;

        public PlayerPresenter(PlayerModel model, PlayerView view, GamePresenter game, InputManager inputManager)
        {
            _model = model;
            _view = view;
            _game = game;
            _inputManager = inputManager;

            _inputManager.OnMoveInput.AddListener(HandleMoveInput);
            _view.OnObstacleHit.AddListener(ResetPlayer);
        }

        private void HandleMoveInput(Vector2 moveInput)
        {
            if (_game.IsGameOver) return;

            int yDir = 0;
            int xDir = 0;

            //TODO: Replace hardcoded values!
            if (moveInput.y > 0.5f)
                yDir = 1;
            else if (moveInput.y < -0.5f)
                yDir = -1;

            if (moveInput.x > 0.5f)
                xDir = 1;
            else if (moveInput.x < -0.5f)
                xDir = -1;

            if (xDir != 0 || yDir != 0)
                Move(xDir, yDir);
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
    }
}