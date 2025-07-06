using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;         // Spielerziel
    public float baseSpeed = 2f;     // Normale Verfolgungsgeschwindigkeit
    public float burstSpeed = 6f;    // Geschwindigkeit beim Boost
    public float burstDuration = 2f; // Wie lange der Boost anhält
    public float burstInterval = 5f; // Wie oft der Boost kommt

    private float currentSpeed;
    private float burstTimer;
    private float burstCooldown;

    private void Start()
    {
        currentSpeed = baseSpeed;
        burstCooldown = burstInterval;
    }

    void Update()
    {
        if (player == null) return;

        // Richtung berechnen
        Vector2 direction = (player.position - transform.position).normalized;

        // Bewegung
        transform.position += (Vector3)(direction * currentSpeed * Time.deltaTime);

        // Boost-Logik
        if (burstCooldown > 0)
        {
            burstCooldown -= Time.deltaTime;
        }
        else
        {
            burstTimer -= Time.deltaTime;
            currentSpeed = burstSpeed;

            if (burstTimer <= 0)
            {
                // Boost vorbei
                currentSpeed = baseSpeed;
                burstCooldown = burstInterval;
                burstTimer = burstDuration;
            }
        }
    }
}