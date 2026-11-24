using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 5f;

    public const string HighScoreKey = "HighScore";
    private float score;
    private bool isRunning = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (!isRunning) return;

        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int GetCurrentCoins()
    {
        return Mathf.FloorToInt(score);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public void CheckHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }

    public int GetCurrentScore()
    {
        return Mathf.FloorToInt(score);
    }

    public void StopScoreAndSave()
    {
        isRunning = false;

        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        int currentScoreInt = Mathf.FloorToInt(score);

        if (currentScoreInt > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, currentScoreInt);
            PlayerPrefs.Save();
        }
    }
}
