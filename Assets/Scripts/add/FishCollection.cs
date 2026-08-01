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
    [SerializeField] private float speedBoostMultiplier = 1.8f;
    [SerializeField] private float speedBoostDuration = 5f;
    private GateController gateController;
    private PlayerMovement playerMovement;

    void Start()
    {
        gateController = FindAnyObjectByType<GateController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            itemCollectingSound.Play();
            Destroy(collision.gameObject);
            fishCollected++;
        }
        else if (collision.gameObject.CompareTag("SpeedBoostFish"))
        {
            if (itemCollectingSound != null) itemCollectingSound.Play();
            Destroy(collision.gameObject);
            if (playerMovement != null)
                playerMovement.ActivateSpeedBoost(speedBoostMultiplier, speedBoostDuration);
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