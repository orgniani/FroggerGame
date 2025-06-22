using Interfaces;
using UnityEngine;

namespace Tests.Mocks
{
    public class MockObstacleView : MonoBehaviour, IObstacleView
    {
        public bool Initialized { get; private set; }
        public Vector2 LastResetPos { get; private set; }

        public void Initialize(Sprite sprite, bool flip)
        {
            Initialized = true;
        }

        public void ResetPosition(float x, float y)
        {
            LastResetPos = new Vector2(x, y);
        }

        public void SetXPosition(float x)
        {
            var pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }

        public MonoBehaviour AsMonoBehaviour() => this;
    }
}