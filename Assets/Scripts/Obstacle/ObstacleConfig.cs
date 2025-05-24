using UnityEngine;

namespace Obstacle
{
    [CreateAssetMenu(menuName = "Config/Obstacle", fileName = "ObstacleCfg", order = 0)]
    public class ObstacleConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
