using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunDistanceSound : MonoBehaviour
{
    [System.Serializable]
    public class LaserGunData
    {
        public AudioSource sourceLaserGun;
        public GameObject laserGunObject;
        public float maxDistance;
    }

    public List<LaserGunData> laserGunsData;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player object not found in the scene.");
        }

        foreach (LaserGunData gunData in laserGunsData)
        {
            if (gunData.sourceLaserGun == null || gunData.laserGunObject == null)
            {
                Debug.LogError("One or more LaserGunData elements are missing AudioSource or GameObject references.");
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            foreach (LaserGunData gunData in laserGunsData)
            {
                AdjustVolumeBasedOnDistance(gunData);
            }
        }
    }

    private void AdjustVolumeBasedOnDistance(LaserGunData gunData)
    {
        float distance = Vector2.Distance(player.transform.position, gunData.laserGunObject.transform.position);
        float volume = 1 - Mathf.Clamp01(distance / gunData.maxDistance);
        gunData.sourceLaserGun.volume = volume;
    }
}
