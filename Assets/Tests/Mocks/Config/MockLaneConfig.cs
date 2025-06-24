using Interfaces;
using UnityEngine;

namespace Tests.Mocks
{
    public class MockLaneConfig : ILaneConfig
    {
        public int ObstacleWidth => 1;
        public float Speed => 2f;
        public int Distance => 3;
        public Sprite Sprite => null;
    }
}