using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourButton : MonoBehaviour
{
    public Colour Colour;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if(Colour == GameManager.instance.WordColour)
        {
            GameManager.instance.ChangeColour();
            GameManager.instance.Score++;
            GameManager.instance.ScoreText.text = GameManager.instance.Score.ToString();
        }
        else
        {
            GameManager.instance.LoseGame();
        }
    }
}
