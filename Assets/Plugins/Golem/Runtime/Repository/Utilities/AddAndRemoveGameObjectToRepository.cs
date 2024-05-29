using UnityEngine;

namespace Golem
{
    internal sealed class AddAndRemoveGameObjectToRepository : MonoBehaviour
    {
        private void Awake()
        {
            Repository.instance.Add(GetComponent<IPersistable>());
        }

        private void OnDestroy()
        {
            Repository.instance.Remove(GetComponent<IPersistable>());
        }
    }
}
