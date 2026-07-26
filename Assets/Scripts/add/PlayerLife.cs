using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private float totalLives;
    private Rigidbody2D body;
    [SerializeField]
    private FloatSo playerLives;

    [SerializeField]private GameObject gameOverText;
    [SerializeField] private AudioSource deathSound;
    [SerializeField]private AudioSource dogBark;
    
    [SerializeField]private Text playerText;
    [SerializeField]private LayerMask enemyLayer;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerText.text = "Lives: " + playerLives.Value + "/9"; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            PlayerDeath();
        }
        else if (collision.gameObject.CompareTag("Dog"))
        {
            dogBark.Play();
            PlayerDeath();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if ((collision.gameObject.transform.localPosition.y + 1.5f) > this.transform.localPosition.y)
            {
                Debug.Log("Player Y: "+ this.transform.localPosition.y + " Evil Cat Y: "+ (collision.gameObject.transform.localPosition.y + 1.5f));
                PlayerDeath();
            }
        }

    }


    public void PlayerDeath()
    {
        deathSound.Play();
        body.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death_trigger");
        playerLives.Value--;
        
        if (playerLives.Value <= 0)
        {
            //game over screen
            GameManager.instance.GameOver();
        }
    }

    public void ResetLives()
    {
        playerLives.Value = totalLives;
    }

    //reloads current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
