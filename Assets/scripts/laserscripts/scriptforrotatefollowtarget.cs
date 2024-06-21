using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scriptforrotatefollowtarget : MonoBehaviour
{
    private Player player;
    public bool allowforrotate = false;
    public float rotationSpeed = 3f;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    private void LateUpdate()
    {
        if (player != null && allowforrotate == true)
        {

            Vector3 direction = player.transform.position - transform.position;


            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);


            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
