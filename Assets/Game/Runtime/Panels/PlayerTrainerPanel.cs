using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(Image))]
    internal class PlayerTrainerPanel : Panel
    {
        [Serializable]
        private sealed class PlayerTrainerPanelSettings
        {
            [SerializeField]
            internal Image image;

            [SerializeField]
            internal Sprite[] sprites;

            [SerializeField, Range(0, 12)]
            internal int frameRate = 6;
        }

        [SerializeField]
        private PlayerTrainerPanelSettings m_Settings = new PlayerTrainerPanelSettings();

        public override IEnumerator HideAsync()
        {
            // 1.75f
            yield return new Parallel(this, m_Settings.image.Animate(m_Settings.sprites, m_Settings.frameRate),
                rectTransform.Translate(rectTransform.anchoredPosition, Vector3.right * -128f, 1.75f, Space.Self, EasingType.linear));

            gameObject.SetActive(false);
        }
    }
}