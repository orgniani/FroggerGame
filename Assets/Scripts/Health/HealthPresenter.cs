namespace Health
{
    public class HealthPresenter
    {
        private readonly HealthModel _model;
        private readonly HealthView _view;

        public HealthPresenter(HealthModel model, HealthView view)
        {
            _model = model;
            _view = view;

            _view.Setup(_model.MaxLives);
            _view.UpdateHearts(_model.CurrentLives);

            _model.OnLivesChanged?.AddListener(_view.UpdateHearts);
        }
    }
}