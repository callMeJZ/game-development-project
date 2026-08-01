using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Changed to standard Update for direct Transform movement
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        Vector3 target = waypoints[currentWaypointIndex].transform.position;

        // Flip based on movement direction
        if (spriteRenderer != null)
        {
            float direction = target.x - transform.position.x;
            if (Mathf.Abs(direction) > 0.01f)
            {
                // Inverted this logic: 
                // Now flips when moving right (direction > 0)
                spriteRenderer.flipX = direction > 0; 
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }
}