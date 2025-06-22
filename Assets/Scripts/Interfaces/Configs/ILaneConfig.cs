using UnityEngine;

namespace Interfaces
{
    public interface ILaneConfig
    {
        int ObstacleWidth { get; }
        float Speed { get; }
        int Distance { get; }
        Sprite Sprite { get; }
    }
}