using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasserbuttonray : MonoBehaviour
{
    public BoxCollider2D lasercontroloff;
    public GameObject buttonforOFF;
    public GameObject weaponON;
    public GameObject weaponOFF;
    public GameObject obj;
    public controllerlaser controllslaser;

    private void LateUpdate()
    {
        if (!buttonforOFF.activeSelf)
        {
            lasercontroloff.enabled = false;
            weaponON.SetActive(false);
            weaponOFF.SetActive(true);
            if (obj != null)
            {
                obj.SetActive(true);
            }
            controllslaser.enabled = false;
        }
    }
}
