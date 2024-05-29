using System;
using System.Collections.Generic;

namespace Golem
{
    public sealed class Repository : Singleton<Repository>
    {
        private List<IPersistable> m_Entities = new List<IPersistable>();
        private Dictionary<string, DataStorage> m_Storage = new Dictionary<string, DataStorage>();

        public void SaveAllDataInternal()
        {
            int count = m_Entities.Count;

            for (int i = 0; i < count; i++)
            {
                Save(m_Entities[i]);
            }
        }

        private void Save(IPersistable entry)
        {
            var dataSettings = entry.GetDataSettings();

            if (dataSettings.persistanceType == PersistenceType.WriteOnly || dataSettings.persistanceType == PersistenceType.ReadAndWrite)
            {
                string key = dataSettings.uniqueID;

                if (!string.IsNullOrEmpty(key))
                {
                    m_Storage[key] = entry.GetDataStorage();
                }
            }
        }

        public void LoadAllDataInternal()
        {
            int count = m_Entities.Count;

            for (int i = 0; i < count; i++)
            {
                Load(m_Entities[i]);
            }
        }

        private void Load(IPersistable entry)
        {
            var dataSettings = entry.GetDataSettings();

            if (dataSettings.persistanceType == PersistenceType.ReadOnly || dataSettings.persistanceType == PersistenceType.ReadAndWrite)
            {
                string key = dataSettings.uniqueID;

                if (!string.IsNullOrEmpty(key) && m_Storage.ContainsKey(key))
                {
                    entry.LoadInternalDataStorage(m_Storage[key]);
                }
            }
        }

        public void Add(IPersistable entry)
        {
            #region Debug
#if UNITY_EDITOR
            if (m_Entities.Contains(entry))
            {
                string message = string.Concat($"The identifier for {entry.GetType().Name} has already been added " +
                    $"change the unique id so each persistable data can access their respective storage.");
                throw new Exception(message);
            }
#endif
            #endregion
            m_Entities.Add(entry);
        }

        public void Remove(IPersistable entry)
        {
            #region Debug
#if UNITY_EDITOR
            if (!m_Entities.Contains(entry))
            {
                string message = string.Concat($"The identifier for {entry.GetType().Name} was not present even " +
                    $"though it was trying to be removed.");
                throw new KeyNotFoundException(message);
            }
#endif
            #endregion
            m_Entities.Remove(entry);
        }
    }

}
