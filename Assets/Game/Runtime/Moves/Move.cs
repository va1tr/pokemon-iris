using System;
using System.Collections;

using UnityEngine;

using Eevee;

namespace Iris
{
    internal abstract class Move : IComparable<Move>
    {
        public readonly Combatant instigator;
        public readonly Combatant target;

        protected readonly GameBattleStateBehaviour m_StateBehaviour;

        protected const int kInstigatorHasPriority = -1;
        protected const int kInstigatorDoesNotHavePriority = 1;

        public Move(GameBattleStateBehaviour stateBehaviour, Combatant instigator, Combatant target)
        {
            m_StateBehaviour = stateBehaviour;

            this.instigator = instigator;
            this.target = target;
        }

        public abstract IEnumerator Run();

        public abstract int CompareTo(Move other);
    }
}