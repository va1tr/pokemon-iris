using UnityEngine;

using Voltorb;

namespace Iris
{
    internal class PartyGraphicsInterface : GraphicalUserInterface
    {
        [SerializeField]
        private PartyPokemonMenu m_PartyPokemonMenu;

        [SerializeField]
        private PartyOptionsMenu m_PartyOptionsMenu;

        [SerializeField]
        private Typewriter m_TextProcessor;

        protected override void BindSceneGraphicReferences()
        {
            Add(m_PartyPokemonMenu);
            Add(m_PartyOptionsMenu);
        }

        internal void SetPartyMenuProperties(PartyGraphicProperties props)
        {
            SetProperties(typeof(PartyPokemonMenu).Name, props);
        }

        internal void PrintCompletedText(string message)
        {
            m_TextProcessor.PrintCompletedText(message);
        }
    }
}
