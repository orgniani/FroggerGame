using Helpers;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Obstacle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private ObstacleView obstaclePrefab;
        [SerializeField] private List<ObstacleConfig> obstacleConfigs;

        [SerializeField] private GameConfig gameConfig;

        private List<ObstaclePresenter> _presenters = new();

        private void Awake()
        {
            ValidateReferences();
        }
        private void Start()
        {
            for (int i = 0; i < gameConfig.LaneAmount; i++)
            {
                float spawnPositionY = gameConfig.BottomBoundary + (i * gameConfig.LaneHeight);
                Vector3 spawnPos = new Vector3(gameConfig.LeftBoundary, spawnPositionY, 0);

                ObstacleView view = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
                view.InitializeBounds(gameConfig.LeftBoundary, gameConfig.RightBoundary);

                ObstacleModel model = new ObstacleModel();

                ObstaclePresenter presenter = new ObstaclePresenter(model, view, obstacleConfigs);
                _presenters.Add(presenter);
            }
        }

        private void Update()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Update();
            }
        }

        private void ValidateReferences()
        {
            ReferenceValidator.Validate(obstaclePrefab, nameof(obstaclePrefab), this);
            ReferenceValidator.Validate(gameConfig, nameof(gameConfig), this);

            foreach (var config in obstacleConfigs)
                ReferenceValidator.Validate(config.Sprite, nameof(config.Sprite), this);

            if (obstacleConfigs.Count == 0)
            {
                Debug.LogError("Obstacle configs list is empty!", this);
            }
        }
    }
}