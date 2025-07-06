using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float walkDuration = 2f;
    public float idleDuration = 1f;
    public float detectionRange = 10f;
    public float chaseSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    private enum State { PatrolWalk, PatrolIdle, Chase }
    private State currentState = State.PatrolWalk;

    private float stateTimer = 0f;
    private int direction = -1; // Startet mit links (-1)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.PatrolWalk:
                PatrolWalk();
                if (PlayerInRange()) SwitchToState(State.Chase);
                break;

            case State.PatrolIdle:
                PatrolIdle();
                if (PlayerInRange()) SwitchToState(State.Chase);
                break;

            case State.Chase:
                Chase();
                if (!PlayerInRange()) SwitchToState(State.PatrolIdle);
                break;
        }

        Animate(rb.linearVelocity);            
        Flip(rb.linearVelocity.x);             
    }

    void PatrolWalk()
    {
        stateTimer += Time.deltaTime;
        rb.linearVelocity = new Vector2(direction * patrolSpeed, rb.linearVelocity.y);

        if (stateTimer >= walkDuration)
        {
            rb.linearVelocity = Vector2.zero;
            SwitchToState(State.PatrolIdle);
        }
    }

    void PatrolIdle()
    {
        stateTimer += Time.deltaTime;
        rb.linearVelocity = Vector2.zero;

        if (stateTimer >= idleDuration)
        {
            direction *= -1;
            SwitchToState(State.PatrolWalk);
        }
    }

    void Chase()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * chaseSpeed;
    }

    void SwitchToState(State newState)
    {
        currentState = newState;
        stateTimer = 0f;
    }

    bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionRange;
    }

    void Animate(Vector2 velocity)
    {
        float horizontalSpeed = Mathf.Abs(velocity.x);
        animator.SetFloat("Speed", horizontalSpeed);
    }

    void Flip(float direction)
    {
        if (direction > 0.01f)
            spriteRenderer.flipX = false;
        else if (direction < -0.01f)
            spriteRenderer.flipX = true;
    }
    void OnDisable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}

