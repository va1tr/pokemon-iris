using System;
using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    internal sealed class BattleWonState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        private BattleGraphicsInterface m_Interface;
        private BattleCoordinator m_Coordinator;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);

        private const float kDelayForHalfSecond = 0.5f;

        public BattleWonState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;
        }

        public override void Enter()
        {
            m_Coordinator = m_StateBehaviour.GetBattleCoordinator();
            m_Interface = m_StateBehaviour.GetBattleGraphicsInterface();

            m_StateBehaviour.StartCoroutine(WildPokemonBattleWonEndSequence());
        }

        private IEnumerator WildPokemonBattleWonEndSequence()
        {
            yield return m_DelayForHalfSecond;

            yield return m_Interface.HideAsync<EnemyPokemonPanel>();

            yield return new Parallel(m_Coordinator, TypeDefeatedPokemonNameCharByChar(),
                m_Interface.HideAsync<EnemyStatsPanel>());

            yield return m_DelayForHalfSecond;

            yield return CalculateExperienceGainAndLevelUp();
        }

        private IEnumerator TypeDefeatedPokemonNameCharByChar()
        {
            m_Coordinator.GetEnemyActivePokemon(out var combatant);

            string message = string.Concat($"Wild {combatant.name} fainted!");

            yield return m_Interface.TypeTextCharByChar(message);
        }

        private IEnumerator CalculateExperienceGainAndLevelUp()
        {
            m_Coordinator.GetPlayerActivePokemon(out var player);
            m_Coordinator.GetEnemyActivePokemon(out var enemy);

            // (base exp gain * level / 7 * 1 / number of participants * 1 * (wild 1 or trainer 1.5) * (traded ? 1 or 1.5)
            int exp = Mathf.FloorToInt(enemy.asset.experience * enemy.level / 7f * 1f / 1f * 1f * 1f * 1f);

            string message = string.Concat($"{player.name} gained {exp} EXP. Points!");

            yield return m_Interface.TypeTextCharByChar(message);

            int totalExp = player.experience + exp;

            while (player.experience < totalExp)
            {
                player.experience = Mathf.FloorToInt(Mathf.Min(totalExp, Mathf.Pow(player.level + 1, 3)));

                yield return m_Interface.SetPlayerStatsPanelExperienceSlider();

                if (player.experience >= Mathf.Pow(player.level + 1, 3))
                {
                    player.LevelUp();

                    m_Coordinator.SetPlayerStatPanelAndAbilityMenuProperties();

                    m_Interface.Show<LevelUpMenu>();

                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

                    m_Interface.Hide<LevelUpMenu>();
                }
            }
        }
    }
}