using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    internal sealed class GameBattleStateBehaviour : StateBehaviour<GameMode>
    {
        [SerializeField]
        private MoveRuntimeSet m_MoveRuntimeSet;

        [SerializeField]
        private GameObjectRuntimeSet m_GameObjectRuntimeSet;

        private BattleCoordinator m_Coordinator;
        private BattleGraphicsInterface m_Interface;

        private static readonly StateMachine<BattleState> s_StateMachine = new StateMachine<BattleState>();

        public override GameMode uniqueId => GameMode.Battle;

        private void Awake()
        {
            CreateBattleStates();
        }

        private void CreateBattleStates()
        {
            var states = new IState<BattleState>[]
            {
                new BattleBeginState<BattleState>(BattleState.Begin, this),
                new BattleWaitState<BattleState>(BattleState.Wait, this),
                new BattleActionState<BattleState>(BattleState.Action, this),
                new BattleWonState<BattleState>(BattleState.Won, this),
                new BattleLostState<BattleState>(BattleState.Lost, this)
            };

            s_StateMachine.AddStatesToStateMachine(states);
            s_StateMachine.SetCurrentStateID(BattleState.Begin);
        }

        public override void Enter()
        {
            m_Coordinator = GetBattleCoordinator();
            m_Interface = GetBattleGraphicsInterface();

            m_Interface.CleanupTextProcessorAndClearText();

            m_Coordinator.SetEnemyActivePokemon();
            m_Coordinator.SetPlayerActivePokemon();

            m_Coordinator.SetEnemyStatPanelProperties();
            m_Coordinator.SetPlayerStatPanelAndAbilityMenuProperties();

            StartBattleStateMachine();
        }

        private void StartBattleStateMachine()
        {
            // Find a better way of checking for switch requests.
            if (m_MoveRuntimeSet.Count() > 0)
            {
                RandomEnemyAttack();
                ChangeState(BattleState.Action);
            }
            else
            {
                s_StateMachine.Start();
            }
        }

        internal void ChangeState(BattleState stateToTransitionInto)
        {
            s_StateMachine.ChangeState(stateToTransitionInto);
        }

        internal BattleCoordinator GetBattleCoordinator()
        {
            if (m_Coordinator == null)
            {
                m_Coordinator = m_GameObjectRuntimeSet.GetComponentFromRuntimeSet<BattleCoordinator>();
            }

            return m_Coordinator;
        }

        internal BattleGraphicsInterface GetBattleGraphicsInterface()
        {
            if (m_Interface == null)
            {
                m_Interface = m_GameObjectRuntimeSet.GetComponentFromRuntimeSet<BattleGraphicsInterface>();
            }

            return m_Interface;
        }

        internal void AddFightMoveToRuntimeSet<T>(T item) where T : AbilitySpec
        {
            GetBattleCoordinator().GetPlayerCombatant(out Combatant player);
            GetBattleCoordinator().GetEnemyCombatant(out Combatant enemy);

            m_MoveRuntimeSet.Add(new Fight(this, player, enemy, item));

            // Enemy Attack
            m_MoveRuntimeSet.Add(new Fight(this, enemy, player, enemy.pokemon.GetAllAbilities()[0]));
        }

        private void RandomEnemyAttack()
        {
            GetBattleCoordinator().GetPlayerCombatant(out Combatant player);
            GetBattleCoordinator().GetEnemyCombatant(out Combatant enemy);

            m_MoveRuntimeSet.Add(new Fight(this, enemy, player, enemy.pokemon.GetAllAbilities()[0]));
        }

        internal MoveRuntimeSet GetMoveRuntimeSet()
        {
            return m_MoveRuntimeSet;
        }

    }
}
