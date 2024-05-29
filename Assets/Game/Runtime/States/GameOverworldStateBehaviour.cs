using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class GameOverworldStateBehaviour : StateBehaviour<GameMode>
    {
        public override GameMode uniqueId => GameMode.Overworld;

        public override void Enter()
        {
            StartCoroutine(DelayBeforeDebugTransition());
        }

        private System.Collections.IEnumerator DelayBeforeDebugTransition()
        {
            yield return new WaitForSeconds(1);

            GameCoordinator.instance.EnterGameMode(GameCoordinator.instance.battleState);
        }
    }
}
