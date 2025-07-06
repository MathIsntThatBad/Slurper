using UnityEngine;
public class ParallaxRepeater : MonoBehaviour
{
    public float parallaxSpeed = 0.2f; // Wie stark sich die Textur pro Kameraeinheit bewegt
    public Transform cam;              // Die Kamera, der gefolgt wird

    private Material mat;
    private Vector2 lastCamPos;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        if (cam == null) cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void Update()
    {
        Vector2 deltaMovement = new Vector2(cam.position.x - lastCamPos.x, 0f);
        Vector2 offset = mat.mainTextureOffset + deltaMovement * parallaxSpeed;
        mat.mainTextureOffset = offset;
        lastCamPos = cam.position;
    }
}
