using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Voltorb
{
    public abstract class GraphicalUserInterface : MonoBehaviour
    {
        private readonly Dictionary<string, Menu> m_SceneGraphicReferences = new Dictionary<string, Menu>();

        private void Awake()
        {
            BindSceneGraphicReferences();
        }

        protected abstract void BindSceneGraphicReferences();

        protected void Add<T>(T entry, string name = null) where T : Menu
        {
            var key = name ?? typeof(T).Name;

            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                m_SceneGraphicReferences.Add(key, entry);
            }
        }

        public void Show<T>(string name = null) where T : Menu
        {
            StartCoroutine(ShowAsync<T>(name));
        }

        public IEnumerator ShowAsync<T>(string name = null) where T : Menu
        {
            var key = name ?? typeof(T).Name;
#region Debug
    #if UNITY_EDITOR
            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                throw new KeyNotFoundException($"key for graphic type {key} was not registered");
            }
    #endif
#endregion
            yield return m_SceneGraphicReferences[key].ShowAsync();
        }

        public void ShowAll()
        {
            foreach (var graphic in m_SceneGraphicReferences.Values)
            {
                graphic.gameObject.SetActive(true);
            }
        }

        public void Hide<T>(string name = null) where T : Menu
        {
            StartCoroutine(HideAsync<T>(name));
        }

        public IEnumerator HideAsync<T>(string name = null) where T : Menu
        {
            var key = name ?? typeof(T).Name;
#region Debug
    #if UNITY_EDITOR
            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                throw new KeyNotFoundException($"key for graphic type {key} was not registered");
            }
    #endif
#endregion
            yield return m_SceneGraphicReferences[key].HideAsync();
        }

        public void HideAll()
        {
            foreach (var graphic in m_SceneGraphicReferences.Values)
            {
                graphic.gameObject.SetActive(false);
            }
        }

        public void SetProperties<T>(string key, T props) where T : struct
        {
#region Debug
#if UNITY_EDITOR
            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                throw new KeyNotFoundException($"key for graphic type {key} was not registered");
            }
#endif
            #endregion
            ((Menu<T>)m_SceneGraphicReferences[key]).SetProperties(props);
        }

        public void SetActive<T>(bool value, string name = null) where T : Menu
        {
            var key = name ?? typeof(T).Name;
#region Debug
    #if UNITY_EDITOR
            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                throw new KeyNotFoundException($"key for graphic type {key} was not registered");
            }
    #endif
#endregion
            m_SceneGraphicReferences[key].gameObject.SetActive(value);
        }

        public T Get<T>(string name = null) where T : Menu
        {
            var key = name ?? typeof(T).Name;
#region Debug
    #if UNITY_EDITOR
            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                throw new KeyNotFoundException($"key for graphic type {key} was not registered");
            }
#endif
            #endregion
            return (T)m_SceneGraphicReferences[key];
        }

        private void OnDestroy()
        {
            ClearAllSceneGraphicReferences();
        }

        private void ClearAllSceneGraphicReferences()
        {
            m_SceneGraphicReferences.Clear();
        }
    }
}