using Interfaces;
using Player;
namespace Game
{
    public class GamePresenter
    {
        private readonly GameModel _model;
        private readonly IGameView _view;

        private IPlayerPresenter _playerPresenter;

        public GamePresenter(GameModel model, IGameView view, IPlayerPresenter playerPresenter)
        {
            _model = model;
            _view = view;

            _playerPresenter = playerPresenter;
            _playerPresenter.BlockMovement();
            _playerPresenter.OnGameOverTriggered.AddListener(TriggerGameOver);

            _view.HideGameOver();
            _view.OnRestartButtonClicked.AddListener(ResetGame);
            _view.OnPlayButtonClicked.AddListener(StartGame);
        }

        private void StartGame()
        {
            _model.StartGame();
            _view.HideMainMenu();
            _playerPresenter.AllowMovement();
        }

        public void TriggerGameOver()
        {
            if (_model.IsGameOver)
                return;

            _model.EndGame();
            _view.ShowGameOver();
        }

        public void ResetGame()
        {
            _model.ResetGame();
            _view.HideGameOver();
            _playerPresenter?.ResetPlayer();
        }
    }
}
