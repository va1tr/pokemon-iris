using System;
using System.Collections;

using UnityEngine;

using Golem;
using Eevee;

namespace Iris
{
    internal sealed class BattleActionState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        public BattleActionState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;
        }

        public override void Enter()
        {
            //m_StateBehaviour.GetBattleGraphicsInterface().SetActive<PlayerTrainerPanel>(false);
            //m_StateBehaviour.GetBattleGraphicsInterface().SetActive<PlayerPokemonPanel>(true);
            //m_StateBehaviour.GetBattleGraphicsInterface().SetActive<MovesMenu>(false);

            var moveRuntimeSet = m_StateBehaviour.GetMoveRuntimeSet();
            moveRuntimeSet.Sort();

            m_StateBehaviour.GetBattleGraphicsInterface().CleanupTextProcessorAndClearText();
            m_StateBehaviour.StartCoroutine(RunAllSelectedActionsInPriority());
        }

        private IEnumerator RunAllSelectedActionsInPriority()
        {
            var moveRuntimeSet = m_StateBehaviour.GetMoveRuntimeSet();

            for (int i = 0; i < moveRuntimeSet.Count(); i++)
            {
                yield return moveRuntimeSet[i].Run();

                var target = moveRuntimeSet[i].target;

                if (target.pokemon.isFainted)
                {
                    switch (target.affinity)
                    {
                        case Affinity.Hostile:
                            m_StateBehaviour.ChangeState(BattleState.Won);
                            yield break;
                        case Affinity.Friendly:
                            m_StateBehaviour.ChangeState(BattleState.Lost);
                            yield break;
                    }
                }
            }

            m_StateBehaviour.ChangeState(BattleState.Wait);
        }

        public override void Exit()
        {
            var moveRuntimeSet = m_StateBehaviour.GetMoveRuntimeSet();
            moveRuntimeSet.Clear();

            m_StateBehaviour.StopCoroutine(RunAllSelectedActionsInPriority());
            m_StateBehaviour.GetBattleGraphicsInterface().CleanupTextProcessorAndClearText();
        }
    }
}
