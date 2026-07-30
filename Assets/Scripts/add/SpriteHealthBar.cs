using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sprite-based health bar using individual heart sprites in the top-right corner.
///
/// SETUP (fixedLevel2 scene):
/// 1. Create a Canvas (Screen Space Overlay, Scale With Screen Size 960x600).
///    Name it "HealthBarCanvas".
/// 2. Add an empty child GameObject inside it. Name it "SpriteHealthBar".
///    Anchor = Top-Right, Pivot = (1,1), AnchoredPosition = (-10,-10).
/// 3. Attach THIS script to "SpriteHealthBar".
/// 4. In Inspector → Heart Sprites → set Size = 9, then drag in
///    HealthBar_0 ... HealthBar_8 from Assets/Environment/Health/HealthBar.png
/// 5. Disable or delete old Healthbar / Lives / HP text UI objects.
/// </summary>
public class SpriteHealthBar : MonoBehaviour
{
    [Header("Heart Sprites — drag HealthBar_0 through HealthBar_8 here")]
    [SerializeField] private Sprite[] heartSprites;

    [Header("Heart Display Settings")]
    [SerializeField] private float heartSize    = 52f;
    [SerializeField] private float heartSpacing = 6f;

    [Header("Colors")]
    [SerializeField] private Color fullHeartColor  = Color.white;
    [SerializeField] private Color emptyHeartColor = new Color(0.15f, 0.15f, 0.15f, 0.5f);

    // ── internals ──────────────────────────────────────────────
    private PlayerHealth customPlayerHealth;
    private Health       platformerHealth;
    private Image[]      heartImages;
    private int          lastHealth = -999;
    private int          maxHearts  = 9;

    // ── Unity lifecycle ─────────────────────────────────────────

    private void Start()
    {
        // Use Start (not Awake) so other scripts finish their Awake first.
        StartCoroutine(InitWithRetry());
    }

    private void Update()
    {
        if (!HasHealthSource()) return;

        int hp = GetCurrentHealth();
        if (hp != lastHealth)
            UpdateHearts(hp);
    }

    // ── public API ──────────────────────────────────────────────

    /// <summary>Force an immediate visual refresh.</summary>
    public void ForceRefresh()
    {
        if (!HasHealthSource()) return;
        UpdateHearts(GetCurrentHealth());
    }

    // ── private helpers ─────────────────────────────────────────

    /// <summary>
    /// Tries to find the player health source, retrying each frame for up to 5 seconds.
    /// This survives edge-cases where the Player spawns slightly after the Canvas.
    /// </summary>
    private IEnumerator InitWithRetry()
    {
        float timeout = 5f;
        float elapsed = 0f;

        while (!HasHealthSource() && elapsed < timeout)
        {
            TryBindHealthSource();

            if (!HasHealthSource())
            {
                elapsed += Time.deltaTime;
                yield return null;   // wait one frame, try again
            }
        }

        if (!HasHealthSource())
        {
            Debug.LogError("[SpriteHealthBar] Could not find a player health source after 5 seconds. " +
                           "Assign a Player prefab with PlayerHealth, or a Platformer PlayerController with Health, in fixedLevel2.");
            yield break;
        }

        maxHearts = Mathf.Max(1, GetMaxHealth());
        BuildHeartImages();
        ForceRefresh();
    }

    private void TryBindHealthSource()
    {
        customPlayerHealth = FindAnyObjectByType<PlayerHealth>();
        if (customPlayerHealth != null)
        {
            platformerHealth = null;
            return;
        }

        var playerController = FindAnyObjectByType<PlayerController>();
        if (playerController != null)
        {
            platformerHealth = playerController.health != null
                ? playerController.health
                : playerController.GetComponent<Health>();
        }
    }

    private bool HasHealthSource()
    {
        return customPlayerHealth != null || platformerHealth != null;
    }

    private int GetCurrentHealth()
    {
        if (customPlayerHealth != null && customPlayerHealth.currentHealth != null)
            return Mathf.RoundToInt(customPlayerHealth.currentHealth.Value);

        return platformerHealth != null ? platformerHealth.CurrentHP : 0;
    }

    private int GetMaxHealth()
    {
        if (customPlayerHealth != null && customPlayerHealth.startingHealth != null)
            return Mathf.RoundToInt(customPlayerHealth.startingHealth.Value);

        return platformerHealth != null ? platformerHealth.maxHP : maxHearts;
    }

    private void BuildHeartImages()
    {
        // Remove any existing children first
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        heartImages = new Image[maxHearts];

        for (int i = 0; i < maxHearts; i++)
        {
            var go = new GameObject("Heart_" + i,
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            go.transform.SetParent(transform, false);

            // Anchor left-center, grow left→right
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin        = new Vector2(0f, 0.5f);
            rt.anchorMax        = new Vector2(0f, 0.5f);
            rt.pivot            = new Vector2(0f, 0.5f);
            rt.sizeDelta        = new Vector2(heartSize, heartSize);
            rt.anchoredPosition = new Vector2(i * (heartSize + heartSpacing), 0f);

            var img = go.GetComponent<Image>();
            img.raycastTarget  = false;
            img.preserveAspect = true;
            img.color          = fullHeartColor;

            // Assign sprite
            if (heartSprites != null && i < heartSprites.Length && heartSprites[i] != null)
            {
                img.sprite = heartSprites[i];
            }
            else
            {
                Debug.LogWarning($"[SpriteHealthBar] heartSprites[{i}] not assigned. " +
                                 "Please assign all 9 HealthBar sprites in the Inspector.");
            }

            heartImages[i] = img;
        }

        // Auto-resize this panel to fit hearts
        var panelRt = GetComponent<RectTransform>();
        if (panelRt != null)
        {
            float w = maxHearts * heartSize + (maxHearts - 1) * heartSpacing;
            panelRt.sizeDelta = new Vector2(w, heartSize);
        }
    }

    private void UpdateHearts(int currentHp)
    {
        lastHealth = currentHp;
        if (heartImages == null) return;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] == null) continue;
            heartImages[i].color = (i < currentHp) ? fullHeartColor : emptyHeartColor;
        }
    }
}
