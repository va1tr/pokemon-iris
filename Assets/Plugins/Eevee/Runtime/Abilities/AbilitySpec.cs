namespace Eevee
{
    public abstract class AbilitySpec
    {
        public readonly ScriptableAbility asset;
        public readonly EffectSpec effectSpec;

        public readonly string name;

        public AbilitySpec(ScriptableAbility asset)
        {
            this.asset = asset;

            name = asset.abilityName;

            effectSpec = asset.effect.CreateEffectSpec(asset);
        }

        public virtual void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);

            if (CanActivateAbility(ref result))
            {
                result.message = string.Concat($"{instigator.pokemon.name} used {asset.abilityName}!\n");
            }
        }

        public abstract System.Collections.IEnumerator ActivateAbility(Combatant instigator, Combatant target);

        public virtual void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);
        }

        protected virtual bool CanActivateAbility(ref SpecResult result)
        {
            return true;
        }
    }

    public sealed class SpecResult
    {
        public string message;
        public bool success;

        public SpecResult(string message, bool success)
        {
            this.message = message;
            this.success = success;
        }

        public static SpecResult CreateSpecResult(string message, bool success)
        {
            return new SpecResult(message, success);
        }
    }
}