using UnityEngine;

namespace Golem
{
    internal sealed class AddGameObjectToRuntimeSet : MonoBehaviour
    {
        [SerializeField]
        private GameObjectRuntimeSet m_RuntimeSet;

        private void Awake()
        {
            m_RuntimeSet.Add(gameObject);
        }

        private void OnDestroy()
        {
            m_RuntimeSet.Remove(gameObject);
        }
    }
}
