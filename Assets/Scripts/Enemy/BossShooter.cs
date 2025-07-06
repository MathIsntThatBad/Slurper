using System.Collections;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;

    [Header("Shooting Timing")]
    public float minShootDelay = 2f;
    public float maxShootDelay = 4f;

    [Header("Player Reference")]
    public Transform player;

    private void Start()
    {
        StartCoroutine(ShootingRoutine());
    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            // Warten bis zum nächsten Angriff
            float waitTime = Random.Range(minShootDelay, maxShootDelay);
            yield return new WaitForSeconds(waitTime);

            // Anzahl der Projectiles bestimmen (1–3)
            int shots = Random.Range(1, 4);

            for (int i = 0; i < shots; i++)
            {
                Shoot();

                // kurze Pause zwischen den Schüssen
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || shootPoint == null || player == null)
        {
            Debug.LogWarning("Projectile oder ShootPoint oder Player nicht zugewiesen!");
            return;
        }

        // Richtung zum Spieler
        Vector2 direction = (player.position - shootPoint.position).normalized;

        // Projektil instanziieren
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Richtung mit Rigidbody setzen
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
