using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class ObstaclePresenter
    {
        private readonly ObstacleModel _model;
        private readonly ObstacleView _view;
        private readonly List<ObstacleConfig> _configs;

        public ObstaclePresenter(ObstacleModel model, ObstacleView view, List<ObstacleConfig> configs)
        {
            _model = model;
            _view = view;
            _configs = configs;

            AssignRandomConfig();

            _view.OnRecycled.AddListener(AssignRandomConfig);
        }

        public void Update()
        {
            _view.Move(_model.Speed);
        }

        private void AssignRandomConfig()
        {
            var config = _configs[Random.Range(0, _configs.Count)];
            _model.ApplyConfig(config);
            _view.SetSprite(config.Sprite);
        }
    }
}