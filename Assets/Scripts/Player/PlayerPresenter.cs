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

            if (moveInput.y > 0.5f)
                MoveLane(1);
            else if (moveInput.y < -0.5f)
                MoveLane(-1);
        }

        private void MoveLane(int dir)
        {
            _model.Move(dir);
            _view.UpdatePosition(_model.CurrentY);

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