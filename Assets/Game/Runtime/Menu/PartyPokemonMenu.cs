using System;

using UnityEngine;
using UnityEngine.UI;

using Golem;
using Eevee;
using Voltorb;

namespace Iris
{
    internal sealed class PartyPokemonMenu : Menu<PartyGraphicProperties>
    {
        [SerializeField]
        private GameObjectRuntimeSet m_GameObjectRuntimeSet;

        [SerializeField]
        private Button[] m_Buttons;

        private Pokemon[] m_Pokemon;

        private const int kMaxNumberOfPartyMembers = 6;

        // Consider a way to avoid having to set the stat panel properties here.
        public override void SetProperties(PartyGraphicProperties props)
        {
            m_Pokemon = props.collection;

            for (int i = 0; i < kMaxNumberOfPartyMembers; i++)
            {
                var button = m_Buttons[i];

                if (m_Pokemon.Length > i)
                {
                    button.interactable = true;

                    var combatant = button.gameObject.GetComponent<Combatant>();

                    SetCombatantProperties(combatant, m_Pokemon[i]);

                    var statsPanel = button.gameObject.GetComponentInChildren<PlayerStatsPanel>();

                    SetStatPanelProperties(statsPanel, m_Pokemon[i]);
                }
                else
                {
                    button.interactable = false;

                    DisableButtonChildrenOnNonInteractable(button);
                }
            }
        }

        private void SetCombatantProperties(Combatant combatant, Pokemon pokemon)
        {
            combatant.affinity = Affinity.Friendly;
            combatant.pokemon = pokemon;
        }

        private void SetStatPanelProperties(PlayerStatsPanel panel, Pokemon pokemon)
        {
            var props = PokemonGraphicProperties.CreateProperties(pokemon);
            panel.SetProperties(props);
        }

        private void DisableButtonChildrenOnNonInteractable(Button button)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
        }

        // Fix this later on, so I  don't need to use lambda functions. Have a button sub-class.
        protected override void AddListeners()
        {
            m_GameObjectRuntimeSet.ClearAndTrimExcess();

            for (int i = 0; i < kMaxNumberOfPartyMembers; i++)
            {
                var button = m_Buttons[i];

                if (button.interactable)
                {
                    var combatant = button.GetComponent<Combatant>();

                    button.onClick.AddListener(() =>
                    {
                        m_GameObjectRuntimeSet.Add(combatant.gameObject);

                        var args = PartyPokemonButtonClickedEventArgs.CreateEventArgs(combatant.pokemon);
                        EventSystem.instance.Invoke(args);
                    });
                }
            }
        }

        protected override void RemoveListeners()
        {
            for (int i = 0; i < kMaxNumberOfPartyMembers; i++)
            {
                var button = m_Buttons[i];

                if (button.interactable)
                {
                    button.onClick.RemoveAllListeners();
                }
            }
        }
    }
}
