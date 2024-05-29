using UnityEngine;

namespace Golem
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<T>();

                    if (s_Instance == null)
                    {
                        s_Instance = CreateGameObjectInstanceAndDontSave();
                    }
                }

                return s_Instance;
            }
        }

        private static T s_Instance;

        protected virtual void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = (T)this;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError(string.Concat($"Multiple instances of singleton type {typeof(T).Name}"));
            }
#endif
        }

        private static T CreateGameObjectInstanceAndDontSave()
        {
            return new GameObject(typeof(T).Name).AddComponent<T>().GetComponent<T>();
        }
    }
}
