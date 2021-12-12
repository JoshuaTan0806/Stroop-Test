using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourButton : MonoBehaviour
{
    public ColourText ColourText;
    public Colour Colour;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if(Colour == ColourText.WordColour)
        {
            ColourText.ChangeColour();
            GameManager.instance.Score++;
            ColourText.ScoreText.text = GameManager.instance.Score.ToString();
            ColourText.TimeRemaining = GameManager.instance.StartTime;
        }
        else
        {
            GameManager.instance.LoseGame();
        }
    }
}
