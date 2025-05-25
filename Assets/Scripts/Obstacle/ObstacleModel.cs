using UnityEngine;

namespace Obstacle
{
    public class ObstacleModel
    {
        public float CurrentX { get; private set; }
        private readonly float _leftBound;
        private readonly float _rightBound;
        private readonly float _speed;

        public ObstacleModel(float startX, float leftBound, float rightBound, float speed)
        {
            CurrentX = startX;
            _leftBound = leftBound;
            _rightBound = rightBound;
            _speed = speed;
        }

        public void UpdatePosition(float deltaTime)
        {
            CurrentX += _speed * deltaTime;

            if (_speed > 0 && CurrentX > _rightBound)
                CurrentX = _leftBound;
            else if (_speed < 0 && CurrentX < _leftBound)
                CurrentX = _rightBound;
        }

        public void SetPosition(float newX)
        {
            CurrentX = newX;
        }
    }
}
