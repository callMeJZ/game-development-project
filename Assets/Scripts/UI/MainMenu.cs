using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Tooltip("Exact name of the Level 1 scene, as it appears in Build Settings.")]
        [SerializeField] private string level1SceneName = "Level1_Village";

        public void StartGame()
        {
            SceneManager.LoadScene(level1SceneName);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}