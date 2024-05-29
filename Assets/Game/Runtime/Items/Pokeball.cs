using System.Collections;

using UnityEngine;

using Arbok;
using Eevee;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/Items/Pokeball", fileName = "New-Pokeball-Item", order = 150)]
    internal sealed class Pokeball : ScriptableItem
    {
        [SerializeField]
        private int m_CatchRate;

        public override ItemSpec CreateItemSpec()
        {
            return new PokeballSpec(this);
        }

        private sealed class PokeballSpec : ItemSpec
        {
            public PokeballSpec(ScriptableItem asset, int count = 1) : base(asset, count)
            {

            }

            public override IEnumerator UseItem(Combatant target)
            {
                // TODO Implement this...

                yield break;
            }
        }
    }
}
