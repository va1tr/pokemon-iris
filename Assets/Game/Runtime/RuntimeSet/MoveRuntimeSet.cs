using UnityEngine;

using Golem;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/RuntimeSet/Move", fileName = "New-Runtime-Move-Set", order = 150)]
    internal sealed class MoveRuntimeSet : RuntimeSet<Move>
    {
        internal void Sort()
        {
            m_Collection.Sort();
        }
    
    }
}
