using Helpers;
using Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private LayerMask obstacleLayerMask;

        [Header("Animator Parameters")]
        [SerializeField] private string jumpTriggerParameter = "jump";
        [SerializeField] private string deadBoolParameter = "dead";

        [Header("Movement Animation Settings")]
        [SerializeField] private float moveDuration = 0.3f;
        [SerializeField] private AnimationCurve moveCurve;

        public UnityEvent<int> OnLaneChange = new UnityEvent<int>();
        public UnityEvent OnObstacleHit { get; } = new UnityEvent();

        private float _startingY;
        private Coroutine _moveCoroutine;

        private void Awake()
        {
            ValidateReferences();
        }

        private void Start()
        {
            _startingY = transform.position.y;
        }

        public void UpdatePosition(float newX, float newY)
        {
            animator.SetTrigger(jumpTriggerParameter);

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(MoveSmoothly(new Vector2(newX, newY)));
        }

        private IEnumerator MoveSmoothly(Vector2 targetPos)
        {
            Vector2 startPos = transform.position;
            float elapsed = 0f;

            while (elapsed < moveDuration)
            {
                float t = elapsed / moveDuration;
                float easedT = moveCurve.Evaluate(t);
                Vector2 newPos = Vector2.Lerp(startPos, targetPos, easedT);

                transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            _moveCoroutine = null;
        }

        private void TeleportToPosition(float newX, float newY)
        {
            var pos = transform.position;
            pos.x = newX;
            pos.y = newY;
            transform.position = pos;
        }

        public void SetFacingDirection(int xDir)
        {
            if (xDir == 0) return;
            spriteRenderer.flipX = xDir < 0;
        }

        public void ResetPosition()
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            animator.SetBool(deadBoolParameter, false);
            TeleportToPosition(0, _startingY);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsInLayerMask(other.gameObject.layer, obstacleLayerMask))
            {
                OnObstacleHit.Invoke();
            }
        }

        private bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }

        public void PlayGameOverAnimation(bool isDead)
        {
            animator.SetBool(deadBoolParameter, isDead);
        }

        private void ValidateReferences()
        {
#if UNITY_INCLUDE_TESTS
            if (Application.isPlaying)
                return;
#endif

            ReferenceValidator.Validate(animator, nameof(animator), this);
            ReferenceValidator.Validate(spriteRenderer, nameof(spriteRenderer), this);

            if (obstacleLayerMask == 0)
            {
                Debug.LogError("Obstacle Layer Mask is not set!", this);
            }
        }
    }
}