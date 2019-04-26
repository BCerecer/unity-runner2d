using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HighScoreText : MonoBehaviour
{
    TextMeshProUGUI highscore;

    void OnEnable()
    {
        highscore = GetComponent<TextMeshProUGUI>();
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }
}
