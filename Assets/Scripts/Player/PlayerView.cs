using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public UnityEvent<int> OnLaneChange;
        public UnityEvent OnObstacleHit;

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

        public void TriggerObstacleHit()
        {
            OnObstacleHit.Invoke();
        }
    }
}
