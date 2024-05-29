using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/Abilities/Tackle", fileName = "New-Tackle-Ability", order = 150)]
    internal sealed class Tackle : ScriptableAbility
    {
        [SerializeField]
        private Sprite m_Sprite;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new TackleAbilitySpec(this, m_Sprite);
        }

        private sealed class TackleAbilitySpec : AbilitySpec
        {
            private readonly Sprite m_Sprite;

            public TackleAbilitySpec(ScriptableAbility asset, Sprite sprite) : base(asset)
            {
                m_Sprite = sprite;
            }

            public override void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PreAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    effectSpec.PreApplyEffectSpec(instigator, target, ref result);
                }
            }

            public override System.Collections.IEnumerator ActivateAbility(Combatant instigator, Combatant target)
            {
                var position = instigator.rectTransform.anchoredPosition;
                var offset = target.rectTransform.anchoredPosition;

                float direction = Mathf.Clamp(Vector2.Dot(Vector2.up, offset - position), -1, 1);

                yield return instigator.rectTransform.Translate(position, Vector3.right * 15f * direction, 0.175f, Space.Self, EasingType.PingPong);

                var image = GameObjectUtility.CreateGameobjectOnCanvas<Image>(target.rectTransform);

                image.rectTransform.sizeDelta = m_Sprite.rect.size;
                image.sprite = m_Sprite;
                image.color = Color.white.AlphaMultiplied(0.75f);

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(offset, Vector3.left * 2f, 0.05f, Space.Self, EasingType.PingPong);
                    yield return target.rectTransform.Translate(offset, Vector3.right * 2f, 0.05f, Space.Self, EasingType.PingPong);
                }

                Destroy(image.gameObject);
            }

        }
    }
}
