using UnityEngine;
using UnityEngine.Events;

namespace Obstacle
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObstacleView : MonoBehaviour
    {
        [SerializeField] private float margin = 1f;

        private float _leftBound;
        private float _rightBound;

        private SpriteRenderer _spriteRenderer;
        public UnityEvent OnRecycled;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void InitializeBounds(float left, float right)
        {
            _leftBound = left - margin;
            _rightBound = right + margin;
        }

        public void Move(float speed)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x > _rightBound)
            {
                var pos = transform.position;
                pos.x = _leftBound;
                transform.position = pos;

                OnRecycled?.Invoke();
            }
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}
