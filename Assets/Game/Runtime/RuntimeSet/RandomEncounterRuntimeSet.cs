using System;

using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/RuntimeSet/Encounter", fileName = "New-Runtime-Random-Encounter-Set", order = 150)]
    internal sealed class RandomEncounterRuntimeSet : RuntimeSet<RandomEncounterSet>
    {
        internal static Pokemon CreatePokemonFromSet(RandomEncounterSet set)
        {
            return new Pokemon(set.asset, null, set.level);
        }

        internal RandomEncounterSet GetRandomEncounterFromSet()
        {
            #region Debug
#if UNITY_EDITOR
            VerifyTotalEncounterRateIsEqualToOneHundred();
#endif
            #endregion

            int seed = Mathf.RoundToInt(UnityEngine.Random.value * 100f);

            for (int i = 0; i < Count(); i++)
            {
                seed -= m_Collection[i].encounterRate;

                if (seed <= 0)
                {
                    return m_Collection[i];
                }
            }

            #region Exception
#if UNITY_EDITOR
            throw new ArgumentOutOfRangeException();
#else
            return null;
#endif
            #endregion
        }

        #region Debug
        private void VerifyTotalEncounterRateIsEqualToOneHundred()
        {
            int totalEncounterRate = 0;

            for (int i = 0; i < Count(); i++)
            {
                totalEncounterRate += m_Collection[i].encounterRate;
            }

            if (totalEncounterRate != 100)
            {
                string message = string.Concat($"The total weights for an encounter set must equal 100, " +
                    $"the total encounter weight is {totalEncounterRate}");
                throw new ArgumentOutOfRangeException(message);
            }
        }
        #endregion
    }

    [Serializable]
    internal sealed class RandomEncounterSet
    {
        [SerializeField]
        internal ScriptablePokemon asset;

        [SerializeField, Range(0, 100)]
        internal uint level;

        [SerializeField, Range(0, 100)]
        internal int encounterRate;
    }
}
