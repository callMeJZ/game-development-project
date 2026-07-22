using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine.SceneManagement;

namespace Platformer.Gameplay
{

    /// <summary>
    /// This event is triggered when the player character enters a trigger with a VictoryZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
    public class PlayerEnteredVictoryZone : Simulation.Event<PlayerEnteredVictoryZone>
    {
        public VictoryZone victoryZone;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            if (model.levelComplete)
                return;

            model.levelComplete = true;
            model.AddScore(model.levelCompleteScore);
            RunState.SetScore(model.score);
            model.player.animator.SetTrigger("victory");
            model.player.controlEnabled = false;

            var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                var loadScene = Simulation.Schedule<LoadSceneAfterVictory>(1.5f);
                loadScene.sceneBuildIndex = nextSceneIndex;
            }
        }
    }
}
