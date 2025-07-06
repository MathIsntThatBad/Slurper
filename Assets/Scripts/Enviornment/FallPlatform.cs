using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallPlatform : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float disappearAfter = 1.5f;
    [SerializeField] private float respawnTime = 5f;

    private Collider2D col;
    private SpriteRenderer sr;
    private bool triggered = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.collider.CompareTag("Player"))
        {
            triggered = true;
            Invoke(nameof(StartBlinkAndFall), delay);
        }
    }

    void StartBlinkAndFall()
    {
        StartCoroutine(FallCoroutine());
    }

    void StartRespawn()
    {
        col.enabled = true;
        sr.enabled = true;
        triggered = false;
    }
    private IEnumerator FallCoroutine()
    {
        float blinkTime = disappearAfter;
        float blinkInterval = 0.1f;

        float timer = 0f;
        while (timer < blinkTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // Jetzt komplett deaktivieren
        sr.enabled = false;
        col.enabled = false;

        Invoke(nameof(StartRespawn), respawnTime);
    }
}
