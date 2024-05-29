using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Golem
{
    public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField]
        protected List<T> m_Collection = new List<T>();

        public T this[int index]
        {
            get => m_Collection[index];
        }

        public int Count()
        {
            return m_Collection.Count;
        }

        public virtual void Add(T item)
        {
            if (!m_Collection.Contains(item))
            {
                m_Collection.Add(item);
            }
        }

        public virtual void Remove(T item)
        {
            if (m_Collection.Contains(item))
            {
                m_Collection.Remove(item);
            }
        }

        public virtual void RemoveAt(int index)
        {
            if (m_Collection.Count <= index)
            {
                m_Collection.RemoveAt(index);
            }
        }

        public virtual void Clear()
        {
            m_Collection.Clear();
        }

        public virtual void ClearAndTrimExcess()
        {
            m_Collection.Clear();
            m_Collection.TrimExcess();
        }

        public T[] ToArray()
        {
            return m_Collection.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
