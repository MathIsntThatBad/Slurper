using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeRight = 2f;
    [SerializeField] private float timeLeft = 2f;  

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = timeRight;
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        float moveDirection = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;
        timer = movingRight ? timeRight : timeLeft;


        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
