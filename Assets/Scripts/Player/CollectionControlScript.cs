using UnityEngine;
public class CoinCollector : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score += 1;
            Destroy(collision.gameObject);
            GameManager.Instance.CollectItem("Coin", 1);
            Debug.Log("Coin collected");
        }
        if (collision.gameObject.CompareTag("Banana"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.CollectItem("Banana", 1);
            Debug.Log("Banana collected");
        }
    }
}