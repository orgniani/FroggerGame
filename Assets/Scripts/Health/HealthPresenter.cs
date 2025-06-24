using Interfaces;

namespace Health
{
    public class HealthPresenter
    {
        private readonly HealthModel _model;
        private readonly IHealthView _view;

        public HealthPresenter(HealthModel model, IHealthView view)
        {
            _model = model;
            _view = view;

            _view.Setup(_model.MaxLives);
            _view.UpdateHearts(_model.CurrentLives);

            _model.OnLivesChanged?.AddListener(_view.UpdateHearts);
        }
    }
}