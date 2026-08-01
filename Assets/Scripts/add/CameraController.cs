using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;


    void Awake()
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
    private void Update()
    {
        if (player == null)
        {
            return;
        }

        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    public void StopFollowing(Transform target)
    {
        if (player == target)
        {
            player = null;
            enabled = false;
        }
    }
}
