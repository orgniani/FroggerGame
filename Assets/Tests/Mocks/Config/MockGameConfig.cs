using UnityEngine;
using Interfaces;

namespace Tests.Mocks
{
    public class MockGameConfig : IGameConfig
    {
        public float LaneWidth => 1f;
        public float LaneHeight => 1f;
        public float TopBoundary => 2f;
        public float BottomBoundary => -2f;
        public float LeftBoundary => -1f;
        public float RightBoundary => 1f;
        public int MaxLives => 3;
        public float InputThreshold => 0.5f;
    }
}