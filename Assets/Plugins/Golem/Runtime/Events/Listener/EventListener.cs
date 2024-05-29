using System;
using System.Collections;
using System.Collections.Generic;

namespace Golem
{
    internal sealed class EventListener : IEnumerable<KeyValuePair<Type, Delegate>>
    {
        private readonly static Dictionary<Type, Delegate> s_Listeners = new Dictionary<Type, Delegate>();

        internal void Add<T>(Type key, Action<T> handler) where T : EventArgs
        {
            if (s_Listeners.TryGetValue(key, out Delegate value))
            {
                s_Listeners[key] = (Action<T>)value + handler;
            }
            else
            {
                s_Listeners[key] = handler;
            }
        }

        internal void Remove<T>(Type key, Action<T> handler) where T : EventArgs
        {
            if (s_Listeners.TryGetValue(key, out Delegate value))
            {
                var result = (Action<T>)value - handler;

                if (result == null)
                {
                    s_Listeners.Remove(key);
                }
                else
                {
                    s_Listeners[key] = result;
                }
            }
        }

        internal bool TryGetValue(Type key, out Delegate value)
        {
            return s_Listeners.TryGetValue(key, out value);
        }

        internal void Clear()
        {
            s_Listeners.Clear();
        }

        public IEnumerator<KeyValuePair<Type, Delegate>> GetEnumerator()
        {
            return s_Listeners.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
