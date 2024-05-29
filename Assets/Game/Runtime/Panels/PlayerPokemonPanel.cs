using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Golem;
using Slowbro;
using Eevee;
using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(Image))]
    internal sealed class PlayerPokemonPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class PlayerPokemonPanelSettings
        {
            [SerializeField]
            internal Image image;

            [SerializeField]
            internal Image overlay;
        }

        [SerializeField]
        private PlayerPokemonPanelSettings m_Settings = new PlayerPokemonPanelSettings();

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Settings.image.sprite = props.pokemon.asset.spriteBack;
        }

        public override IEnumerator ShowAsync()
        {
            gameObject.SetActive(true);

            m_Settings.overlay.gameObject.SetActive(true);

            yield return new Parallel(this, m_Settings.overlay.material.Alpha(1f, 0.4f, EasingType.PingPong), m_Settings.image.material.Alpha(1f, 0.4f, EasingType.linear),
                rectTransform.Scale(Vector3.zero, Vector3.one, 0.2f, Space.Self, EasingType.EaseOutSine));

            m_Settings.overlay.gameObject.SetActive(false);
        }

        internal IEnumerator FlashPokemonImageOnDamage()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return m_Settings.image.material.Flash(0f, 0.1f, EasingType.PingPong);
            }
        }

        public override IEnumerator HideAsync()
        {
            var position = rectTransform.anchoredPosition;

            yield return new Parallel(this, m_Settings.image.material.Alpha(0f, 0.35f, EasingType.linear),
                rectTransform.Translate(rectTransform.anchoredPosition, Vector2.down * 64f, 0.35f, Space.Self, EasingType.EaseOutSine));

            gameObject.SetActive(false);
            rectTransform.anchoredPosition = position;
        }
    }
}
