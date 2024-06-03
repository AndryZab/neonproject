using System.Collections;
using UnityEngine;

public class BlockHidden : MonoBehaviour
{
    public float fadeDuration = 1.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisappearEffect());
        }
        Debug.Log(collision.gameObject.name);
    }

    IEnumerator DisappearEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = spriteRenderer.material;

        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1.25f, 0.0f, timer / fadeDuration);
            material.SetFloat("Vector1_E974001A", alpha); // Оновлення параметра Dissolve_Amount
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
