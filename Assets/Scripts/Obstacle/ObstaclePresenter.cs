namespace Obstacle
{
    public class ObstaclePresenter
    {
        private readonly ObstacleModel _model;
        private readonly ObstacleView _view;

        public ObstaclePresenter(ObstacleModel model, ObstacleView view)
        {
            _model = model;
            _view = view;
        }

        public void Update(float deltaTime)
        {
            _model.UpdatePosition(deltaTime);
            _view.SetXPosition(_model.CurrentX);
        }

        public void Reset(float newX)
        {
            _model.SetPosition(newX);
            _view.SetXPosition(newX);
        }

        public float GetCurrentX()
        {
            return _model.CurrentX;
        }
    }
}