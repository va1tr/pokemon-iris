using UnityEngine;

namespace Eevee
{
    public abstract class ScriptableAbility : ScriptableObject
    {
        [SerializeField]
        private string m_AbilityName;

        [SerializeField]
        private ScriptableEffect m_Effect;

        [SerializeField]
        private Container m_Container = new Container();

        public string abilityName
        {
            get => m_AbilityName;
        }

        public ScriptableEffect effect
        {
            get => m_Effect;
        }

        public Container container
        {
            get => m_Container;
        }

        public abstract AbilitySpec CreateAbilitySpec();
    }
}