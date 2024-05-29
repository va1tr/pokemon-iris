using UnityEngine;

namespace Voltorb
{
    internal sealed class TabLayoutNavigation : MonoBehaviour
    {
        [SerializeField]
        private TabButton[] m_TabButtons;

        private TabButton m_SelectedTabButton;

        private int m_Index;

        private const string kHorizontalAxis = "Horizontal";

        private void Awake()
        {
            SetSelectedTabButtonToFirstInArray();
        }

        private void SetSelectedTabButtonToFirstInArray()
        {
            Debug.Log("ran");
            m_SelectedTabButton = m_TabButtons[0];
            m_SelectedTabButton.Select();
        }

        private void Update()
        {
            CaptureInputAndSwitchTabsIfButtonIsPressed();
        }

        private void CaptureInputAndSwitchTabsIfButtonIsPressed()
        {
            var input = new Vector2(Input.GetAxisRaw(kHorizontalAxis), 0f);

            if (input.x != 0)
            {
                int index = Mathf.Clamp(Mathf.RoundToInt(m_Index + input.x), 0, m_TabButtons.Length - 1);

                if (m_Index != index)
                {
                    m_Index = index;
                    SwitchBetweenTabs();
                }
            }
        }

        private void SwitchBetweenTabs()
        {
            m_SelectedTabButton.Deselect();
            m_SelectedTabButton = m_TabButtons[m_Index];
            m_SelectedTabButton.Select();
        }
    }
}
