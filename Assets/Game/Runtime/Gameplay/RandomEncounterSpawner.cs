using System;

using UnityEngine;

using Eevee;

namespace Iris
{
    internal sealed class RandomEncounterSpawner : Summoner
    {
        [SerializeField]
        private RandomEncounterRuntimeSet m_EncounterSet;

        private Pokemon m_Pokemon;

        protected override void CreateStartupPokemonParty()
        {
            m_Pokemon = RandomEncounterRuntimeSet.CreatePokemonFromSet(m_EncounterSet.GetRandomEncounterFromSet());
        }

        public override Pokemon GetActiveOrFirstPokemonThatIsNotFainted()
        {
            return GetFirstPokemonThatIsNotFainted();
        }

        public override Pokemon GetFirstPokemonThatIsNotFainted()
        {
            if (!m_Pokemon.isFainted)
            {
                m_Pokemon.activeSelf = true;
                return m_Pokemon;
            }

            #region Debug
#if UNITY_EDITOR
            throw new ArgumentOutOfRangeException("No non-fainted pokemon found!");
#else
            return null;
#endif
            #endregion
        }
    }
}
