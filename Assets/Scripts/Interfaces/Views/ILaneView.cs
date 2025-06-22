using Interfaces;
using UnityEngine;

namespace Interfaces
{
    public interface ILaneView
    {
        IObstacleView ObstaclePrefab { get; }
        Transform Transform { get; }
    }
}
