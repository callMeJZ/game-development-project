using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]private AudioSource hurtSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField]private AudioSource dogBark;
    [SerializeField]private AudioSource dogHowl;
    [SerializeField]public FloatSo startingHealth;
    [SerializeField]private TextMeshProUGUI livesLostText;
    private Animator anim;
    private Rigidbody2D body;
    private BoxCollider2D coll;
    [SerializeField]public FloatSo currentHealth;
    private bool isImmune = false;
    private bool isDead = false;
    private float nextDamageTime = 0f;

    private PlayerMovement playerMovement;

   [Header("iFrames")]
    [SerializeField]private float iFramesDuration;
    [SerializeField]private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        ResetLives();
    }

    private void Update()
    {
        if (livesLostText != null && spriteRend != null)
        {
            if (spriteRend.flipX)
            {
                livesLostText.alignment = TextAlignmentOptions.Right;
            } 
            else
            {
                livesLostText.alignment = TextAlignmentOptions.Left;
            }
        }

        CheckDogOverlap();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTagged(collision.gameObject, "RottenFish"))
        {
            Take1Damage();
            Destroy(collision.gameObject);
        }
        else if (IsDog(collision.gameObject))
        {
            HurtFromDog();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsDog(collision.gameObject))
        {
            HurtFromDog();
        }
    }
    public void Take1Damage()
    {
        if (isDead || currentHealth == null || Time.time < nextDamageTime)
        {
            return;
        }

        nextDamageTime = Time.time + 1f;
        currentHealth.Value -= 1;

        if (currentHealth.Value > 0)
        {
            if (livesLostText != null)
            {
                livesLostText.text = "-1";
                StartCoroutine(DisplayTextFor2Seconds());
            }

            TriggerHurtAnimation();
            StartCoroutine(Invulnerability());
            PlayAudio(hurtSound);
        }
        else
        {
            PlayerDeath();
        }
    }
    private IEnumerator DisplayTextFor2Seconds()
    {
        if (livesLostText == null) yield break;
        livesLostText.enabled = true;
        yield return new WaitForSeconds(2);
        livesLostText.enabled = false;
    }
    public void Take2Damage()
    {
        if (isDead || currentHealth == null || Time.time < nextDamageTime)
        {
            return;
        }

        nextDamageTime = Time.time + 1f;
        currentHealth.Value -= 2;

        if (currentHealth.Value > 0)
        {
            if (livesLostText != null)
            {
                livesLostText.text = "-2";
                StartCoroutine(DisplayTextFor2Seconds());
            }

            TriggerHurtAnimation();
            StartCoroutine(Invulnerability());
            PlayAudio(hurtSound);
        }
        else
        {
            PlayerDeath();
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsTagged(collision.gameObject, "Trap"))
        {
            Take1Damage();
        }

        else if (IsTagged(collision.gameObject, "Enemy"))
        {
            if ((collision.gameObject.transform.position.y + 1.5f) > this.transform.position.y)
            {
                if (!isImmune)
                {
                    Take2Damage();
                }
            }
        }
        else if (IsDog(collision.gameObject))
        {
            HurtFromDog();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsDog(collision.gameObject))
        {
            HurtFromDog();
        }
    }

    private void HurtFromDog()
    {
        if (isImmune || isDead || currentHealth == null || currentHealth.Value <= 0 || Time.time < nextDamageTime)
        {
            return;
        }

        PlayAudio(dogBark);
        Take1Damage();
    }

    private void CheckDogOverlap()
    {
        if (coll == null || isImmune || isDead)
        {
            return;
        }

        Bounds bounds = coll.bounds;
        bounds.Expand(new Vector3(0.5f, 0.5f, 0f));

        Collider2D[] hits = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
        foreach (Collider2D hit in hits)
        {
            if (hit != null && hit.gameObject != gameObject && IsDog(hit.gameObject))
            {
                HurtFromDog();
                return;
            }
        }
    }

    private bool IsTagged(GameObject target, string tagName)
    {
        return target != null && target.tag == tagName;
    }

    private bool IsDog(GameObject target)
    {
        if (target == null)
        {
            return false;
        }

        return IsTagged(target, "Dog") || target.name.Contains("Dog");
    }

    private void TriggerHurtAnimation()
    {
        if (anim == null)
        {
            return;
        }

        foreach (var parameter in anim.parameters)
        {
            if (parameter.name == "hurt" && parameter.type == AnimatorControllerParameterType.Trigger)
            {
                anim.SetTrigger("hurt");
                return;
            }
        }
    }

    private void PlayAudio(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void PlayerDeath()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        PlayAudio(deathSound);
        StopCameraFollow();

        if (coll != null)
        {
            coll.enabled = false;
        }

        if (body != null)
        {
            body.bodyType = RigidbodyType2D.Static;
        }

        TriggerDeathAnimation();
        Invoke(nameof(ReloadCurrentScene), 2f);
    }

    public void ResetLives()
    {
        isDead = false;
        nextDamageTime = 0f;
        currentHealth.Value = startingHealth.Value;
    }

    public void Add3Lives()
    {
        currentHealth.Value += 3;
        if (currentHealth.Value > startingHealth.Value)
        {
            currentHealth.Value = 9;
        }
    }

    private bool IsJumpHeld()
    {
        Keyboard keyboard = Keyboard.current;
        bool keyboardJump = keyboard != null &&
            (keyboard.spaceKey.isPressed || keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed);

        Gamepad gamepad = Gamepad.current;
        return keyboardJump || (gamepad != null && gamepad.buttonSouth.isPressed);
    }

    public void _RestartLevel()
    {
        //Call GameOverscene
        GameManager.instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
        ResetLives();
        SceneManager.LoadScene(10); 
    }

    //reloads current level
    public void RestartLevel()
    {
        Invoke(nameof(ReloadCurrentScene), 2);
    }

    private void ReloadCurrentScene()
    {
        ResetLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Invulnerability()
    {
        isImmune=true;
        Physics2D.IgnoreLayerCollision(9, 12, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {  
            spriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 12, false);
        isImmune=false;
    }

    private void StopCameraFollow()
    {
        var model = Simulation.GetModel<PlatformerModel>();
        if (model.virtualCamera != null)
        {
            if (model.virtualCamera.Follow == transform)
            {
                model.virtualCamera.Follow = null;
            }

            if (model.virtualCamera.LookAt == transform)
            {
                model.virtualCamera.LookAt = null;
            }
        }

        var cameraControllers = FindObjectsByType<CameraController>(FindObjectsSortMode.None);
        foreach (var cameraController in cameraControllers)
        {
            cameraController.StopFollowing(transform);
        }
    }

    private void TriggerDeathAnimation()
    {
        if (anim == null)
        {
            return;
        }

        foreach (var parameter in anim.parameters)
        {
            if (parameter.name == "death_trigger" && parameter.type == AnimatorControllerParameterType.Trigger)
            {
                anim.SetTrigger("death_trigger");
                return;
            }
        }

        foreach (var parameter in anim.parameters)
        {
            if (parameter.name == "dead" && parameter.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool("dead", true);
                return;
            }
        }
    }

}
