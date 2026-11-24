using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (ScoreSystem.Instance != null)
        {
            ScoreSystem.Instance.StopScoreAndSave();

            int finalScore = ScoreSystem.Instance.GetCurrentScore();
            int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

            int currentTotalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            PlayerPrefs.SetInt("TotalCoins", currentTotalCoins + finalScore);
            PlayerPrefs.Save();

            if (finalScoreText != null)
                finalScoreText.text = "Score: " + finalScore.ToString();

            if (highScoreText != null)
                highScoreText.text = "Best: " + highScore.ToString();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
