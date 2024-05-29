using UnityEngine;

using Arbok;

namespace Iris
{
    internal sealed class Inventory : MonoBehaviour
    {
        [SerializeField]
        private ItemRuntimeSet m_ItemRuntimeSet;

        [SerializeField]
        private ScriptableItem[] m_StartingItems;

        private void Start()
        {
            CreateStartupInventory();
        }

        private void CreateStartupInventory()
        {
            int length = m_StartingItems.Length;

            for (int i = 0; i < length; i++)
            {
                m_ItemRuntimeSet.Add(m_StartingItems[i].CreateItemSpec());
            }
        }
    }
}
