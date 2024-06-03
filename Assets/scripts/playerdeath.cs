using System.Collections;
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
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
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
        audiomanager.musicsource.clip = audiomanager.background;
        audiomanager.musicsource.Play();
        respplayer.LoadObjectStates();
        if (brokennon != null)
        {
           Transform parentTransform = brokennon.transform;
           int childCount = parentTransform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                 GameObject childObject = parentTransform.GetChild(i).gameObject;

                 childObject.SetActive(true);

                 SpriteRenderer spriteRenderer = childObject.GetComponent<SpriteRenderer>();
                 if (spriteRenderer != null)
                 {
                    Color color = spriteRenderer.color;
                    color.a = 1f; // 1f означає повну непрозорість
                    spriteRenderer.color = color;
                 }
            }

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint")) // Перевіряємо, чи це чекпоінт
        {
            lastPassedCheckpoint = other.gameObject; // Зберігаємо останній пройдений чекпоінт
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
        audiomanager.Stopmusic();
        respplayer.DeactivateAllObjects();
        scripteffects.effectsource.Stop();
        audiomanager.PlaySFX(audiomanager.death);
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        playerMovement.DisableFlip();
        Invoke(nameof(ShowDeathCanvas), 0.55f);
    }

    public void ShowDeathCanvas()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
    }
}
