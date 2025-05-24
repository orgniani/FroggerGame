using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private readonly float _startingY;
        private readonly float _laneHeight;
        public float CurrentY { get; private set; }
        public float MaxY { get; }

        public PlayerModel(float startingY, float laneHeight, float maxY)
        {
            _startingY = startingY;
            _laneHeight = laneHeight;
            MaxY = maxY;

            CurrentY = startingY;
        }

        public void Move(int dir)
        {
            CurrentY += dir * _laneHeight;
            CurrentY = Mathf.Clamp(CurrentY, _startingY, MaxY);
        }

        public void Reset()
        {
            CurrentY = _startingY;
        }

        public bool HasReachedGoal => Mathf.Approximately(CurrentY, MaxY);
    }
}