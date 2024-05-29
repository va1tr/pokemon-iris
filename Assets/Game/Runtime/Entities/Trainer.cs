using System;

using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    internal class Trainer : Summoner, IPersistable
    {
        [SerializeField]
        private TrainerRuntimeSet m_TrainerSet;

        [SerializeField]
        private PokemonRuntimeSet m_PokemonRuntimeSet;

        [SerializeField]
        private DataSettings m_DataSettings;

        private TrainerDataStorage m_DataStorage = new TrainerDataStorage();

        protected override void Start()
        {
            if (!m_DataStorage.isInitialised)
            {
                CreateStartupPokemonParty();

                m_DataStorage.isInitialised = true;
            }
        }

        protected override void CreateStartupPokemonParty()
        {
            int count = m_TrainerSet.Count();

            for (int i = 0; i < count; i++)
            {
                m_PokemonRuntimeSet.Add(TrainerRuntimeSet.CreatePokemonFromSet(m_TrainerSet[i]));
            }
        }

        public override Pokemon GetActiveOrFirstPokemonThatIsNotFainted()
        {
            int count = m_PokemonRuntimeSet.Count();

            for (int i = 0; i < count; i++)
            {
                var pokemon = m_PokemonRuntimeSet[i];

                if (pokemon != null && pokemon.activeSelf)
                {
                    return pokemon;
                }
            }

            return GetFirstPokemonThatIsNotFainted();
        }

        public override Pokemon GetFirstPokemonThatIsNotFainted()
        {
            int count = m_PokemonRuntimeSet.Count();

            for (int i = 0; i < count; i++)
            {
                var pokemon = m_PokemonRuntimeSet[i];

                if (pokemon != null && !pokemon.isFainted)
                {
                    pokemon.activeSelf = true;
                    return pokemon;
                }
            }

            #region Debug
#if UNITY_EDITOR
            throw new ArgumentOutOfRangeException("No non-fainted pokemon found!");
#else
            return null;
#endif
            #endregion
        }

        public void LoadInternalDataStorage(DataStorage dataStorage)
        {
            m_DataStorage = (TrainerDataStorage)dataStorage;
        }

        public DataSettings GetDataSettings()
        {
            return m_DataSettings;
        }

        public DataStorage GetDataStorage()
        {
            return m_DataStorage;
        }

        private sealed class TrainerDataStorage : DataStorage
        {
            internal bool isInitialised;
        }
    }
}
