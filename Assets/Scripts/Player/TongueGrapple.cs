using UnityEngine;

public class TongueGrapple : MonoBehaviour
{
    [Header("Zunge Setup")]
    [SerializeField] private Transform tongueOrigin;
    [SerializeField] private float maxTongueLength = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioClip tongueOut;
    [SerializeField] private AudioClip tongueIn;
    [SerializeField] private AudioSource audioSource;

    [Header("Line Renderer")]
    [SerializeField] private LineRenderer lineRenderer;

    private SpringJoint2D joint;
    private Rigidbody2D rb;
    private bool isAttached = false;
    private Vector2 grapplePoint;
    //TODO:
    private New_Movement playerMovement;

    private void Awake()
    {
        //TODO:
        playerMovement = GetComponent<New_Movement>();


        rb = GetComponent<Rigidbody2D>();

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.05f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerMovement.IsInFormA())
        {
            TryGrapple();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseGrapple();
        }

        if (isAttached)
        {
            UpdateLineRenderer();
        }
        //Include logic where tongue can be used to pull to the target 
        if (isAttached)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Force of left and right swing 
            rb.AddForce(new Vector2(horizontalInput * 20f, 0f));

            //Pull up 
            if (verticalInput > 0)
            {
                joint.distance -= 5f * Time.deltaTime; 
                joint.distance = Mathf.Max(1f, joint.distance); 
            }
            //Pull down 
            if (verticalInput < 0)
            {
                joint.distance += 5f * Time.deltaTime;
                joint.distance = Mathf.Min(maxTongueLength, joint.distance);
            }
        }
    }

    private void TryGrapple()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - tongueOrigin.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(tongueOrigin.position, direction, maxTongueLength, groundLayer);

        if (hit.collider != null)
        {

            //
            audioSource.PlayOneShot(tongueOut);
            //
            isAttached = true;
            grapplePoint = hit.point;

            joint = gameObject.AddComponent<SpringJoint2D>();
            joint.autoConfigureDistance = false;
            joint.connectedAnchor = grapplePoint;
            joint.distance = Vector2.Distance(transform.position, grapplePoint);
            joint.dampingRatio = 0.5f;
            joint.frequency = 1.5f;

            joint.enableCollision = true;

           
            lineRenderer.positionCount = 2;
        }
    }

    public void ReleaseGrapple()
    {
        if (joint != null)
        {
            //
            audioSource.PlayOneShot(tongueIn);
            //
            Destroy(joint);
        }

        isAttached = false;
        lineRenderer.positionCount = 0;
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, tongueOrigin.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }
}
