using Obstacle;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace Lane
{
    public class LanePresenter
    {
        private readonly LaneModel _model;
        private readonly ILaneView _view;

        private readonly Transform _parent;
        private readonly IObstacleView _obstaclePrefab;

        private readonly List<ObstaclePresenter> _obstaclePresenters = new();

        private bool _initialized;

        public LanePresenter(LaneModel model, ILaneView view)
        {
            _model = model;
            _view = view;
            _parent = view.Transform;
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
            Debug.Log("Spawning obstacle!");

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
            Debug.Log("Creating new obstacle at X=" + _model.LastSpawnedX);

            bool flip = _model.Direction == LaneDirection.Left;

            var viewGO = GameObject.Instantiate(_obstaclePrefab.AsMonoBehaviour(), _parent);
            var view = viewGO.GetComponent<IObstacleView>();

            view.Initialize(_model.Config.Sprite, flip);
            view.ResetPosition(_model.LastSpawnedX, _view.Transform.position.y);

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