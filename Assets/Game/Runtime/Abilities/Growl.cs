using UnityEngine;

using Slowbro;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/Abilities/Growl", fileName = "New-Growl-Ability", order = 150)]
    internal sealed class Growl : ScriptableAbility
    {
        [SerializeField]
        private GameObject m_Prefab;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new GrowlAbilitySpec(this, m_Prefab);
        }

        private sealed class GrowlAbilitySpec : AbilitySpec
        {
            private readonly GameObject m_Prefab;

            public GrowlAbilitySpec(ScriptableAbility asset, GameObject prefab) : base(asset)
            {
                m_Prefab = prefab;
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

                var instance = GameObjectUtility.CreateGameobjectOnCanvas<RectTransform>(m_Prefab, instigator.rectTransform);

                instance.anchoredPosition += new Vector2(instance.sizeDelta.x / 4, instance.sizeDelta.y / 16) * direction;

                var subEmitters = instance.GetComponentsInChildren<ParticleSystem>();

                foreach (var ps in subEmitters)
                {
                    var velocityOverLifetime = ps.velocityOverLifetime;
                    velocityOverLifetime.xMultiplier *= direction;

                    var renderer = ps.GetComponent<ParticleSystemRenderer>();

                    if (direction < 0)
                    {
                        renderer.flip = Vector3.up * Mathf.Abs(direction);
                    }
                    else
                    {
                        renderer.flip = Vector3.zero;
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(offset, Vector3.left * 2f, 0.2f, Space.Self, EasingType.PingPong);
                    yield return target.rectTransform.Translate(offset, Vector3.right * 2f, 0.2f, Space.Self, EasingType.PingPong);
                }

                Destroy(instance.gameObject);
            }

        }

    }
}


/*
 
                //yield return image.Animate(((Growl)asset).m_Sprites, 24f, 4);
                //for (int i = 0; i < 4; i++)
                //{
                //    yield return image.Animate(((Growl)asset).m_Sprites, 12f);
                //}

                //yield return image.Animate(((Growl)asset).m_Sprites, 12f);

                //yield return new Parallel(instigator, image.Animate(((Growl)asset).m_Sprites, 12f, 4),
                //    image.rectTransform.Translate(position + instigator.rectTransform.sizeDelta / 2 * direction, Vector3.one * 32f * direction, 1f, Space.Self, EasingType.EaseOutSine));


                var image = CreateHiddenGameObjectInstanceAndDontSave<Image>();

                image.transform.SetParent(instigator.transform.parent);

                image.rectTransform.anchorMin = instigator.rectTransform.anchorMin;
                image.rectTransform.anchorMax = instigator.rectTransform.anchorMax;

                image.rectTransform.pivot = instigator.rectTransform.pivot;

                image.rectTransform.position = instigator.rectTransform.position;
                image.rectTransform.localScale = instigator.rectTransform.localScale;
                image.rectTransform.sizeDelta = ((Growl)asset).m_Sprites[0].rect.size;

                var start = position + instigator.rectTransform.sizeDelta / 2.5f * direction;

                yield return new Parallel(instigator,
                    AnimateGrowlSpriteOnImage(instigator, image, start, Vector3.up * 24f * direction, 0, 1),
                    AnimateGrowlSpriteOnImage(instigator, image, start, Vector3.right * 24f * direction, 2, 3),
                    AnimateGrowlSpriteOnImage(instigator, image, start, Vector3.down * 24f * direction, 4, 5));



 */ 