using UnityEngine;

public class New_Movement : MonoBehaviour
{
    [Header("Bewegung")]
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float moveSpeed = 5f;
    

    [Header("Form-Objekte")]
    [SerializeField] private GameObject visualA;
    [SerializeField] private GameObject visualB;

    private Rigidbody2D body;
    private Animator animator;
    private bool isGrounded = false;
    private int jumpCount = 0;
    private bool isFormA = true;
    private float defaultScaleX;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        defaultScaleX = transform.localScale.x;

        SetActiveForm(true); // Form A aktivieren zu Beginn
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Bewegung
        body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);

        // Flip nach Blickrichtung
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(defaultScaleX, transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-defaultScaleX, transform.localScale.y, transform.localScale.z);

        // Animator-Werte
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("isJumping", !isGrounded);

        // Springen
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Form wechseln mit Taste X
        if (Input.GetKeyDown(KeyCode.X))
            ToggleForm();
    }

    private void Jump()
    {
        if (isGrounded || jumpCount < 2)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ToggleForm()
    {
        isFormA = !isFormA;
        SetActiveForm(isFormA);
    }

    private void SetActiveForm(bool useFormA)
    {
        // Visuals aktivieren/deaktivieren
        visualA.SetActive(useFormA);
        visualB.SetActive(!useFormA);

        // Animator setzen
        animator = (useFormA ? visualA : visualB).GetComponent<Animator>();

        // Collider wechseln
        var colliderA = visualA.GetComponent<Collider2D>();
        var colliderB = visualB.GetComponent<Collider2D>();
        if (colliderA != null) colliderA.enabled = useFormA;
        if (colliderB != null) colliderB.enabled = !useFormA;

        // Optional: Eigene Werte je Form
        if (useFormA)
        {
            moveSpeed = 5f;
            jumpSpeed = 10f;
        }
        else
        {
            moveSpeed = 7f;
            jumpSpeed = 12f;
        }
    }

    // Optional für andere Scripte
    public bool IsInFormA() => isFormA;
}
