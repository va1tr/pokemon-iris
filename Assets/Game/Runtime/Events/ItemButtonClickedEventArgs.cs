using System;

using Arbok;

namespace Iris
{
    internal sealed class ItemButtonClickedEventArgs : EventArgs
    {
        internal readonly ItemSpec item;

        internal ItemButtonClickedEventArgs(ItemSpec item)
        {
            this.item = item;
        }

        internal static ItemButtonClickedEventArgs CreateEventArgs(ItemSpec item)
        {
            return new ItemButtonClickedEventArgs(item);
        }
    }

}
