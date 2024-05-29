using UnityEngine;
using UnityEngine.EventSystems;

namespace Voltorb
{
    internal sealed class KeyboardOnlyInputModule : StandaloneInputModule
	{
        public override void Process()
        {
            if (!eventSystem.isFocused && ShouldIgnoreEventsOnNoFocus())
            {
                return;
            }

            bool usedEvent = SendUpdateEventToSelectedObject();

            if (eventSystem.sendNavigationEvents)
            {
                if (!usedEvent)
                {
                    usedEvent |= SendMoveEventToSelectedObject();
                }

                if (!usedEvent)
                {
                    SendSubmitEventToSelectedObject();
                }
            }
        }

        private bool ShouldIgnoreEventsOnNoFocus()
        {
            switch (SystemInfo.operatingSystemFamily)
            {
                case OperatingSystemFamily.Windows:
                case OperatingSystemFamily.Linux:
                case OperatingSystemFamily.MacOSX:
#if UNITY_EDITOR
                    if (UnityEditor.EditorApplication.isRemoteConnected)
                        return false;
#endif
                    return true;
                default:
                    return false;
            }
        }
    }
}
