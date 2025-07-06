using UnityEngine;
using System.Collections;

public class CurtainController : MonoBehaviour
{
    public Transform leftCurtain;
    public Transform rightCurtain;
    public Transform topLeft;
    public Transform topRight;
    public Transform topMid;

    public float sideMoveDistance = 3f;
    public float sideMoveDuration = 2f;

    public float topMoveDistance = 3f;
    public float topMoveDuration = 0.5f;

    void Start()
    {
        StartCoroutine(OpenCurtains());
    }

    IEnumerator OpenCurtains()
    {
        // Positionen berechnen
        Vector3 leftTarget = leftCurtain.position + Vector3.left * sideMoveDistance;
        Vector3 rightTarget = rightCurtain.position + Vector3.right * sideMoveDistance;

        // Öffne linke und rechte gleichzeitig
        float elapsed = 0f;
        Vector3 leftStart = leftCurtain.position;
        Vector3 rightStart = rightCurtain.position;

        while (elapsed < sideMoveDuration)
        {
            float t = elapsed / sideMoveDuration;
            leftCurtain.position = Vector3.Lerp(leftStart, leftTarget, t);
            rightCurtain.position = Vector3.Lerp(rightStart, rightTarget, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Sicherstellen, dass sie am Ziel sind
        leftCurtain.position = leftTarget;
        rightCurtain.position = rightTarget;

        // Kleiner Delay, wenn du willst
        //yield return new WaitForSeconds(0.2f);

        // Obere Vorhänge schnell wegziehen
        Vector3 up = Vector3.up * topMoveDistance;

        StartCoroutine(MoveOverTime(topLeft, topLeft.position, topLeft.position + up, topMoveDuration));
        StartCoroutine(MoveOverTime(topRight, topRight.position, topRight.position + up, topMoveDuration));
        StartCoroutine(MoveOverTime(topMid, topMid.position, topMid.position + up, topMoveDuration));
    }

    IEnumerator MoveOverTime(Transform obj, Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            obj.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.position = to;
    }
}
