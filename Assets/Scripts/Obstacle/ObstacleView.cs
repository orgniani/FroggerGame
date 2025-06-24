using Helpers;
using UnityEngine;
using Interfaces;

namespace Obstacle
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObstacleView : MonoBehaviour, IObstacleView
    {
        private SpriteRenderer _spriteRenderer;
        public MonoBehaviour AsMonoBehaviour() => this;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            ReferenceValidator.Validate(_spriteRenderer, nameof(_spriteRenderer), this);
        }

        public void Initialize(Sprite sprite, bool flip)
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.flipX = flip;
        }

        public void ResetPosition(float x, float y)
        {
            transform.position = new Vector3(x, y, 0);
        }

        public void SetXPosition(float x)
        {
            var pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }
    }
}
