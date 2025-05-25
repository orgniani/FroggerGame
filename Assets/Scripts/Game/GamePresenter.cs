using Player;
namespace Game
{
    public class GamePresenter
    {
        private readonly GameModel _model;
        private readonly GameView _view;

        private PlayerPresenter _playerPresenter;
        public bool IsGameOver => _model.IsGameOver;

        public GamePresenter(GameModel model, GameView view, PlayerPresenter playerPresenter)
        {
            _model = model;
            _view = view;

            _playerPresenter = playerPresenter;
            _playerPresenter.OnGameOverTriggered.AddListener(TriggerGameOver);

            _view.HideGameOver();
            _view.OnRestartButtonClicked.AddListener(ResetGame);
        }

        public void TriggerGameOver()
        {
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
