using UnityEngine;

namespace Eevee
{
    public abstract class EffectSpec
    {
        public readonly ScriptableAbility asset;

        protected readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        protected readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;

        public EffectSpec(ScriptableAbility asset)
        {
            this.asset = asset;
        }

        public virtual void PreApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {
            CanApplyEffectSpec(instigator, target, ref result);
        }

        public abstract System.Collections.IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target, SpecResult result);

        protected virtual bool CanApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {
            return CheckAbilityAccuracy(instigator.pokemon, target.pokemon, ref result);
        }

        private bool CheckAbilityAccuracy(Pokemon instigator, Pokemon target, ref SpecResult result)
        {
            result.success = (Random.value * 100f) <= asset.container.accuracy;

            if (!result.success)
            {
                result.message += string.Concat($"{instigator.name}'s attack missed!\n");
            }

            return result.success;
        }
    }
}
