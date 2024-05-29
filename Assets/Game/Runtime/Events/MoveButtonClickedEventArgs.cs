using System;

namespace Iris
{
    internal sealed class MoveButtonClickedEventArgs : EventArgs
    {
        internal readonly MoveSelection selection;

        internal MoveButtonClickedEventArgs(MoveSelection selection)
        {
            this.selection = selection;
        }

        internal static MoveButtonClickedEventArgs CreateEventArgs(MoveSelection selection)
        {
            return new MoveButtonClickedEventArgs(selection);
        }
    }
} 