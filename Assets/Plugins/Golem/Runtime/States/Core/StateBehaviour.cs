using System;

using UnityEngine;

namespace Golem
{
    public abstract class StateBehaviour<T> : MonoBehaviour, IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        public virtual T uniqueId { get; }

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
