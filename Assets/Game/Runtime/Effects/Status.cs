using System;
using System.Collections;

using UnityEngine;

using Slowbro;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/Effect/Status", fileName = "New-Status-Effect", order = 150)]
    internal sealed class Status : ScriptableEffect
    {
        [SerializeField]
        private Texture2D m_Attack;

        [SerializeField]
        private Texture2D m_Defence;

        [SerializeField]
        private Texture2D m_Speed;

        [SerializeField]
        private Texture2D m_Accuracy;

        [SerializeField]
        private Texture2D m_Evasion;

        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new StatusEffectSpec(asset, this);
        }

        private Texture2D GetTextureForStatistic(StatisticType statType)
        {
            switch (statType)
            {
                case StatisticType.Attack:
                    return m_Attack;
                case StatisticType.Defence:
                    return m_Defence;
                case StatisticType.Speed:
                    return m_Speed;
                case StatisticType.Accuracy:
                    return m_Accuracy;
                case StatisticType.Evasion:
                    return m_Evasion;
                default:
                    break;
            }

#if UNITY_EDITOR
            throw new Exception($"Texture for stat type {statType} was not found");
#else
            return null;
#endif
        }

        private sealed class StatusEffectSpec : EffectSpec
        {
            private readonly Status m_Status;

            public StatusEffectSpec(ScriptableAbility asset, Status status) : base(asset)
            {
                m_Status = status;   
            }

            protected override bool CanApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.CanApplyEffectSpec(instigator, target, ref result);
                
                if (result.success)
                {
                    CheckStatisticStageLevel(instigator.pokemon, target.pokemon, ref result);
                }

                return result.success;
            }

            private bool CheckStatisticStageLevel(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                foreach (var modifier in asset.container.modifiers)
                {
                    switch (modifier.target)
                    {
                        case EffectModifierType.Self:
                            CheckStatisticIsWithinMinAndMaxRange(instigator, modifier.stat, ref result);
                            break;
                        case EffectModifierType.Target:
                            CheckStatisticIsWithinMinAndMaxRange(target, modifier.stat, ref result);
                            break;
                    }

                    if (!result.success)
                    {
                        break;
                    }
                }

                return result.success;
            }

            private bool CheckStatisticIsWithinMinAndMaxRange(Pokemon combatant, StatisticType statType, ref SpecResult result)
            {
                if (combatant.TryGetStatistic(statType, out Statistic stat))
                {
                    if (stat.stage >= 6)
                    {
                        result.message += string.Concat($"{combatant.name} {statType} won't go higher!\n");
                        result.success = false;
                    }
                    else if (stat.stage <= -6)
                    {
                        result.message += string.Concat($"{combatant.name} {statType} won't go lower!\n");
                        result.success = false;
                    }
                }

                return result.success;
            }

            // TODO Consider using a list to sort them by priority; target debuff, target buff, self debuff, self buff or whatever the order is...
            public override IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target, SpecResult result)
            {
                var modifiers = asset.container.modifiers;

                foreach (var modifier in GetAllTargetDebuffModifiers(modifiers))
                {
                    ApplyStageModifiersToCombatant(target, modifier.stat, modifier.multiplier, ref result);

                    yield return target.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, -1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(result);

                result.message = string.Empty;

                foreach (var modifier in GetAllTargetBuffModifiers(modifiers))
                {
                    ApplyStageModifiersToCombatant(target, modifier.stat, modifier.multiplier, ref result);

                    yield return target.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, 1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(result);

                result.message = string.Empty;

                foreach (var modifier in GetAllSelfTargetDebuffModifiers(modifiers))
                {
                    ApplyStageModifiersToCombatant(instigator, modifier.stat, modifier.multiplier, ref result);

                    yield return instigator.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, -1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(result);

                result.message = string.Empty;

                foreach (var modifier in GetAllSelfTargetBuffModifiers(modifiers))
                {
                    ApplyStageModifiersToCombatant(instigator, modifier.stat, modifier.multiplier, ref result);

                    yield return instigator.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, 1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(result);

                result.message = string.Empty;
            }

            private void ApplyStageModifiersToCombatant(Combatant combatant, StatisticType statType, int multiplier, ref SpecResult result)
            {
                var pokemon = combatant.pokemon;

                if (pokemon.TryGetStatistic(statType, out Statistic stat))
                {
                    stat.stage += multiplier;

                    if (multiplier < 0)
                    {
                        result.message += string.Concat($"{pokemon.name}'s {statType} fell!\n");
                    }
                    else if (multiplier > 0)
                    {
                        result.message += string.Concat($"{pokemon.name}'s {statType} rose!\n");
                    }
                }
            }

            private IEnumerator TypeStatusCharByCharWithOneSecondDelay(SpecResult result)
            {
                // TODO. I dunno about this...
                var graphicsInterface = FindObjectOfType<BattleGraphicsInterface>();

                var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < messages.Length; i++)
                {
                    yield return graphicsInterface.TypeTextCharByChar(messages[i]);
                    yield return m_DelayForOneSecond;
                }
            }

            private static EffectModifiers[] GetAllTargetDebuffModifiers(EffectModifiers[] modifiers)
            {
                return Array.FindAll(modifiers, (x) => x.target == EffectModifierType.Target && x.multiplier < 0);
            }

            private static EffectModifiers[] GetAllTargetBuffModifiers(EffectModifiers[] modifiers)
            {
                return Array.FindAll(modifiers, (x) => x.target == EffectModifierType.Target && x.multiplier > 0);
            }

            private static EffectModifiers[] GetAllSelfTargetDebuffModifiers(EffectModifiers[] modifiers)
            {
                return Array.FindAll(modifiers, (x) => x.target == EffectModifierType.Self && x.multiplier < 0);
            }

            private static EffectModifiers[] GetAllSelfTargetBuffModifiers(EffectModifiers[] modifiers)
            {
                return Array.FindAll(modifiers, (x) => x.target == EffectModifierType.Self && x.multiplier > 0);
            }
        }
    }
}
