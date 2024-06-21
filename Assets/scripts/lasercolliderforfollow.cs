using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasercolliderforfollow : MonoBehaviour
{
    public scriptforrotatefollowtarget followlaser;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            followlaser.allowforrotate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            followlaser.allowforrotate = false;
        }
    }
}
