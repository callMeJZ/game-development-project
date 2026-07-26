using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLogic : MonoBehaviour
{
    private bool isBarking;
    [SerializeField]private AudioSource dogsound;
    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (Vector3.Distance (transform.position, player.transform.position) < 10)
        {
            if (isBarking == false)
            {
                StartCoroutine(DogBarking());
                isBarking = true;
            }
        }
    }

    private IEnumerator DogBarking()
    {
        //Play Bark Sound;
        dogsound.Play();
        yield return new WaitForSeconds(3);
        isBarking = !isBarking;
    }
}
