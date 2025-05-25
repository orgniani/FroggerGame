using Config;

namespace Lane
{
    public class LaneModel
    {
        public LaneConfig Config { get; }
        public LaneDirection Direction { get; }

        private readonly float _leftBoundary;
        private readonly float _rightBoundary;

        public float LastSpawnedX { get; private set; }

        public LaneModel(LaneConfig config, LaneDirection direction, float leftBoundary, float rightBoundary)
        {
            Config = config;
            Direction = direction;
            _leftBoundary = leftBoundary;
            _rightBoundary = rightBoundary;

            LastSpawnedX = CalculateInitialSpawnX();
        }

        private float CalculateInitialSpawnX()
        {
            return Direction == LaneDirection.Right
                ? _leftBoundary - Config.ObstacleWidth
                : _rightBoundary + Config.ObstacleWidth;
        }

        public void UpdateNextSpawnX()
        {
            float step = Config.ObstacleWidth + Config.Distance;
            LastSpawnedX += Direction == LaneDirection.Right ? step : -step;
        }

        public float GetSpeed()
        {
            return Direction == LaneDirection.Right ? Config.Speed : -Config.Speed;
        }

        public float GetLeftBoundWithMargin() => _leftBoundary - Config.ObstacleWidth;
        public float GetRightBoundWithMargin() => _rightBoundary + Config.ObstacleWidth;
    }
}