using System;
using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    internal sealed class BattleWaitState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        private IEnumerator m_BouncePokemonAndStatsPanelAsync;

        public BattleWaitState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;

            m_BouncePokemonAndStatsPanelAsync = BouncePokemonAndStatsPanelWhileWaiting();
        }

        public override void Enter()
        {
            m_StateBehaviour.GetBattleGraphicsInterface().SetActive<PlayerTrainerPanel>(false);
            m_StateBehaviour.GetBattleGraphicsInterface().SetActive<PlayerPokemonPanel>(true);

            EventSystem.instance.AddListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.AddListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);

            PrintPokemonNameAndShowMovesMenu();

            m_StateBehaviour.StartCoroutine(m_BouncePokemonAndStatsPanelAsync);
        }

        private void PrintPokemonNameAndShowMovesMenu()
        {
            m_StateBehaviour.GetBattleCoordinator().GetPlayerActivePokemon(out var combatant);

            string message = string.Concat($"What will {combatant.name} do?");

            m_StateBehaviour.GetBattleGraphicsInterface().PrintCompletedText(message);
            m_StateBehaviour.GetBattleGraphicsInterface().Show<MovesMenu>();
        }

        private IEnumerator BouncePokemonAndStatsPanelWhileWaiting()
        {
            while (true)
            {
                yield return m_StateBehaviour.GetBattleGraphicsInterface().BouncePokemonAndStatsPanelWhileWaiting();
            }
        }

        private void OnMoveButtonClicked(MoveButtonClickedEventArgs args)
        {
            var selection = args.selection;

            switch (selection)
            {
                case MoveSelection.Fight:
                    HideMovesMenuAndShowAbilitiesMenu();
                    break;
                case MoveSelection.Pokemon:
                    TransitionIntoMenuGameMode();
                    break;
                case MoveSelection.Bag:
                    TransitionIntoBagGameMode();
                    break;
                default:
                    break;
            }
        }

        private void HideMovesMenuAndShowAbilitiesMenu()
        {
            m_StateBehaviour.GetBattleGraphicsInterface().Hide<MovesMenu>();
            m_StateBehaviour.GetBattleGraphicsInterface().Show<AbilitiesMenu>();
        }

        private void TransitionIntoMenuGameMode()
        {
            Exit();

            GameCoordinator.instance.EnterGameMode(GameCoordinator.instance.partyState);
        }

        private void TransitionIntoBagGameMode()
        {
            Exit();

            GameCoordinator.instance.EnterGameMode(GameCoordinator.instance.bagState);
        }

        private void OnAbilityButtonClicked(AbilityButtonClickedEventArgs args)
        {
            m_StateBehaviour.AddFightMoveToRuntimeSet(args.abilitySpec);
            m_StateBehaviour.ChangeState(BattleState.Action);
        }

        public override void Exit()
        {
            m_StateBehaviour.StopCoroutine(m_BouncePokemonAndStatsPanelAsync);

            ClearTextAndHideAbilitiesMenu();

            EventSystem.instance.RemoveListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.RemoveListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);
        }

        private void ClearTextAndHideAbilitiesMenu()
        {
            m_StateBehaviour.GetBattleGraphicsInterface().CleanupTextProcessorAndClearText();

            m_StateBehaviour.GetBattleGraphicsInterface().Hide<AbilitiesMenu>();
            m_StateBehaviour.GetBattleGraphicsInterface().Hide<MovesMenu>();
        }
    }
}