using UnityEngine;
public class CoinCollector : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            MusicManager.Instance.PlayCoinGatheredSound();
            Destroy(collision.gameObject);
            GameManager.Instance.CollectItem("Coin", 1);
            Debug.Log("Coin collected");
        }
        if (collision.gameObject.CompareTag("Banana"))
        {
            MusicManager.Instance.PlayOtherGatheredSound();
            Destroy(collision.gameObject);
            GameManager.Instance.CollectItem("Banana", 1);
            Debug.Log("Banana collected");
        }
    }
}