using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Golem
{
    internal struct FastEnumIntEqualityComparer<T> : IEqualityComparer<T>
    {
        internal static class BoxAvoidance
        {
            private static readonly Func<T, int> _wrapper;

            static BoxAvoidance()
            {
                var paramter = Expression.Parameter(typeof(T), null);
                var convert = Expression.ConvertChecked(paramter, typeof(int));

                _wrapper = Expression.Lambda<Func<T, int>>(convert, paramter).Compile();
            }

            public static int ToInt(T value)
            {
                return _wrapper(value);
            }
        }

        public bool Equals(T x, T y)
        {
            return BoxAvoidance.ToInt(x) == BoxAvoidance.ToInt(y);
        }

        public int GetHashCode(T value)
        {
            return BoxAvoidance.ToInt(value);
        }
    }
}
