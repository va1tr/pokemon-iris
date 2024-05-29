using System.Collections;
using UnityEngine;

namespace Voltorb
{
    public abstract class Menu : MonoBehaviour
    {
        private void OnEnable()
        {
            AddListeners();
        }

        protected virtual void AddListeners()
        {

        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        protected virtual void RemoveListeners()
        {

        }

        public virtual IEnumerator ShowAsync()
        {
            gameObject.SetActive(true);
            yield break;
        }

        public virtual IEnumerator HideAsync()
        {
            gameObject.SetActive(false);
            yield break;
        }
    }

    public abstract class Menu<T> : Menu where T : struct
    {
        public abstract void SetProperties(T properties);
    }
}
