using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class GameCoordinator : Singleton<GameCoordinator>
    {
        internal GameOverworldStateBehaviour overworldState => m_OverworldState;
        internal GameBattleStateBehaviour battleState => m_BattleState;
        internal GamePartyStateBehaviour partyState => m_PartyState;
        internal GameBagStateBehaviour bagState => m_BagState;

        private GameOverworldStateBehaviour m_OverworldState;
        private GameBattleStateBehaviour m_BattleState;
        private GamePartyStateBehaviour m_PartyState;
        private GameBagStateBehaviour m_BagState;

        private static readonly Stack<IState<GameMode>> s_States = new Stack<IState<GameMode>>();

        private readonly WaitForEndOfFrame m_WaitForEndOfFrame = new WaitForEndOfFrame();

        private static bool s_IsTransitioning = false;

        protected override void Awake()
        {
            base.Awake();

            GetStartupGameModeStates();
        }

        private void GetStartupGameModeStates()
        {
            m_OverworldState = GetComponent<GameOverworldStateBehaviour>();
            m_BattleState = GetComponent<GameBattleStateBehaviour>();
            m_PartyState = GetComponent<GamePartyStateBehaviour>();
            m_BagState = GetComponent<GameBagStateBehaviour>();
        }

        private void Start()
        {
            EnterGameMode(m_OverworldState);
        }

        internal void EnterGameMode(IState<GameMode> stateToTransitionInto)
        {
            if (!s_IsTransitioning)
            {
                StartCoroutine(TransitionGameMode(stateToTransitionInto));
            }
        }

        internal void ExitGameMode()
        {
            if (!s_IsTransitioning)
            {
                StartCoroutine(TransitionGameMode(null));
            }
        }

        private IEnumerator TransitionGameMode(IState<GameMode> stateToTransitionInto)
        {
            s_IsTransitioning = true;

            if (s_States.Count > 0)
            {
                yield return CameraFade.FadeIn();

                s_States.Peek().Exit();

                Repository.instance.SaveAllDataInternal();

                yield return SceneSystem.instance.UnloadSceneAsync(s_States.Peek().uniqueId.ToString());

                yield return m_WaitForEndOfFrame;
            }

            if (stateToTransitionInto != null)
            {
                s_States.Push(stateToTransitionInto);
            }
            else
            {
                s_States.Pop();
            }

            yield return SceneSystem.instance.LoadSceneAsync(s_States.Peek().uniqueId.ToString());

            Repository.instance.LoadAllDataInternal();

            yield return m_WaitForEndOfFrame;

            s_States.Peek().Enter();

            yield return CameraFade.FadeOut();

            s_IsTransitioning = false;
        }
    }
}
