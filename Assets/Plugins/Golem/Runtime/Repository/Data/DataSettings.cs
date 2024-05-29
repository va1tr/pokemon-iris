using System;

using UnityEngine;

namespace Golem
{
    [Serializable]
    public sealed class DataSettings
    {
        [SerializeField]
        private string m_UniqueID;

        [SerializeField]
        private PersistenceType m_PersistanceType;

        public string uniqueID
        {
            get => m_UniqueID;
            set => m_UniqueID = value;
        }

        public PersistenceType persistanceType
        {
            get => m_PersistanceType;
            set => m_PersistanceType = value;
        }
    }

}
