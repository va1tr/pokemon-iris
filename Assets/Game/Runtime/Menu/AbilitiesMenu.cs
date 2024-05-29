using UnityEngine;
using UnityEngine.UI;

using Golem;
using Eevee;
using Voltorb;

namespace Iris
{
    internal sealed class AbilitiesMenu : Menu<PokemonGraphicProperties>
    {
        [SerializeField]
        private Button[] m_AbilityButtons;

        private AbilitySpec[] m_Abilities;

        private const uint kMaxNumberOfAbilities = 4;

        private const string kNoValidAbility = "-";

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Abilities = props.pokemon.GetAllAbilities();

            for (int i = 0; i < kMaxNumberOfAbilities; i++)
            {
                if (m_Abilities[i] != null)
                {
                    m_AbilityButtons[i].GetComponentInChildren<Text>().text = m_Abilities[i].asset.name.ToString().ToUpper();
                    m_AbilityButtons[i].interactable = true;
                }
                else
                {
                    m_AbilityButtons[i].GetComponentInChildren<Text>().text = kNoValidAbility;
                    m_AbilityButtons[i].interactable = false;
                }
            }
        }

        protected override void AddListeners()
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_AbilityButtons[0].gameObject);

            m_AbilityButtons[0].onClick.AddListener(OnAbilityButtonOneClicked);
            m_AbilityButtons[1].onClick.AddListener(OnAbilityButtonTwoClicked);
            m_AbilityButtons[2].onClick.AddListener(OnAbilityButtonThreeClicked);
            m_AbilityButtons[3].onClick.AddListener(OnAbilityButtonFourClicked);
        }

        private void OnAbilityButtonOneClicked()
        {
            var args = AbilityButtonClickedEventArgs.CreateEventArgs(m_Abilities[0]);
            EventSystem.instance.Invoke(args);
        }

        private void OnAbilityButtonTwoClicked()
        {
            var args = AbilityButtonClickedEventArgs.CreateEventArgs(m_Abilities[1]);
            EventSystem.instance.Invoke(args);
        }

        private void OnAbilityButtonThreeClicked()
        {
            var args = AbilityButtonClickedEventArgs.CreateEventArgs(m_Abilities[2]);
            EventSystem.instance.Invoke(args);
        }

        private void OnAbilityButtonFourClicked()
        {
            var args = AbilityButtonClickedEventArgs.CreateEventArgs(m_Abilities[3]);
            EventSystem.instance.Invoke(args);
        }

        protected override void RemoveListeners()
        {
            m_AbilityButtons[0].onClick.RemoveListener(OnAbilityButtonOneClicked);
            m_AbilityButtons[1].onClick.RemoveListener(OnAbilityButtonTwoClicked);
            m_AbilityButtons[2].onClick.RemoveListener(OnAbilityButtonThreeClicked);
            m_AbilityButtons[3].onClick.RemoveListener(OnAbilityButtonFourClicked);
        }

    }
}
