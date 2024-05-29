using System;

namespace Golem
{
    public interface IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        T uniqueId { get; }

        void Enter();
        void Update();
        void Exit();
    }
}
