using Obstacle;
using UnityEngine;

namespace Lane
{
    public class LaneView : MonoBehaviour
    {
        [SerializeField] private ObstacleView obstaclePrefab;
        public ObstacleView ObstaclePrefab => obstaclePrefab;

        private LanePresenter _presenter;

        public void SetPresenter(LanePresenter presenter)
        {
            _presenter = presenter;
        }

        private void Update()
        {
            _presenter?.Update(Time.deltaTime);
        }
    }
}