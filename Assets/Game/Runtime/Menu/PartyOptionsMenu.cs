using UnityEngine;
using UnityEngine.UI;

using Golem;
using Eevee;
using Voltorb;

namespace Iris
{
    internal sealed class PartyOptionsMenu : Menu
    {
        [SerializeField]
        private GameObjectRuntimeSet m_GameObjectRuntimeSet;

        [SerializeField]
        private Button m_SummaryButton;

        [SerializeField]
        private Button m_SwitchButton;

        [SerializeField]
        private Button m_ReturnButton;

        [SerializeField]
        private Combatant[] m_Combatants;

        private Combatant m_ActiveCombatant;

        protected override void AddListeners()
        {
            m_SummaryButton.onClick.AddListener(OnSummaryButtonClicked);
            m_SwitchButton.onClick.AddListener(OnSwitchButtonClicked);
            m_ReturnButton.onClick.AddListener(OnReturnButtonClicked);
        }

        private void OnSummaryButtonClicked()
        {
            var selected = m_GameObjectRuntimeSet.GetComponentFromRuntimeSet<Combatant>();

            var args = PartyOptionsButtonClickedEventArgs.CreateEventArgs(PartySelection.Summary, selected, null);
            EventSystem.instance.Invoke(args);
        }

        private void OnSwitchButtonClicked()
        {
            var active = GetActiveCombatant();
            var selected = m_GameObjectRuntimeSet.GetComponentFromRuntimeSet<Combatant>();

            var args = PartyOptionsButtonClickedEventArgs.CreateEventArgs(PartySelection.Switch, active, selected);
            EventSystem.instance.Invoke(args);
        }

        private void OnReturnButtonClicked()
        {
            m_GameObjectRuntimeSet.ClearAndTrimExcess();

            var args = PartyOptionsButtonClickedEventArgs.CreateEventArgs(PartySelection.Return, null, null);
            EventSystem.instance.Invoke(args);
        }

        private Combatant GetActiveCombatant()
        {
            if (m_ActiveCombatant == null)
            {
                foreach (var combatant in m_Combatants)
                {
                    if (combatant.gameObject.activeSelf && combatant.pokemon.activeSelf)
                    {
                        m_ActiveCombatant = combatant;
                        break;
                    }
                }
            }

            return m_ActiveCombatant;
        }

        protected override void RemoveListeners()
        {
            m_SummaryButton.onClick.RemoveListener(OnSummaryButtonClicked);
            m_SwitchButton.onClick.RemoveListener(OnSwitchButtonClicked);
            m_ReturnButton.onClick.RemoveListener(OnReturnButtonClicked);
        }
    }
}
