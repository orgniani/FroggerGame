using UnityEngine;
using UnityEngine.Events;

namespace Obstacle
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObstacleView : MonoBehaviour
    {
        [SerializeField] private float leftBound = -10f;
        [SerializeField] private float rightBound = 10f;

        private SpriteRenderer _spriteRenderer;
        public UnityEvent OnRecycled;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Move(float speed)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x > rightBound)
            {
                var pos = transform.position;
                pos.x = leftBound;
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
