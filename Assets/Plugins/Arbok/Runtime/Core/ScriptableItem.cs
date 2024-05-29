using System;

using UnityEngine;

namespace Arbok
{
    public abstract class ScriptableItem : ScriptableObject
    {
        [SerializeField]
        private string m_UniqueID = Guid.NewGuid().ToString();

        [SerializeField]
        private string m_ItemName;

        [SerializeField]
        private string m_Description;

        [SerializeField]
        private Sprite m_Sprite;

        [SerializeField]
        private ItemType m_Type;

        public string uniqueId
        {
            get => m_UniqueID;
        }

        public string itemName
        {
            get => m_ItemName;
        }

        public string description
        {
            get => m_Description;
        }

        public Sprite sprite
        {
            get => m_Sprite;
        }

        public ItemType type
        {
            get => m_Type;
        }

        public abstract ItemSpec CreateItemSpec();
    }
}
