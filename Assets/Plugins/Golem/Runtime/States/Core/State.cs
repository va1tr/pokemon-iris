using System;

namespace Golem
{
    public abstract class State<T> : IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        public T uniqueId { get; }

        public State(T uniqueID)
        {
            this.uniqueId = uniqueID;
        }

        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
