using System;
using System.Collections;

using UnityEngine;

using Slowbro;
using Eevee;

namespace Iris
{
    internal sealed class Switch : Move
    {
        private readonly Pokemon m_Current;
        private readonly Pokemon m_SwitchInto;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        private readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;

        public Switch(GameBattleStateBehaviour stateBehaviour, Combatant instigator, Combatant target)
            : base(stateBehaviour, instigator, target)
        {
            m_Current = instigator.pokemon;
            m_SwitchInto = target.pokemon;
        }

        public override IEnumerator Run()
        {
            m_StateBehaviour.GetBattleGraphicsInterface().SetActive<PlayerTrainerPanel>(false);
            m_StateBehaviour.GetBattleGraphicsInterface().SetActive<PlayerPokemonPanel>(true);

            yield return m_DelayForHalfSecond;

            yield return TypeMessageCharByCharWithOneSecondDelay(string.Concat($"That's enough {m_Current.name}!\n"));

            yield return new Parallel(m_StateBehaviour,
                m_StateBehaviour.GetBattleGraphicsInterface().HideAsync<PlayerPokemonPanel>(),
                m_StateBehaviour.GetBattleGraphicsInterface().HideAsync<PlayerStatsPanel>());

            m_Current.activeSelf = false;
            m_SwitchInto.activeSelf = true;

            m_StateBehaviour.GetBattleCoordinator().SetPlayerActivePokemon(m_SwitchInto);
            m_StateBehaviour.GetBattleCoordinator().SetPlayerStatPanelAndAbilityMenuProperties();

            yield return TypeMessageCharByCharWithOneSecondDelay(string.Concat($"Go! {m_SwitchInto.name}!\n"));

            yield return new Parallel(m_StateBehaviour,
                m_StateBehaviour.GetBattleGraphicsInterface().ShowAsync<PlayerPokemonPanel>(),
                m_StateBehaviour.GetBattleGraphicsInterface().ShowAsync<PlayerStatsPanel>());
        }

        private IEnumerator TypeMessageCharByCharWithOneSecondDelay(string message)
        {
            yield return m_StateBehaviour.GetBattleGraphicsInterface().TypeTextCharByChar(message);

            yield return m_DelayForOneSecond;
        }

        public override int CompareTo(Move other)
        {
            return kInstigatorHasPriority;
        }
    }
}