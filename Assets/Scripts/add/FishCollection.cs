using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCollection : MonoBehaviour
{
    public float fishCollected = 0;
    bool isOpen = false;

    [SerializeField]public float totalFish = 1;
    [SerializeField] private AudioSource itemCollectingSound;
    private GateController gateController;

  

    void Start()
    {
        gateController = FindObjectOfType<GateController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            itemCollectingSound.Play();
            Destroy(collision.gameObject);
            fishCollected++;
        }    
    }

    private void Update()
    {
        if (fishCollected == totalFish && !isOpen)
        {
            gateController.OpenGate();
            isOpen = true; 
        }
    }
    
    
}