using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleLayerMask;

        public UnityEvent<int> OnLaneChange = new UnityEvent<int>();
        public UnityEvent OnObstacleHit = new UnityEvent();

        private float _startingY;

        private void Start()
        {
            _startingY = transform.position.y;
        }

        public void UpdatePosition(float newX, float newY)
        {
            var pos = transform.position;
            pos.x = newX;
            pos.y = newY;
            transform.position = pos;
        }

        public void ResetPosition()
        {
            UpdatePosition(0, _startingY);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsInLayerMask(other.gameObject.layer, obstacleLayerMask))
            {
                OnObstacleHit.Invoke();
            }
        }

        private bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }
    }
}
