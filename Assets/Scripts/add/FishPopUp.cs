using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPopUp : MonoBehaviour
{
    private bool isCollected;
    private GameObject fish;
    private Animator anim;
    [SerializeField]private AudioSource boxOpenSound;
    // Start is called before the first frame update
    void Start()
    {
        fish = this.gameObject.transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCollected)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isCollected = !isCollected;
                StartCoroutine(PopUp());
            }
        }
    }

    IEnumerator PopUp()
    {
        boxOpenSound.Play();
        anim.SetTrigger("open_box_trigger");
        yield return new WaitForSeconds(0.5f);
        fish.transform.position = new Vector3(fish.transform.position.x, fish.transform.position.y+4f, fish.transform.position.z);
    }
}
