using UnityEngine;

namespace Obstacle
{
    public class ObstacleModel
    {
        public float Speed { get; private set; }
        public Sprite Sprite { get; private set; }

        public void ApplyConfig(ObstacleConfig config)
        {
            Speed = config.Speed;
            Sprite = config.Sprite;
        }
    }
}
