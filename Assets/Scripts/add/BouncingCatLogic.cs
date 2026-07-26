using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingCatLogic : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private Rigidbody2D player_body;
    [SerializeField]private AudioSource catMeow;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        player_body = player.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((collision.gameObject.transform.position.y) > this.gameObject.transform.position.y + 1.5f)
            {
                player_body.linearVelocity = new Vector2(player_body.linearVelocity.x, 50f);
                catMeow.Play();
            }

                
        }
    }
}
