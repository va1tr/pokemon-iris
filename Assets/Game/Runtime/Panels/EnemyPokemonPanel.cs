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
    internal sealed class EnemyPokemonPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class EnemyPokemonPanelSettings
        {
            [SerializeField]
            internal Image image;
        }

        [SerializeField]
        private EnemyPokemonPanelSettings m_Settings = new EnemyPokemonPanelSettings();

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Settings.image.sprite = props.pokemon.asset.spriteFront;
        }

        public override IEnumerator ShowAsync()
        {
            gameObject.SetActive(true);

            yield return new Parallel(this, m_Settings.image.material.Alpha(1f, 0.01f, EasingType.linear));
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
            yield return new Parallel(this, m_Settings.image.material.Alpha(0f, 0.35f, EasingType.linear),
                rectTransform.Translate(rectTransform.anchoredPosition, Vector2.down * 64f, 0.35f, Space.Self, EasingType.EaseOutSine));

            gameObject.SetActive(false);
        }
    }
}
