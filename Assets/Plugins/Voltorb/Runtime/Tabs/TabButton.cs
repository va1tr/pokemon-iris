using UnityEngine;
using UnityEngine.UI;

namespace Voltorb
{
    public class TabButton : MonoBehaviour
    {
        public virtual void Select()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deselect()
        {
            gameObject.SetActive(false);
        }
    }
}
