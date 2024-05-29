using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/RuntimeSet/Pokemon", fileName = "New-Runtime-Pokemon-Set", order = 150)]
    internal sealed class PokemonRuntimeSet : RuntimeSet<Pokemon>
    {
        private const int kMaxNumberOfPartyMembers = 6;

        public override void Add(Pokemon item)
        {
            if (m_Collection.Count < kMaxNumberOfPartyMembers)
            {
                base.Add(item);
            }
        }

        internal bool TryGetActivePokemon(out Pokemon pokemon)
        {
            pokemon = null;

            for (int i = 0; i < Count(); i++)
            {
                pokemon = m_Collection[i];

                if (pokemon.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
