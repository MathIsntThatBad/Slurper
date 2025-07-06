using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollisionControll : MonoBehaviour
{
    public GameObject[] hearts;
    private int currentLives;
    public GameObject gameOverUI;

    public GameObject clearUI;
    public Text level1Text;
    public Text level2Text;
    public Text level3Text;
    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f;
    private float invincibilityTimer;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public float blinkInterval = 0.1f;


    private void Start()
    {
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
        currentLives = hearts.Length;
        UpdateHearts();
    }
    private void Update()
    {
        //Logic for invincibility
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            currentLives--;
            currentLives--;
            currentLives--;
            UpdateHearts();
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            string levelName = SceneManager.GetActiveScene().name;
            if (levelName == "Boss")
            {
                SceneManager.LoadScene("LevelSelect");
            }
            else
            {
                //Save logic
                float zeit = Time.timeSinceLevelLoad;
                GameManager.Instance.CompleteLevel(levelName, zeit);
                Debug.Log("Level " + levelName + " is completed!");

                //Level selection logic
                string[] parts = levelName.Split('_');
                string prefix = parts[0];
                int levelNumber = int.Parse(parts[1]);
                if (levelNumber == 3)
                {
                    clearUI.SetActive(true);
                    string levelName1 = prefix + "_1";
                    float bestTime1 = GameManager.Instance.GetLevelBestTime(levelName1);
                    string text1 = $"Best Time: {bestTime1:F2} s";
                    level1Text.text = text1;

                    string levelName2 = prefix + "_2";
                    float bestTime2 = GameManager.Instance.GetLevelBestTime(levelName2);
                    string text2 = $"Best Time: {bestTime2:F2} s";
                    level2Text.text = text2;

                    string levelName3 = prefix + "_3";
                    float bestTime3 = GameManager.Instance.GetLevelBestTime(levelName3);
                    string text3 = $"Best Time: {bestTime3:F2} s";
                    level3Text.text = text3;


                    //Stat screen afer completing region
                    Debug.Log("Level " + levelName1 + " best time: " + bestTime1);
                    Debug.Log("Level " + levelName2 + " best time: " + bestTime2);
                    Debug.Log("Level " + levelName3 + " best time: " + bestTime3);

                    //SceneManager.LoadScene("LevelSelect");
                }
                else
                {
                    string newLevel = prefix + "_" + (levelNumber + 1);
                    SceneManager.LoadScene(newLevel);
                }
            }
        }
    }
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i == currentLives - 1)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
    private void TakeDamage()
    {
        //Dont take damage if currently invincible
        if (isInvincible) return;

        currentLives--;
        UpdateHearts();
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        StartCoroutine(BlinkSprite());
        if (currentLives <= 0)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private IEnumerator BlinkSprite()
    {
        while (isInvincible)
        {
            // Immer aktuelle aktive SpriteRenderer holen
            var activeSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true)
                .Where(sr => sr.gameObject.activeInHierarchy).ToList();

            foreach (var sr in activeSpriteRenderers)
            {
                sr.enabled = !sr.enabled;
            }

            yield return new WaitForSeconds(blinkInterval);

            // Wieder zurückschalten, damit beim nächsten Frame korrekt geblinkt wird
            foreach (var sr in activeSpriteRenderers)
            {
                sr.enabled = !sr.enabled;
            }

            yield return new WaitForSeconds(blinkInterval);
        }

        // Nach Unverwundbarkeit: alle SpriteRenderer sichtbar machen
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>(true))
        {
            sr.enabled = true;
        }
    }
}
