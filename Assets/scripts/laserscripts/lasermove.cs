﻿using UnityEngine;
using System.Collections;

public class laserhit : MonoBehaviour
{
    public LayerMask raycastLayerMask;
    public PlayerDeath PlayerDeathScript;
    [SerializeField] private float DefDistanceRay = 100;
    public Transform laserfirepoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    private bool playerHit = false;
    public GameObject laserbutonON;
    public GameObject laserbutonOFF;
    public ParticleSystem sparticleSystem;
    [SerializeField] private float scrollSpeedX = 0.5f;
    private float cordY;
    private float offsetX;
    public Material sharedMaterial;
    public bool OnDisolve = false;
    public AudioSource shieldsound;
    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        
        

    }

    void Start()
    {
        
        if (sparticleSystem != null)
        {
            sparticleSystem.Stop();
        }
    }

    private void LateUpdate()
    {
        ShootLaser();
        ScrollTexture();
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserfirepoint.position, transform.right, DefDistanceRay, raycastLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") && !playerHit)
            {
                PlayerDeathScript.Die();
                playerHit = true;
                if (PlayerDeathScript.deathCanvas)
                {
                    playerHit = false;
                }
            }
            if (hit.collider.CompareTag("buttonlasser"))
            {
                if (laserbutonON != null)
                {
                    laserbutonON.SetActive(false);
                }
                if (laserbutonOFF != null)
                {
                    laserbutonOFF.SetActive(true);
                }
            }
            if (hit.collider.CompareTag("Shield"))
            {
                OnDisolve = true;
                if (sparticleSystem != null)
                {
                   sparticleSystem.Play();

                }
                if (shieldsound != null)
                {
                    shieldsound.UnPause();
                }
            }
            else
            {
                OnDisolve = false;

                if (sparticleSystem != null)
                {
                  sparticleSystem.Stop();

                }
                if (shieldsound != null)
                {
                    shieldsound.Pause();
                }
            }
        }

        Draw2DRay(laserfirepoint.position, hit.point != null ? hit.point : (Vector2)laserfirepoint.transform.right * DefDistanceRay);
    }

    void Draw2DRay(Vector2 StartPos, Vector2 EndPos)
    {
        m_lineRenderer.SetPosition(0, StartPos);
        m_lineRenderer.SetPosition(1, EndPos);

        if (sparticleSystem != null)
        {
            sparticleSystem.transform.position = EndPos;
            
        }
    }

    void ScrollTexture()
    {
        // Отримати поточне значення Y з матеріалу
        float currentY = sharedMaterial.mainTextureOffset.y;

        // Оновити значення X
        if (offsetX <= 0)
        {
            offsetX -= Time.deltaTime * scrollSpeedX;

            if (offsetX <= -100f)
            {
                offsetX = 0f;
            }
        }

        // Встановити нове значення X, залишаючи Y незмінним
        sharedMaterial.mainTextureOffset = new Vector2(offsetX, currentY);
    }

    
}
