using System.Collections;
using System.Collections.Generic;
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RottenFish"))
        {
            Take1Damage();
            Destroy(collision.gameObject);
        }
    }
    public void Take1Damage()
    {
        currentHealth.Value -= 1;

        if (currentHealth.Value > 0)
        {
            //player hurt
            if (livesLostText != null)
            {
                livesLostText.text = "-1";
                StartCoroutine(DisplayTextFor2Seconds());
            }
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            hurtSound.Play();
        }
        else
        {
            //player dead
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
        currentHealth.Value -= 2;

        if (currentHealth.Value > 0)
        {
            //player hurt
            if (livesLostText != null)
            {
                livesLostText.text = "-2";
                StartCoroutine(DisplayTextFor2Seconds());
            }
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            hurtSound.Play();
        }
        else
        {
            //player dead
            PlayerDeath();
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Take1Damage();
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if ((collision.gameObject.transform.position.y + 1.5f) > this.transform.position.y)
            {
                if (isImmune==false){
                Take2Damage();}
            }
        }
        else if (collision.gameObject.CompareTag("Dog"))
        {
            if (collision.gameObject.transform.position.y + 1.5f > this.transform.position.y)
            {
                if (isImmune==false){
                dogBark.Play();
                Take2Damage();}
            }

            else
            {
                playerMovement.doubleJump = true;
                if (dogHowl != null)
                {
                    dogHowl.Play();
                }
                if (IsJumpHeld())
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, 22f);
                }
                else
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, 14f);
                }
                
            }
        }

    }


    public void PlayerDeath()
    {
        deathSound.Play();
        coll.enabled = false;
        body.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death_trigger");
    }

    public void ResetLives()
    {
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
        Invoke("_RestartLevel", 2);
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

}
