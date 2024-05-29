using System;

using Arbok;

namespace Iris
{
    internal sealed class ItemTabButtonSelectedEventArgs : EventArgs
    {
        internal readonly ItemSpec[] items;

        internal ItemTabButtonSelectedEventArgs(ItemSpec[] items)
        {
            this.items = items;
        }

        internal static ItemTabButtonSelectedEventArgs CreateEventArgs(ItemSpec[] items)
        {
            return new ItemTabButtonSelectedEventArgs(items);
        }
    }

}
