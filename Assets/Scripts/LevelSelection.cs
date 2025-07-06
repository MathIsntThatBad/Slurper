using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerZoneMover : MonoBehaviour
{
    public Transform[] zones; // Die 4 Zonen als Positionen (Sprites)
    public int currentZoneIndex = 0;

    public float jumpHeight = 2f;
    public float jumpDuration = 0.3f;

    private bool isJumping = false;

    public Text fruitCount;
    public Text coinCount;
    public GameObject mountains;
    public GameObject forest;
    public GameObject beach;
    public GameObject cave;
    void Start()
    {
        // Startposition des Spielers auf der ersten Zone
        int coinCountNum = GameManager.Instance.GetItemCount("Coin");
        Debug.Log(coinCountNum);
        int fruitCountNum = GameManager.Instance.GetItemCount("Banana");
        Debug.Log(fruitCountNum);
        fruitCount.text = fruitCountNum.ToString();
        coinCount.text = coinCountNum.ToString();

        transform.position = zones[currentZoneIndex].position;
    }

    void Update()
    {
        if (isJumping) return;

        if (Input.GetKeyDown(KeyCode.W))
            TryJumpTo(GetZoneIndex("up"));
        else if (Input.GetKeyDown(KeyCode.A))
            TryJumpTo(GetZoneIndex("left"));
        else if (Input.GetKeyDown(KeyCode.S))
            TryJumpTo(GetZoneIndex("down"));
        else if (Input.GetKeyDown(KeyCode.D))
            TryJumpTo(GetZoneIndex("right"));

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartLevelForZone(currentZoneIndex);
        }
        setCurrentZoneLabel();
    }

    int GetZoneIndex(string direction)
    {
        // Beispiel für Viereck-Zonen:
        // Index 0 = unten links
        // Index 1 = unten rechts
        // Index 2 = oben rechts
        // Index 3 = oben links

        switch (direction)
        {
            case "up":
                if (currentZoneIndex == 7) return 8;
                if (currentZoneIndex == 8) return 1;
                if (currentZoneIndex == 5) return 4;
                if (currentZoneIndex == 4) return 3;
                if (currentZoneIndex == 6) return 9;
                if (currentZoneIndex == 9) return 2;
                break;
            case "down":
                if (currentZoneIndex == 1) return 8;
                if (currentZoneIndex == 8) return 7;
                if (currentZoneIndex == 3) return 4;
                if (currentZoneIndex == 4) return 5;
                if (currentZoneIndex == 2) return 9;
                if (currentZoneIndex == 9) return 6;
                break;
            case "left":
                if (currentZoneIndex == 3) return 2;
                if (currentZoneIndex == 5) return 6;
                if (currentZoneIndex == 2) return 1;
                if (currentZoneIndex == 6) return 7;
                if (currentZoneIndex == 4) return 9;
                if (currentZoneIndex == 9) return 8;
                break;
            case "right":
                if (currentZoneIndex == 0) return 1;
                if (currentZoneIndex == 1) return 2;
                if (currentZoneIndex == 2) return 3;
                if (currentZoneIndex == 7) return 6;
                if (currentZoneIndex == 6) return 5;
                if (currentZoneIndex == 8) return 9;
                if (currentZoneIndex == 9) return 4;
                break;
        }
        return currentZoneIndex; // Keine Bewegung
    }

    void TryJumpTo(int newZoneIndex)
    {
        if (newZoneIndex == currentZoneIndex) return;

        StartCoroutine(JumpToPosition(zones[newZoneIndex].position));
        currentZoneIndex = newZoneIndex;
    }

    System.Collections.IEnumerator JumpToPosition(Vector3 targetPos)
    {
        isJumping = true;

        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / jumpDuration;

            float height = 4 * jumpHeight * t * (1 - t); // Parabel-Höhe

            transform.position = Vector3.Lerp(startPos, targetPos, t) + Vector3.up * height;

            yield return null;
        }

        transform.position = targetPos;
        isJumping = false;
    }

    void StartLevelForZone(int zoneIndex)
    {
        // Beispiel: Lade für jede Zone ein anderes Level
        switch (zoneIndex)
        {
            case 1:
                SceneManager.LoadScene("F_1");
                MusicManager.Instance?.PlayCategoryMusic("Forest");
                break;
            case 3:
                SceneManager.LoadScene("B_1");
                MusicManager.Instance?.PlayCategoryMusic("Beach");
                break;
            case 5:
                SceneManager.LoadScene("M_1");
                MusicManager.Instance?.PlayCategoryMusic("Mountain");
                break;
            case 7:
                SceneManager.LoadScene("C_1");
                MusicManager.Instance?.PlayCategoryMusic("Cave");
                break;
            case 9:
                SceneManager.LoadScene("Boss");
                MusicManager.Instance?.PlayCategoryMusic("Boss");
                break;
        }
    }
    void setCurrentZoneLabel()
    {
        switch (currentZoneIndex)
        {
            case 1:
                forest.SetActive(true);
                beach.SetActive(false);
                mountains.SetActive(false);
                cave.SetActive(false);
                break;
            case 3:
                forest.SetActive(false);
                beach.SetActive(true);
                mountains.SetActive(false);
                cave.SetActive(false);
                break;
            case 5:
                forest.SetActive(false);
                beach.SetActive(false);
                mountains.SetActive(true);
                cave.SetActive(false);
                break;
            case 7:
                forest.SetActive(false);
                beach.SetActive(false);
                mountains.SetActive(false);
                cave.SetActive(true);
                break;
            default:
                forest.SetActive(false);
                beach.SetActive(false);
                mountains.SetActive(false);
                cave.SetActive(false);
                break;
        }

    }
    public void StartCurrentZone()
    {
        switch (currentZoneIndex)
        {
            case 1:
                SceneManager.LoadScene("F_1");
                MusicManager.Instance?.PlayCategoryMusic("Forest");
                break;
            case 3:
                SceneManager.LoadScene("B_1");
                MusicManager.Instance?.PlayCategoryMusic("Beach");
                break;
            case 5:
                SceneManager.LoadScene("M_1");
                MusicManager.Instance?.PlayCategoryMusic("Cave");
                break;
            case 7:
                SceneManager.LoadScene("C_1");
                MusicManager.Instance?.PlayCategoryMusic("Mountain");
                break;
            case 9:
                SceneManager.LoadScene("Boss");
                MusicManager.Instance?.PlayCategoryMusic("Boss");
                break;
        }
    }
    public void BackToStartScreen()
    {
        SceneManager.LoadScene("MainMenu");
        MusicManager.Instance?.PlayCategoryMusic("Menu");
    }
}
