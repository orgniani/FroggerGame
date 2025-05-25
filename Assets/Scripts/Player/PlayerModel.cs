using Config;
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

        public bool HasReachedGoal => Mathf.Approximately(CurrentY, MaxY);

        public PlayerModel(Vector3 startingPosition, GameConfig gameConfig)
        {
            _startingX = startingPosition.x;
            _startingY = startingPosition.y;

            _laneWidth = gameConfig.LaneWidth;
            _laneHeight = gameConfig.LaneHeight;

            MinX = gameConfig.LeftBoundary;
            MaxX = gameConfig.RightBoundary;
            
            MaxY = gameConfig.TopBoundary;

            CurrentX = _startingX;
            CurrentY = _startingY;
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
    }
}