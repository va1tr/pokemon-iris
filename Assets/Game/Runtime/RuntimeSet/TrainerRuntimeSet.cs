using System;

using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/RuntimeSet/TrainerSet", fileName = "New-Runtime-Trainer-Set", order = 150)]
    internal sealed class TrainerRuntimeSet : RuntimeSet<TrainerSet>
    {
        internal static Pokemon CreatePokemonFromSet(TrainerSet set)
        {
            return new Pokemon(set.asset, null, set.level);
        }
    }

    [Serializable]
    internal sealed class TrainerSet
    {
        [SerializeField]
        internal ScriptablePokemon asset;

        [SerializeField, Range(1, 100)]
        internal uint level;
    }
}
