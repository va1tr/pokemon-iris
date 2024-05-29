using UnityEngine;
using UnityEngine.UI;

using Arbok;
using Golem;

namespace Iris
{
    [RequireComponent(typeof(Button))]
    internal sealed class ItemButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_Button;

        [SerializeField]
        private Text m_ItemName;

        [SerializeField]
        private Text m_ItemCount;

        private ItemSpec m_Item;

        internal void SetProperties(ItemSpec item)
        {
            m_Item = item;

            m_ItemName.text = string.Concat($"{m_Item.name.ToUpper()}");
            m_ItemCount.text = string.Concat($"x {m_Item.count}");
        }

        private void OnEnable()
        {
            m_Button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            EventSystem.instance.Invoke(ItemButtonClickedEventArgs.CreateEventArgs(m_Item));
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveListener(OnButtonClicked);
        }
    }

}
