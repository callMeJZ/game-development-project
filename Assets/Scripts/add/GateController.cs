using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private AudioSource gateOpenSound;
    private Collider2D gateCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gateCollider = GetComponent<Collider2D>();
        gateCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenGate()
    {
        gateOpenSound.Play();
        anim.SetTrigger("open_gate_trigger");
        gateCollider.isTrigger = true;
    }
}
