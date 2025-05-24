using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public float CurrentY { get; private set; }
        public float CurrentX { get; private set; }

        private readonly float _startingY;
        private readonly float _startingX;
        private readonly float _laneHeight;
        private readonly float _laneWidth;

        public float MaxY { get; }
        public float MinX { get; }
        public float MaxX { get; }

        //TODO: Too many parameters needed
        public PlayerModel(float startingX, float startingY, float laneWidth, float laneHeight, float maxY, float minX, float maxX)
        {
            _startingX = startingX;
            _startingY = startingY;
            _laneWidth = laneWidth;
            _laneHeight = laneHeight;

            MinX = minX;
            MaxX = maxX;
            MaxY = maxY;

            CurrentX = startingX;
            CurrentY = startingY;
        }

        public void Move(int xDir, int yDir)
        {
            CurrentY += yDir * _laneHeight;
            CurrentY = Mathf.Clamp(CurrentY, _startingY, MaxY);

            CurrentX += xDir * _laneWidth;
            CurrentX = Mathf.Clamp(CurrentX, MinX, MaxX);
        }

        public void Reset()
        {
            CurrentX = _startingX;
            CurrentY = _startingY;
        }

        public bool HasReachedGoal => Mathf.Approximately(CurrentY, MaxY);
    }
}