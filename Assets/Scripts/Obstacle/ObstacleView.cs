using UnityEngine;

namespace Obstacle
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObstacleView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
