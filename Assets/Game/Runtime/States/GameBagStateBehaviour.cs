using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class GameBagStateBehaviour : StateBehaviour<GameMode>
    {
        [SerializeField]
        private MoveRuntimeSet m_MoveRuntimeSet;

        [SerializeField]
        private ItemRuntimeSet m_ItemRuntimeSet;

        public override GameMode uniqueId => GameMode.Bag;


    }
}
