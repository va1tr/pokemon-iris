using System;
using System.Collections;

using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class BattleLostState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        private BattleGraphicsInterface m_GraphicsInterface;
        private BattleCoordinator m_Coordinator;

        public BattleLostState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;
        }

        public override void Enter()
        {
            //m_Coordinator.StartCoroutine(WildPokemonBattleLostEndSequence());
        }

        //private IEnumerator WildPokemonBattleLostEndSequence()
        //{
        //    yield return m_GraphicsInterface.HideAsync<PlayerPokemonPanel>();
        //}
    }
}
