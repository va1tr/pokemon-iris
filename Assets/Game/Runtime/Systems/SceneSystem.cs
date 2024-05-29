using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Golem;

namespace Iris
{
    internal sealed class SceneSystem : Singleton<SceneSystem>
    {
        private static readonly HashSet<string> s_LoadedScenes = new HashSet<string>();

        internal IEnumerator LoadSceneAsync(string sceneName)
        {
            if (!s_LoadedScenes.Contains(sceneName))
            {
                s_LoadedScenes.Add(sceneName);

                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        internal IEnumerator UnloadSceneAsync(string sceneName)
        {
            if (s_LoadedScenes.Contains(sceneName))
            {
                s_LoadedScenes.Remove(sceneName);

                yield return SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }
        }
    }
}
