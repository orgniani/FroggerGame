using Helpers;
using Obstacle;
using UnityEngine;

namespace Lane
{
    public class LaneView : MonoBehaviour
    {
        [SerializeField] private ObstacleView obstaclePrefab;
        public ObstacleView ObstaclePrefab => obstaclePrefab;

        private LanePresenter _presenter;

        private void Awake()
        {
            ReferenceValidator.Validate(obstaclePrefab, nameof(obstaclePrefab), this);
        }

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