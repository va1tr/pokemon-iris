using System.Collections;

using UnityEngine;

using Voltorb;
using Slowbro;

namespace Iris
{
    internal sealed class BattleGraphicsInterface : GraphicalUserInterface
    {
        [SerializeField]
        private MovesMenu m_MovesMenu;

        [SerializeField]
        private AbilitiesMenu m_AbilitiesMenu;

        [SerializeField]
        private LevelUpMenu m_LevelUpMenu;

        [SerializeField]
        private PlayerPanel m_PlayerPanel;

        [SerializeField]
        private PlayerStatsPanel m_PlayerStatsPanel;

        [SerializeField]
        private PlayerTrainerPanel m_PlayerTrainerPanel;

        [SerializeField]
        private PlayerPokemonPanel m_PlayerPokemonPanel;

        [SerializeField]
        private EnemyPanel m_EnemyPanel;

        [SerializeField]
        private EnemyStatsPanel m_EnemyStatsPanel;

        [SerializeField]
        private EnemyPokemonPanel m_EnemyPokemonPanel;

        [SerializeField]
        private Typewriter m_Typewriter;

        protected override void BindSceneGraphicReferences()
        {
            Add(m_MovesMenu);
            Add(m_AbilitiesMenu);
            Add(m_LevelUpMenu);

            Add(m_PlayerPanel);
            Add(m_PlayerStatsPanel);
            Add(m_PlayerTrainerPanel);
            Add(m_PlayerPokemonPanel);

            Add(m_EnemyPanel);
            Add(m_EnemyStatsPanel);
            Add(m_EnemyPokemonPanel);
        }

        internal IEnumerator BouncePokemonAndStatsPanelWhileWaiting()
        {
            yield return new Parallel(this,
                    m_PlayerPokemonPanel.rectTransform.Translate(m_PlayerPokemonPanel.rectTransform.anchoredPosition, Vector3.down, 1.2f, Space.Self, EasingType.PingPong),
                    m_PlayerStatsPanel.rectTransform.Translate(m_PlayerStatsPanel.rectTransform.anchoredPosition, Vector3.down, 1.5f, Space.Self, EasingType.PingPong));
        }

        internal IEnumerator SetPlayerStatsPanelExperienceSlider()
        {
            yield return m_PlayerStatsPanel.SetExperienceBarValue();
        }

        internal IEnumerator SetPlayerStatsPanelHealthSlider()
        {
            yield return m_PlayerStatsPanel.SetHealthBarValue();
        }

        internal IEnumerator ShakePlayerStatsPanel()
        {
            yield return m_PlayerStatsPanel.ShakeStatsPanel();
        }

        internal IEnumerator FlashPlayerPokemonImageOnDamage()
        {
            yield return m_PlayerPokemonPanel.FlashPokemonImageOnDamage();
        }

        internal IEnumerator SetEnemyStatsPanelHealthSlider()
        {
            yield return m_EnemyStatsPanel.SetHealthBarValue();
        }

        internal IEnumerator ShakeEnemyStatsPanel()
        {
            yield return m_EnemyStatsPanel.ShakeStatsPanel();
        }

        internal IEnumerator FlashEnemyPokemonImageOnDamage()
        {
            yield return m_EnemyPokemonPanel.FlashPokemonImageOnDamage();
        }

        internal IEnumerator TypeTextCharByChar(string text)
        {
            yield return m_Typewriter.TypeTextCharByChar(text);
        }

        internal void PrintTextCharByChar(string text)
        {
            m_Typewriter.PrintTextCharByChar(text);
        }

        internal void PrintCompletedText(string text)
        {
            m_Typewriter.PrintCompletedText(text);
        }

        internal void CleanupTextProcessorAndClearText()
        {
            m_Typewriter.CleanupAndClearAllText();
        }

    }
}