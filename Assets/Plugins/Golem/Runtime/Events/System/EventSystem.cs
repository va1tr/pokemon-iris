using System;

namespace Golem
{ 
    public sealed class EventSystem : Singleton<EventSystem>
    {
        private readonly static EventListener s_Listeners = new EventListener();

        public void AddListener<T>(Action<T> handler) where T : EventArgs
        {
            var key = typeof(T);

            s_Listeners.Add(key, handler);
        }

        public void RemoveListener<T>(Action<T> handler) where T : EventArgs
        {
            var key = typeof(T);

            s_Listeners.Remove(key, handler);
        }

        public void Invoke<T>(T args) where T : EventArgs
        {
            var key = typeof(T);

            if (s_Listeners.TryGetValue(key, out Delegate handler))
            {
                ((Action<T>)handler)?.Invoke(args);
            }
        }
    }
}
