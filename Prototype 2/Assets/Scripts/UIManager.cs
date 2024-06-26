using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] amount;
    public TextMeshProUGUI score, time;
    public GameObject menu, achievement;
    public Button playButton, achievementButton, backButton;
    
    private void Start() {
        var data = SimpleJSON.JSON.Parse(File.ReadAllText(Constant.achievementPath));
        score.text = "High Score: " + data.GetString(Constant.highScore);
        time.text = FormatTime(Mathf.CeilToInt(data.GetInt("time")));

        achievementButton.onClick.AddListener(Achievement);
        playButton.onClick.AddListener(Play);
        backButton.onClick.AddListener(Back);
        
        amount[0].text = data.GetString("amount", "dog");
        amount[1].text = data.GetString("amount", "fox");
        amount[2].text = data.GetString("amount", "moose");
    }

    private string FormatTime(int second) {
        TimeSpan time = TimeSpan.FromSeconds(second);
        return time.ToString("mm\\:ss");
    }

    private void Back() {
        achievement.SetActive(false);
        menu.SetActive(true);
    }

    private void Achievement() {
        achievement.SetActive(true);
        menu.SetActive(false);
    }

    private void Play() {
        SceneManager.LoadScene(1);
    }
}
