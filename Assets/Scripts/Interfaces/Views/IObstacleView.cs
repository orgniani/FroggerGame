using UnityEngine;

namespace Interfaces
{
    public interface IObstacleView
    {
        void Initialize(Sprite sprite, bool flip);
        void ResetPosition(float x, float y);
        void SetXPosition(float x);

        MonoBehaviour AsMonoBehaviour();
    }
}