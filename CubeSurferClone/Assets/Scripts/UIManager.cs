using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    GameObject levelIndicator;
    [SerializeField]
    TMP_Text currentLevelText;
    [SerializeField]
    Slider currentLevelProgress;
    [SerializeField]
    GameObject levelIndicatorPanel;
    [SerializeField]
    GameObject levelProgressPanel;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject nextLevelPanel;
    public void UpdateUI()
    {
        scoreText.text = PlayerPrefs.GetInt("score", 0).ToString();
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        currentLevelText.text = currentLevel.ToString();
        for (int i = 0; i < levelIndicator.transform.childCount; i++)
        {
            levelIndicator.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = (currentLevel + i).ToString();
        }
        levelIndicatorPanel.SetActive(true);
    }

    public void StartGame()
    {
        levelIndicatorPanel.SetActive(false);
        levelProgressPanel.SetActive(true);
    }

    public void UpdatePathProgress(float _percent)
    {
        currentLevelProgress.value = _percent;
    }

    public void ShowGameOver()
    {
        levelProgressPanel.SetActive(false);

        gameOverPanel.SetActive(true);
    }

    public void NextLevel()
    {
        levelProgressPanel.SetActive(false);

        nextLevelPanel.SetActive(true);
    }
    public void HideScore()
    {
        scoreText.transform.parent.gameObject.SetActive(false);
    }
}