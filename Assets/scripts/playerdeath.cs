﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public LayerMask trapLayer;
    public ParticleTrail scripteffects;
    private CapsuleCollider2D colider;
    public ObjectManager respplayer;
    public Player playerMovement;
    public GameObject deathCanvas;
    public Button pausebuttonactiveoff;
    private Rigidbody2D rb;
    private Animator anim;
    public Player groundCheck; 
    Audiomanager audiomanager;
    [SerializeField] private GameObject checkpoint;
    private GameObject lastPassedCheckpoint;
    public GameObject brokennon;
    public bool playerdead = false;

    public bool checkforplatformreset = false;
    AudioSource[] sourceaudio;
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
        sourceaudio = FindObjectsOfType<AudioSource>();
    }
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colider = GetComponent<CapsuleCollider2D>();


        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
    }

    public void RespawnPlayer()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
        if (colider != null)
        {
            colider.enabled = true;
        }
        pausebuttonactiveoff.interactable = true;

        if (lastPassedCheckpoint != null)
        {
            transform.position = lastPassedCheckpoint.transform.position;
        }

        playerMovement.EnableFlip();

        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.Play("Idle");
        foreach (AudioSource audioSource in sourceaudio)
        {
            audioSource.Play();

        }
        respplayer.LoadObjectStates();
        checkforplatformreset = true;
        if (brokennon != null)
        {
            EnableAllChildren(brokennon.transform);
        }
    }

    private void EnableAllChildren(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            child.gameObject.SetActive(true);
            Debug.Log("Enabled: " + child.name); 
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (spriteRenderer.material != null)
                {
                    spriteRenderer.material.SetFloat("Vector1_E974001A", 1.9f);

                }
                Color color = spriteRenderer.color;
                color.a = 1f; 
                spriteRenderer.color = color;
                Debug.Log("Set opacity for: " + child.name); 
            }

            
            if (child.childCount > 0)
            {
                EnableAllChildren(child);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint")) 
        {
            lastPassedCheckpoint = other.gameObject; 
            Debug.Log(lastPassedCheckpoint);
        }

    }

    private bool IsTrap(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f, trapLayer);
        return colliders.Length > 0;
    }









    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsTrap(collision.gameObject))
        {
            Die();
        }
    }

    private bool IsTrap(GameObject obj)
    {
        return obj.CompareTag("Trap") || obj.layer == LayerMask.NameToLayer("traps");
    }

    public void Die()
    {
        if (colider != null)
        {
            colider.enabled = false;
        }
        pausebuttonactiveoff.interactable = false;
        playerdead = true;
        respplayer.DeactivateAllObjects();
        audiomanager.PlaySFX(audiomanager.death);
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        playerMovement.DisableFlip();
        Invoke(nameof(stopallsources), 0.9f);
        Invoke(nameof(ShowDeathCanvas), 0.9f);
    }

    private void stopallsources()
    {
        foreach (AudioSource audioSource in sourceaudio)
        {
            audioSource.Stop();

        }
    }
    public void ShowDeathCanvas()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
    }
}
