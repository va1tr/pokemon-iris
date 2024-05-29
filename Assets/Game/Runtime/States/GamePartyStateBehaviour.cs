using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    internal sealed class GamePartyStateBehaviour : StateBehaviour<GameMode>
    {
        [SerializeField]
        private PokemonRuntimeSet m_PokemonRuntimeSet;

        [SerializeField]
        private MoveRuntimeSet m_MoveRuntimeSet;

        [SerializeField]
        private GameObjectRuntimeSet m_GameObjectRuntimeSet;

        private PartyGraphicsInterface m_Interface;

        public override GameMode uniqueId => GameMode.Party;

        public override void Enter()
        {
            m_Interface = m_GameObjectRuntimeSet.GetComponentFromRuntimeSet<PartyGraphicsInterface>();

            m_Interface.SetPartyMenuProperties(PartyGraphicProperties.CreateProperties(m_PokemonRuntimeSet.ToArray()));
            m_Interface.PrintCompletedText(string.Concat($"Choose a POKEMON."));

            AddListeners();
        }

        private void AddListeners()
        {
            EventSystem.instance.AddListener<PartyPokemonButtonClickedEventArgs>(OnPartyPokemonButtonClicked);
            EventSystem.instance.AddListener<PartyOptionsButtonClickedEventArgs>(OnPartyOptionsButtonClicked);
        }

        private void OnPartyPokemonButtonClicked(PartyPokemonButtonClickedEventArgs args)
        {
            m_Interface.Show<PartyOptionsMenu>();

            m_Interface.PrintCompletedText(string.Concat($"What should {args.pokemon.name} do?"));
        }

        private void OnPartyOptionsButtonClicked(PartyOptionsButtonClickedEventArgs args)
        {
            switch (args.selection)
            {
                case PartySelection.Summary:
                    ProcessPokemonSummaryRequest(args);
                    break;
                case PartySelection.Switch:
                    ProcessPokemonSwitchRequest(args);
                    break;
                case PartySelection.Return:
                    HidePokemonOptionsMenuAndShowParty();
                    break;
            }
        }

        private void ProcessPokemonSummaryRequest(PartyOptionsButtonClickedEventArgs args)
        {
            Debug.Log("pokemon summary selected");
        }

        private void ProcessPokemonSwitchRequest(PartyOptionsButtonClickedEventArgs args)
        {
            var instigator = args.instigator;
            var target = args.target;

            if (instigator == target)
            {
                DenyPokemonSwitchOnTargetAlreadyInBattle(target);
            }
            else if (target.pokemon.isFainted)
            {
                DenyPokemonSwitchOnTargetFainted(target);
            }
            else
            {
                AllowPokemonSwitch(instigator, target);
            }
        }

        private void DenyPokemonSwitchOnTargetAlreadyInBattle(Combatant target)
        {
            m_Interface.PrintCompletedText(string.Concat($"{target.pokemon.name} is already in battle!"));
        }

        private void DenyPokemonSwitchOnTargetFainted(Combatant target)
        {
            m_Interface.PrintCompletedText(string.Concat($"{target.pokemon.name} is unable to battle!"));
        }

        private void AllowPokemonSwitch(Combatant instigator, Combatant target)
        {
            AddSwitchMoveToRuntimeSet(instigator, target);

            GameCoordinator.instance.ExitGameMode();
        }

        private void AddSwitchMoveToRuntimeSet<T>(T instigator, T target) where T : Combatant
        {
            m_MoveRuntimeSet.Add(new Switch(GameCoordinator.instance.battleState, instigator, target));
        }

        private void HidePokemonOptionsMenuAndShowParty()
        {
            m_Interface.Hide<PartyOptionsMenu>();
            m_Interface.Show<PartyPokemonMenu>();

            m_Interface.PrintCompletedText(string.Concat($"Choose a POKEMON."));
        }

        public override void Exit()
        {
            RemoveListeners();   
        }

        private void RemoveListeners()
        {
            EventSystem.instance.RemoveListener<PartyOptionsButtonClickedEventArgs>(OnPartyOptionsButtonClicked);
            EventSystem.instance.RemoveListener<PartyPokemonButtonClickedEventArgs>(OnPartyPokemonButtonClicked);
        }
    }
}
