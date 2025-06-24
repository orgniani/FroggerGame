using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Obstacle;

namespace Tests.PlayMode
{
    public class ObstacleViewTests
    {
        private GameObject _go;
        private ObstacleView _view;
        private SpriteRenderer _spriteRenderer;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _go = new GameObject("ObstacleViewTestObject");
            _spriteRenderer = _go.AddComponent<SpriteRenderer>();
            _view = _go.AddComponent<ObstacleView>();

            _view.Invoke("Awake", 0f);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Initialize_SetsSpriteAndFlip()
        {
            var tex = new Texture2D(32, 32);
            var sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), Vector2.zero);

            _view.Initialize(sprite, true);
            yield return null;

            Assert.AreEqual(sprite, _spriteRenderer.sprite);
            Assert.IsTrue(_spriteRenderer.flipX);

            _view.Initialize(sprite, false);
            yield return null;

            Assert.IsFalse(_spriteRenderer.flipX);
        }

        [UnityTest]
        public IEnumerator ResetPosition_SetsTransformPositionCorrectly()
        {
            _view.ResetPosition(3f, 5f);
            yield return null;

            Assert.AreEqual(new Vector3(3f, 5f, 0f), _go.transform.position);
        }

        [UnityTest]
        public IEnumerator SetXPosition_ChangesOnlyXCoordinate()
        {
            _go.transform.position = new Vector3(1f, 2f, 0f);
            _view.SetXPosition(9f);
            yield return null;

            var pos = _go.transform.position;
            Assert.AreEqual(9f, pos.x, 0.01f);
            Assert.AreEqual(2f, pos.y, 0.01f);
            Assert.AreEqual(0f, pos.z, 0.01f);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_go);
            yield return null;
        }
    }
}