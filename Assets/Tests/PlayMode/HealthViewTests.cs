using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Health;

namespace Tests.PlayMode
{
    public class HealthViewTests
    {
        private GameObject _go;
        private HealthView _healthView;

        [SetUp]
        public void SetUp()
        {
            _go = new GameObject("HealthView");
            _healthView = _go.AddComponent<HealthView>();

            var fullHeart = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
            var emptyHeart = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 1, 1), Vector2.zero);

            var heartPrefabGO = new GameObject("Heart", typeof(UnityEngine.UI.Image));
            var heartPrefab = heartPrefabGO.GetComponent<UnityEngine.UI.Image>();
            var heartParent = new GameObject("HeartParent").transform;

            typeof(HealthView).GetField("fullHeart", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_healthView, fullHeart);
            typeof(HealthView).GetField("emptyHeart", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_healthView, emptyHeart);
            typeof(HealthView).GetField("heartPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_healthView, heartPrefab);
            typeof(HealthView).GetField("heartParent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_healthView, heartParent);
        }

        [UnityTest]
        public System.Collections.IEnumerator SetupAndUpdateHearts_WorksCorrectly()
        {
            _healthView.Setup(3);
            _healthView.UpdateHearts(2);

            yield return null;

            var hearts = typeof(HealthView).GetField("_hearts", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(_healthView) as System.Collections.Generic.List<UnityEngine.UI.Image>;

            Assert.AreEqual(3, hearts.Count);
            Assert.AreEqual(hearts[0].sprite, _healthView.GetType().GetField("fullHeart", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_healthView));
            Assert.AreEqual(hearts[2].sprite, _healthView.GetType().GetField("emptyHeart", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_healthView));
        }
    }
}
