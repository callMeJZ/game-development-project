using Platformer.Core;
using UnityEngine.SceneManagement;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Loads a configured build scene after the victory animation has had time to play.
    /// </summary>
    public class LoadSceneAfterVictory : Simulation.Event<LoadSceneAfterVictory>
    {
        public int sceneBuildIndex;

        public override void Execute()
        {
            if (sceneBuildIndex < 0 || sceneBuildIndex >= SceneManager.sceneCountInBuildSettings)
                return;

            Simulation.Clear();
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
