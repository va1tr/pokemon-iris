using System;

using UnityEngine;

namespace Golem
{
    [CreateAssetMenu(menuName = "ScriptableObject/Golem/RuntimeSet/GameObject", fileName = "New-Runtime-GameObject-Set", order = 150)]
    public sealed class GameObjectRuntimeSet : RuntimeSet<GameObject>
    {
        private const int kMinValueForFirstItem = 0;

        public T GetComponentFromRuntimeSet<T>() where T : MonoBehaviour
        {
            return GetFirstGameObject().GetComponent<T>();
        }

        public GameObject GetFirstGameObject()
        {
#region Debug
    #if UNITY_EDITOR
            VerifyRuntimeSetCountIsGreaterThanZero();
    #endif
#endregion
            return m_Collection[kMinValueForFirstItem];
        }
#region Debug
    #if UNITY_EDITOR
        private void VerifyRuntimeSetCountIsGreaterThanZero()
        {
            if (Count() <= 0)
            {
                string message = string.Concat($"The count for gameobject runtime set {name} " +
                    $"is currently: {Count()}, amount must be at least one.");
                throw new ArgumentOutOfRangeException(message);
            }
        }
    #endif
#endregion

    }
}
