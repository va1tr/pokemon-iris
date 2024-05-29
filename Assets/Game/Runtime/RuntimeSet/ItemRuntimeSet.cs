using System;

using UnityEngine;

using Arbok;
using Golem;

namespace Iris
{
    [CreateAssetMenu(menuName = "ScriptableObject/Iris/RuntimeSet/Item", fileName = "New-Runtime-Item-Set", order = 150)]
    internal sealed class ItemRuntimeSet : RuntimeSet<ItemSpec>
    {
        private const uint kMaxNumberOfItems = 999;
        private const uint kMinNumberOfItems = 1;

        public override void Add(ItemSpec item)
        {
            if (TryGetItemByUniqueId(ref item))
            {
                if (item.count < kMaxNumberOfItems)
                {
                    item.count++;
                }
            }
            else
            {
                m_Collection.Add(item);
            }
        }

        public override void Remove(ItemSpec item)
        {
            if (TryGetItemByUniqueId(ref item))
            {
                if (item.count > kMinNumberOfItems)
                {
                    item.count--;
                }
            }
            else
            {
                m_Collection.Remove(item);
            }
        }

        internal ItemSpec[] GetItemsByType(ItemType match)
        {
            return Array.FindAll(m_Collection.ToArray(), (x) => x.type == match);
        }

        private bool TryGetItemByUniqueId(ref ItemSpec item)
        {
            var items = GetItemsByType(item.type);

            for (int i = 0; i < items.Length; i++)
            {
                if (string.Equals(item.asset.uniqueId, items[i].asset.uniqueId))
                {
                    item = items[i];
                    return true;
                }
            }

            return false;
        }
    }
}
