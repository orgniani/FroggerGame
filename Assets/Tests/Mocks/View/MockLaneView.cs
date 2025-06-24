using Interfaces;
using UnityEngine;

namespace Tests.Mocks
{
    public class MockLaneView : MonoBehaviour, ILaneView
    {
        public IObstacleView ObstaclePrefab { get; set; }

        public Transform Transform => transform;
    }
}