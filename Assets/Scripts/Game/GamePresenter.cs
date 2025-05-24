namespace Game
{
    public class GamePresenter
    {
        private GameModel _model;
        private GameView _view;

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

        public bool IsGameOver => _model.IsGameOver;
    }
}
