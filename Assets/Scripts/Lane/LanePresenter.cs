using Obstacle;
using System.Collections.Generic;
using UnityEngine;

namespace Lane
{
    public class LanePresenter
    {
        private readonly LaneModel _model;
        private readonly LaneView _view;

        private readonly Transform _parent;
        private readonly ObstacleView _obstaclePrefab;

        private readonly List<ObstaclePresenter> _obstaclePresenters = new();

        private bool _initialized;

        public LanePresenter(LaneModel model, LaneView view)
        {
            _model = model;
            _view = view;
            _parent = view.transform;
            _obstaclePrefab = view.ObstaclePrefab;
        }

        public void Update(float deltaTime)
        {
            MoveObstacles(deltaTime);

            if (!_initialized)
            {
                _model.UpdateNextSpawnX();
                SpawnObstacle();
                _initialized = true;
                return;
            }

            TrySpawnObstacleIfNeeded();
        }

        private void MoveObstacles(float deltaTime)
        {
            foreach (var presenter in _obstaclePresenters)
                presenter.Update(deltaTime);
        }

        private void TrySpawnObstacleIfNeeded()
        {
            if (IsBeyondBoundary())
                SpawnObstacle();
        }

        private bool IsBeyondBoundary()
        {
            return _model.Direction == LaneDirection.Right
                ? _model.LastSpawnedX < _model.GetRightBoundWithMargin()
                : _model.LastSpawnedX > _model.GetLeftBoundWithMargin();
        }

        private void SpawnObstacle()
        {
            var reusable = FindReusablePresenter();
            if (reusable != null)
                ReuseObstacle(reusable);
            else
                CreateAndAddNewObstacle();

            _model.UpdateNextSpawnX();
        }

        private ObstaclePresenter FindReusablePresenter()
        {
            foreach (var presenter in _obstaclePresenters)
            {
                var x = presenter.GetCurrentX();
                if (_model.Direction == LaneDirection.Right && x > _model.GetRightBoundWithMargin())
                    return presenter;
                if (_model.Direction == LaneDirection.Left && x < _model.GetLeftBoundWithMargin())
                    return presenter;
            }
            return null;
        }

        private void ReuseObstacle(ObstaclePresenter presenter)
        {
            presenter.Reset(_model.LastSpawnedX);
        }

        private void CreateAndAddNewObstacle()
        {
            bool flip = _model.Direction == LaneDirection.Left;

            var view = GameObject.Instantiate(_obstaclePrefab, _parent);
            view.Initialize(_model.Config.Sprite, flip);
            view.ResetPosition(_model.LastSpawnedX, _view.transform.position.y);

            var model = new ObstacleModel(
                _model.LastSpawnedX,
                _model.GetLeftBoundWithMargin(),
                _model.GetRightBoundWithMargin(),
                _model.GetSpeed()
            );

            var presenter = new ObstaclePresenter(model, view);
            _obstaclePresenters.Add(presenter);
        }
    }
}