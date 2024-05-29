using System;
using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    internal sealed class BattleBeginState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        private BattleCoordinator m_Coordinator;
        private BattleGraphicsInterface m_Interface;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);

        private const float kDelayForHalfSecond = 0.5f;

        public BattleBeginState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;
        }

        public override void Enter()
        {
            m_Coordinator = m_StateBehaviour.GetBattleCoordinator();
            m_Interface = m_StateBehaviour.GetBattleGraphicsInterface();

            m_Interface.HideAll();

            m_StateBehaviour.StartCoroutine(WildPokemonBattleStartSequence());
        }

        private IEnumerator WildPokemonBattleStartSequence()
        {
            yield return new Parallel(m_StateBehaviour,
                m_Interface.ShowAsync<PlayerPanel>(),
                m_Interface.ShowAsync<PlayerTrainerPanel>(),
                m_Interface.ShowAsync<EnemyPanel>(),
                m_Interface.ShowAsync<EnemyPokemonPanel>());

            yield return new Parallel(m_StateBehaviour, PrintEncounterNameCharByChar(),
                m_Interface.ShowAsync<EnemyStatsPanel>());

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            yield return new Parallel(m_StateBehaviour, PrintSummonNameCharByChar(),
                m_Interface.HideAsync<PlayerTrainerPanel>());

            yield return new Parallel(m_StateBehaviour,
                m_Interface.ShowAsync<PlayerPokemonPanel>(),
                m_Interface.ShowAsync<PlayerStatsPanel>());

            yield return m_DelayForHalfSecond;

            m_StateBehaviour.ChangeState(BattleState.Wait);
        }

        private IEnumerator PrintEncounterNameCharByChar()
        {
            m_Coordinator.GetEnemyActivePokemon(out var combatant);

            string message = string.Concat($"A wild {combatant.name} appeared!");

            yield return m_Interface.TypeTextCharByChar(message);
        }

        private IEnumerator PrintSummonNameCharByChar()
        {
            m_Coordinator.GetPlayerActivePokemon(out var combatant);

            string message = string.Concat($"Go! {combatant.name}!");

            yield return m_Interface.TypeTextCharByChar(message);
        }

        public override void Exit()
        {
            m_StateBehaviour.StopCoroutine(WildPokemonBattleStartSequence());
        }
    }
}