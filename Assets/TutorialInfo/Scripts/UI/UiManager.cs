using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject winPanel;
    public TextMeshProUGUI winnerText;

    void Start()
    {
        // Game starts paused
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void StartGame()
    {
        // 🔒 Prevent starting again mid-race
        if (Time.timeScale == 1f)
            return;

        // Hide start UI
        startPanel.SetActive(false);

        // Reset race
        RaceManager.Instance.ResetRace();

        // Start race
        Time.timeScale = 1f;
    }

    public void ShowWinner(string winnerName)
    {
        // Lock game
        Time.timeScale = 0f;

        // Show result
        winPanel.SetActive(true);
        winnerText.text = winnerName;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
