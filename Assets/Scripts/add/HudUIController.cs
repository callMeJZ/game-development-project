using Platformer.Mechanics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Drives the Score and Level UI text elements from the GameController model.
/// Attach to any GameObject and assign the two TMP_Text references in the Inspector.
/// HP is handled separately by SpriteHealthBar (heart icons).
/// </summary>
public class HudUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;

    void Update()
    {
        if (GameController.Instance == null) return;

        var model = GameController.Instance.model;

        // Score
        scoreText.text = $"Score: {model.score}";

        // Level
        levelText.text = $"Level: {SceneManager.GetActiveScene().buildIndex}";
    }
}
