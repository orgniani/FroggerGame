using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private ObstacleView obstaclePrefab;
        [SerializeField] private List<ObstacleConfig> obstacleConfigs;

        //TODO: Should NOT set the number of lanes here!
        [SerializeField] private int numLanes = 3;
        [SerializeField] private float laneHeight = 1f;
        [SerializeField] private float startX = -8f;

        private List<ObstaclePresenter> _presenters = new();

        private void Start()
        {
            for (int i = 0; i < numLanes; i++)
            {
                Vector3 spawnPos = new Vector3(startX, i * laneHeight, 0);
                ObstacleView view = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
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
    }
}