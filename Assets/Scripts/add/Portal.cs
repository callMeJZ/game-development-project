using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;

    public bool isPortal2;
    public float distance = 0.2f;
    [SerializeField]private AudioSource teleportationSound;
    private SpriteRenderer spriteRend1;
    private SpriteRenderer spriteRend2;

    void Start()
    {
        spriteRend1 = GameObject.FindGameObjectWithTag("Portal1").GetComponent<SpriteRenderer>();
        spriteRend2 = GameObject.FindGameObjectWithTag("Portal2").GetComponent<SpriteRenderer>();
        if (isPortal2 == false)
        {
            destination = GameObject.FindGameObjectWithTag("Portal2").GetComponent<Transform>();
        } else
        {
            destination = GameObject.FindGameObjectWithTag("Portal1").GetComponent<Transform>();
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
            teleportationSound.Play();
            StartCoroutine(Invulnerability());
        }

        
    }

    
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 11, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
        spriteRend1.color = Color.black;
        spriteRend2.color = Color.black;
        yield return new WaitForSeconds(2);
        spriteRend1.color = Color.white;
        spriteRend2.color = Color.white;
        Physics2D.IgnoreLayerCollision(9, 11, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
