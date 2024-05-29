using System;
using System.Collections;

using UnityEngine;

using Slowbro;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/Effect/Damage", fileName = "New-Damage-Effect", order = 150)]
    internal sealed class Damage : ScriptableEffect
    {
        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new DamageEffectSpec(asset);
        }

        private sealed class DamageEffectSpec : EffectSpec
        {
            public DamageEffectSpec(ScriptableAbility asset) : base(asset)
            {

            }

            public override IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target, SpecResult result)
            {
                result.message = string.Empty;

                CalculateDamageDealt(instigator.pokemon, target.pokemon, ref result);

                yield return OnDamageEffectSpec(instigator, target, result);
            }

            private void CalculateDamageDealt(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                float power = asset.container.power;
                float attack = instigator.attack.valueModified;
                float defence = target.defence.valueModified;

                // TODO. Change it so that crits aren't 25%, this just for testing.
                int critical = ((1f / 4f * 100f) > (UnityEngine.Random.value * 100f)) ? 2 : 1;
                float random = UnityEngine.Random.Range(85f, 100f) / 100f;

                float damage = Mathf.Floor((((2f * instigator.level / 5f + 2f) * power * attack / defence / 50f) + 2f) * critical * random);

#if UNITY_EDITOR
                Debug.Log($"CRIT: {critical}, RNG: {random}, DMG: {damage}");
#endif

                target.health.value = Mathf.Max(target.health.value - damage, 0f);

                if (critical > 1)
                {
                    result.message += string.Concat($"A critical hit!\n");
                }
            }

            private IEnumerator OnDamageEffectSpec(Combatant instigator, Combatant target, SpecResult result)
            {
                // TODO. I dunno about this...
                var graphicsInterface = FindObjectOfType<BattleGraphicsInterface>();

                switch (target.affinity)
                {
                    case Affinity.Friendly:
                        yield return new Parallel(graphicsInterface, graphicsInterface.ShakePlayerStatsPanel(),
                            graphicsInterface.FlashPlayerPokemonImageOnDamage());
                        yield return graphicsInterface.SetPlayerStatsPanelHealthSlider();
                        break;
                    case Affinity.Hostile:
                        yield return new Parallel(graphicsInterface, graphicsInterface.ShakeEnemyStatsPanel(),
                            graphicsInterface.FlashEnemyPokemonImageOnDamage());
                        yield return graphicsInterface.SetEnemyStatsPanelHealthSlider();
                        break;
                }

                var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < messages.Length; i++)
                {
                    yield return graphicsInterface.TypeTextCharByChar(messages[i]);
                    yield return m_DelayForOneSecond;
                }
            }

        }

    }
}