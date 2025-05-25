namespace Game
{
    public class GamePresenter
    {
        private readonly GameModel _model;
        private readonly GameView _view;

        public bool IsGameOver => _model.IsGameOver;

        public GamePresenter(GameModel model, GameView view)
        {
            _model = model;
            _view = view;

            _view.HideGameOver();
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
        }
    }
}
