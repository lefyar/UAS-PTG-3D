using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    [Header("UI Panels")]
    public GameObject homePanel;
    public GameObject shopPanel;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = $"High Score {highScore}";
    }

    public void OpenShop()
    {
        homePanel.SetActive(false);
        shopPanel.SetActive(true);

        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.UpdateUI();
        }

    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        homePanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
