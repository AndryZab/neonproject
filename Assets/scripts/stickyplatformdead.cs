using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatformDead : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 4.3f;
    private int currentWaypointIndex = 0;
    public GameObject spikeforstraight;
    public GameObject targetformove;
    private bool hasMoved = false;
    private PlayerDeath playerdeath;
    private void Awake()
    {
        playerdeath = FindAnyObjectByType<PlayerDeath>();
    }
    private void Update()
    {
        if (playerdeath.checkforplatformreset == true)
        {
            hasMoved = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasMoved)
        {
            spikeforstraight.SetActive(true);
            StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        hasMoved = true; 

        while (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, targetformove.transform.position) >= .1f)
        {
            targetformove.transform.position = Vector2.MoveTowards(targetformove.transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            yield return null;
        }

        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
        gameObject.SetActive(false);
    }
}
