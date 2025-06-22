using Helpers;
using Obstacle;
using UnityEngine;
using Interfaces;

namespace Lane
{
    public class LaneView : MonoBehaviour, ILaneView
    {
        [SerializeField] private ObstacleView obstaclePrefab;
        
        private LanePresenter _presenter;
        
        public IObstacleView ObstaclePrefab => obstaclePrefab;
        public Transform Transform => transform;

        private void Awake()
        {
#if UNITY_INCLUDE_TESTS
            if (Application.isPlaying)
                return;
#endif

            ReferenceValidator.Validate(obstaclePrefab, nameof(obstaclePrefab), this);
        }

        public void SetPresenter(LanePresenter presenter)
        {
            _presenter = presenter;
            Debug.Log("LanePresenter set on LaneView");
        }

        private void Update()
        {
            Debug.Log("LaneView.Update() called");
            _presenter?.Update(Time.deltaTime);
        }
    }
}