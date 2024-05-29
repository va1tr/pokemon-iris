using UnityEngine;

using Golem;
using Voltorb;

namespace Iris
{
    internal class ItemScrollViewNavigation : ScrollViewNavigation
    {
        [SerializeField]
        private ItemButton[] m_ItemButtons;

        private void OnEnable()
        {
            EventSystem.instance.AddListener<ItemTabButtonSelectedEventArgs>(OnItemTabButtonSelected);
        }

        private void OnItemTabButtonSelected(ItemTabButtonSelectedEventArgs args)
        {
            int length = m_ItemButtons.Length;

            for (int i = 0; i < length; i++)
            {
                var itemButton = m_ItemButtons[i];

                if (i < args.items.Length)
                {
                    itemButton.gameObject.SetActive(true);
                    itemButton.SetProperties(args.items[i]);
                }
                else
                {
                    itemButton.gameObject.SetActive(false);
                }
            }
        }

        private void OnDisable()
        {
            EventSystem.instance.RemoveListener<ItemTabButtonSelectedEventArgs>(OnItemTabButtonSelected);
        }
    }
}
