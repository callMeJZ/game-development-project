using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        GUIStyle hudStyle;
        GUIStyle completionStyle;

        void OnEnable()
        {
            Instance = this;

            if (SceneManager.GetActiveScene().buildIndex == 0)
                RunState.StartNewRun();

            model.score = RunState.Score;
            model.levelComplete = false;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }

        void OnGUI()
        {
            if (Event.current.type != EventType.Repaint)
                return;

            if (hudStyle == null)
            {
                hudStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 24,
                    fontStyle = FontStyle.Bold
                };
                hudStyle.normal.textColor = Color.white;
            }

            var health = model.player != null ? model.player.health : null;
            var hpText = health != null ? $"HP: {health.CurrentHP}/{health.maxHP}" : "HP: -";
            var level = SceneManager.GetActiveScene().buildIndex + 1;
            GUI.Label(new Rect(20, 20, 260, 36), hpText, hudStyle);
            GUI.Label(new Rect(20, 52, 260, 36), $"Score: {model.score}", hudStyle);
            GUI.Label(new Rect(20, 84, 260, 36), $"Level: {level}", hudStyle);

            if (!model.levelComplete || SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
                return;

            if (completionStyle == null)
            {
                completionStyle = new GUIStyle(hudStyle)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 34
                };
            }
            GUI.Label(new Rect(0, Screen.height * 0.4f, Screen.width, 50), "PROTOTYPE COMPLETE", completionStyle);
            GUI.Label(new Rect(0, Screen.height * 0.4f + 45, Screen.width, 40), $"Final score: {model.score}", completionStyle);
        }
    }
}
