using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourButton : MonoBehaviour
{
    //the text which changes colour and word
    public ColourText ColourText;

    //the colour scriptable object that corresponds to the text of this button
    public Colour Colour;

    //the button component of this gameobject
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //if the text of this button is the same as the word's colour
        if(Colour == ColourText.WordColour)
        {
            //change the colour
            ColourText.ChangeColour();

            //increase the score
            GameManager.instance.Score++;
            ColourText.ScoreText.text = GameManager.instance.Score.ToString();

            //reset the timer
            ColourText.TimeRemaining = GameManager.instance.StartTime;
        }

        //otherwise
        else
        {
            //lose the game
            GameManager.instance.LoseGame();
        }
    }
}
