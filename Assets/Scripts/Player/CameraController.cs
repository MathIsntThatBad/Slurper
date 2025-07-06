using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 2f;
    public float yOffset = 1f;
    public Transform target;

    public Transform leftBoundary;
    public Transform rightBoundary;
    public Transform bottomBoundary;
    public Transform topBoundary;


    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Start()
    {
        Camera cam = Camera.main;

        // orthographicSize ist die halbe Höhe der Kamera
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
    }

    private void Update()
    {

        if (target == null || leftBoundary == null || rightBoundary == null || topBoundary == null || bottomBoundary == null)
            return;

        Camera cam = Camera.main;
        float cameraHalfHeight = cam.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * cam.aspect;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        // Grenzen abrufen
        float minX = leftBoundary.position.x + cameraHalfWidth;
        float minY = bottomBoundary.position.y + cameraHalfHeight;
        float maxY = topBoundary.position.y - cameraHalfHeight;
        float maxX = rightBoundary.position.x - cameraHalfWidth;


        // Position clampen
        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY); // oben & unten

        // Neue Position setzen mit sanftem Übergang
        Vector3 finalPosition = new Vector3(clampedX, clampedY, -10f);
        transform.position = Vector3.Lerp(transform.position, finalPosition, speed * Time.deltaTime);
    }
}
