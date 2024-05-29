using System;

using UnityEngine;
using UnityEngine.Events;

namespace Voltorb
{
    [Serializable]
    public abstract class TabButtonUnityEvent<T> : UnityEvent<T>
    {
        
    }
}
