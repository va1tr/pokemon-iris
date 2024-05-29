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
    internal sealed class EnemyStatsPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class StatsPanelSettings
        {
            [SerializeField]
            internal Text name;

            [SerializeField]
            internal Text level;

            [SerializeField]
            internal Slider healthBar;
        }

        [SerializeField]
        private StatsPanelSettings m_Settings = new StatsPanelSettings();

        private Pokemon m_Pokemon;

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Pokemon = props.pokemon;

            m_Settings.name.text = m_Pokemon.name;

            float level = m_Pokemon.level;
            m_Settings.level.text = string.Concat($"Lv{level}");

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = m_Pokemon.health.maxValue;
            healthBar.value = m_Pokemon.health.value;
        }

        public override IEnumerator ShowAsync()
        {
            gameObject.SetActive(true);

            yield return rectTransform.Translate(new Vector3(-128f, 0f), Vector3.zero, 0.425f, Space.World, EasingType.linear);
        }

        internal IEnumerator SetHealthBarValue()
        {
            int current = Mathf.FloorToInt(m_Settings.healthBar.value);
            int target = Mathf.FloorToInt(m_Pokemon.health.value);
            int difference = current - target;

            float duration = 0.5f + (difference / 32f);

            yield return m_Settings.healthBar.Interpolate(m_Pokemon.health.value, duration, EasingType.EaseOutSine);
        }

        internal IEnumerator ShakeStatsPanel()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return rectTransform.Translate(rectTransform.anchoredPosition, Vector3.down, 0.05f, Space.Self, EasingType.PingPong);
            }         
        }
    }
}