using UnityEngine;
using UnityEngine.UI;


using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class MovesMenu : Menu
    {
        [SerializeField]
        private Button m_FightButton;

        [SerializeField]
        private Button m_BagButton;

        [SerializeField]
        private Button m_PokemonButton;

        [SerializeField]
        private Button m_RunButton;

        protected override void AddListeners()
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_FightButton.gameObject);

            m_FightButton.onClick.AddListener(OnFightButtonClicked);
            m_BagButton.onClick.AddListener(OnBagButtonClicked);
            m_PokemonButton.onClick.AddListener(OnPokemonButtonClicked);
            m_RunButton.onClick.AddListener(OnRunButtonClicked);
        }

        private void OnFightButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.Fight);
            EventSystem.instance.Invoke(args);
        }

        private void OnBagButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.Bag);
            EventSystem.instance.Invoke(args);
        }

        private void OnPokemonButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.Pokemon);
            EventSystem.instance.Invoke(args);
        }

        private void OnRunButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.Run);
            EventSystem.instance.Invoke(args);
        }

        protected override void RemoveListeners()
        {
            m_FightButton.onClick.RemoveListener(OnFightButtonClicked);
            m_BagButton.onClick.RemoveListener(OnBagButtonClicked);
            m_PokemonButton.onClick.RemoveListener(OnPokemonButtonClicked);
            m_RunButton.onClick.RemoveListener(OnRunButtonClicked);
        }
    }
}