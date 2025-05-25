using Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;
        [SerializeField] private Image heartPrefab;

        [SerializeField] private Transform heartParent;

        private List<Image> _hearts = new();

        private void Awake()
        {
            ValidateReferences();
        }

        public void Setup(int maxLives)
        {
            _hearts.Clear();

            for (int i = 0; i < maxLives; i++)
            {
                Image heart = Instantiate(heartPrefab, heartParent);
                _hearts.Add(heart);
            }
        }

        public void UpdateHearts(int currentLives)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                _hearts[i].sprite = i < currentLives ? fullHeart : emptyHeart;
            }
        }

        private void ValidateReferences()
        {
            ReferenceValidator.Validate(fullHeart, nameof(fullHeart), this);
            ReferenceValidator.Validate(emptyHeart, nameof(emptyHeart), this);
            ReferenceValidator.Validate(heartPrefab, nameof(heartPrefab), this);
            ReferenceValidator.Validate(heartParent, nameof(heartParent), this);
        }
    }
}