using System;
using System.Collections;

using UnityEngine;

using Slowbro;
using Eevee;

namespace Iris
{
    internal sealed class Fight : Move
    {
        private readonly AbilitySpec m_AbilitySpec;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        private readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;

        public Fight(GameBattleStateBehaviour stateBehaviour, Combatant instigator, Combatant target, AbilitySpec abilitySpec)
            : base(stateBehaviour, instigator, target)
        {
            m_AbilitySpec = abilitySpec;
        }

        // It's still kinda ass, but it'll do.
        public override IEnumerator Run()
        {
            m_AbilitySpec.PreAbilityActivate(instigator, target, out SpecResult result);

            yield return TypeSpecResultMessagesCharByCharWithHalfSecondDelay(result);

            if (result.success)
            {
                yield return m_AbilitySpec.ActivateAbility(instigator, target);

                yield return m_DelayForHalfSecond;

                result.message = string.Empty;

                yield return m_AbilitySpec.effectSpec.ApplyEffectSpec(instigator, target, result);

                yield return m_DelayForHalfSecond;

                result.message = string.Empty;
            }

            m_StateBehaviour.GetBattleGraphicsInterface().CleanupTextProcessorAndClearText();
        }

        private IEnumerator TypeSpecResultMessagesCharByCharWithHalfSecondDelay(SpecResult result)
        {
            var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messages.Length; i++)
            {
                yield return m_StateBehaviour.GetBattleGraphicsInterface().TypeTextCharByChar(messages[i]);

                yield return m_DelayForHalfSecond;
            }
        }

        private IEnumerator TypeSpecResultMessagesCharByCharWithOneSecondDelay(SpecResult result)
        {
            var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messages.Length; i++)
            {
                yield return m_StateBehaviour.GetBattleGraphicsInterface().TypeTextCharByChar(messages[i]);

                yield return m_DelayForOneSecond;
            }
        }

        public override int CompareTo(Move other)
        {
            if (instigator.pokemon.speed.value > other.instigator.pokemon.speed.value)
            {
                return kInstigatorHasPriority;
            }
            else if (instigator.pokemon.speed.value == other.instigator.pokemon.speed.value)
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    return kInstigatorHasPriority;
                }
                else
                {
                    return kInstigatorDoesNotHavePriority;
                }
            }
            else
            {
                return kInstigatorDoesNotHavePriority;
            }
        }
    }
}