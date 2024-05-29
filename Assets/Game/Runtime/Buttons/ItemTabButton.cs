using UnityEngine;

using Arbok;
using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class ItemTabButton : TabButton
    {
        [SerializeField]
        private ItemRuntimeSet m_ItemRuntimeSet;

        [SerializeField]
        private ItemType m_ItemType;

        private ItemSpec[] items
        {
            get
            {
                if (m_Items == null)
                {
                    m_Items = m_ItemRuntimeSet.GetItemsByType(m_ItemType);
                }

                return m_Items;
            }
        }

        private ItemSpec[] m_Items;

        public override void Select()
        {
            base.Select();

            EventSystem.instance.Invoke(ItemTabButtonSelectedEventArgs.CreateEventArgs(items));
        }
    }
}