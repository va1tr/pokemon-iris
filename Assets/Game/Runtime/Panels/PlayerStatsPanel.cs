using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Eevee;
using Voltorb;

namespace Iris
{
    internal sealed class PlayerStatsPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class StatsPanelSettings
        {
            [SerializeField]
            internal Text name;

            [SerializeField]
            internal Text level;

            [SerializeField]
            internal Text health;

            [SerializeField]
            internal Slider healthBar;

            [SerializeField]
            internal Slider experienceBar;
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

            float current = m_Pokemon.health.value;
            float max = m_Pokemon.health.maxValue;
            m_Settings.health.text = string.Concat($"{current}/{max}");

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = m_Pokemon.health.maxValue;
            healthBar.value = m_Pokemon.health.value;

            var experienceBar = m_Settings.experienceBar;

            experienceBar.minValue = Mathf.Pow(m_Pokemon.level, 3f);
            experienceBar.maxValue = Mathf.Pow(m_Pokemon.level + 1, 3f);
            experienceBar.value = m_Pokemon.experience;
        }

        public override IEnumerator ShowAsync()
        {
            gameObject.SetActive(true);

            yield return rectTransform.Translate(new Vector3(128f, 0f), Vector3.zero, 0.425f, Space.World, EasingType.linear);
        }

        internal IEnumerator SetExperienceBarValue()
        {
            m_Settings.experienceBar.minValue = Mathf.Pow(m_Pokemon.level, 3f);
            m_Settings.experienceBar.maxValue = Mathf.Pow(Mathf.Min(m_Pokemon.level + 1, 100f), 3f);

            float difference = m_Pokemon.experience - m_Settings.experienceBar.value;
            float duration = 0.5f + (difference / 64f);

            yield return m_Settings.experienceBar.Interpolate(m_Pokemon.experience, duration, EasingType.EaseOutSine);
        }

        internal IEnumerator SetHealthBarValue()
        {
            int current = Mathf.FloorToInt(m_Settings.healthBar.value);
            int target = Mathf.FloorToInt(m_Pokemon.health.value);
            int max = Mathf.FloorToInt(m_Settings.healthBar.maxValue);

            int difference = current - target;
            float duration = 0.5f + (difference / 64f);

            yield return new Parallel(this, TypeHealthTextCharByChar(target, current, max),
                    m_Settings.healthBar.Interpolate(m_Pokemon.health.value, duration, EasingType.EaseOutSine));
        }

        private IEnumerator TypeHealthTextCharByChar(int target, int current, int max)
        {
            while (current != target)
            {
                int value = Mathf.FloorToInt(m_Settings.healthBar.value);

                if (current != value)
                {
                    current = value;
                    m_Settings.health.text = string.Concat($"{current}/{max}");
                }

                yield return null;
            }
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