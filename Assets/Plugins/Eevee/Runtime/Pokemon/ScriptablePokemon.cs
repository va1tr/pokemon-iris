using System;

using UnityEngine;

namespace Eevee
{
    [CreateAssetMenu(menuName = "ScriptableObject/Eevee/Pokemon/Asset", fileName = "New-Pokemon", order = 150)]
    public sealed class ScriptablePokemon : ScriptableObject
    {
        [SerializeField]
        internal new string name;

        [SerializeField]
        private Sprite m_SpriteFront;

        [SerializeField]
        private Sprite m_SpriteBack;

        [SerializeField]
        private uint m_Health;

        [SerializeField]
        private uint m_Attack;

        [SerializeField]
        private uint m_Defence;

        [SerializeField]
        private uint m_Speed;

        [SerializeField]
        private uint m_level;

        [SerializeField]
        private uint m_Experience;

        [SerializeField]
        private LevelRequiredAbility[] m_Abilities;

        public Sprite spriteFront
        {
            get => m_SpriteFront;
        }

        public Sprite spriteBack
        {
            get => m_SpriteBack;
        }

        internal uint health
        {
            get => m_Health;
        }

        internal uint attack
        {
            get => m_Attack;
        }

        internal uint defence
        {
            get => m_Defence;
        }

        internal uint speed
        {
            get => m_Speed;
        }

        internal uint level
        {
            get => m_level;
        }

        public uint experience
        {
            get => m_Experience;
        }

        internal LevelRequiredAbility[] abilities
        {
            get => m_Abilities;
        }
    }

    [Serializable]
    internal sealed class LevelRequiredAbility
    {
        [SerializeField]
        private ScriptableAbility m_Ability;

        [SerializeField]
        private uint m_LevelRequired;

        internal ScriptableAbility ability
        {
            get => m_Ability;
        }

        internal uint levelRequired
        {
            get => m_LevelRequired;
        }
    }
}
