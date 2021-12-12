using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColourText : MonoBehaviour
{
    //all colours in the same order as the buttons
    public Colour[] Colours;

    //the buttons for each different colour
    public Button[] Buttons;

    //the text which changes colour and word
    public TextMeshProUGUI Text;

    //the current colour of this word
    [HideInInspector] public Colour WordColour;

    //the current text of this word
    [HideInInspector] public Colour WordText;

    //text of how much time is left
    public TextMeshProUGUI TimeText;
    //how much time is left
    [HideInInspector] public float TimeRemaining;

    //text of the player's current score
    public TextMeshProUGUI ScoreText;

    /// <summary>
    /// Changes the colour and text of the displayed word
    /// </summary>
    [ContextMenu("Change Colour")]
    public void ChangeColour()
    {
        //select a random colour for the colour of the displayed word
        WordColour = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];
        //select a random colour for the text of the displayed word
        WordText = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];

        //ensures the colour and text isn't the same
        while (WordText == WordColour)
        {
            WordText = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];
        }

        //set the words text
        Text.text = WordText.Name;

        //set the words colour
        Text.color = new Color32(WordColour.R, WordColour.G, WordColour.B, 255);
    }

    private void Update()
    {
        //tick down the time remaining
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            TimeText.text = Mathf.RoundToInt(TimeRemaining).ToString();
        }
        //if the time reaches 0, lose the game
        else if (GameManager.instance.CurrentGameState == GameManager.GameState.InGame)
        {
            GameManager.instance.LoseGame();
        }
    }

    /// <summary>
    /// sets the number of buttons depending on the number of colours selected
    /// </summary>
    /// <param name="NumberOfColours"></param>
    public void ResetColours(int NumberOfColours)
    {
        //set all buttons to false first
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].gameObject.SetActive(false);
        }

        //enable the amount of buttons corresponding to the number of colours selected
        for (int i = 0; i < NumberOfColours; i++)
        {
            Buttons[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// initialise some properties
    /// </summary>
    public void StartGame()
    {
        //reset all timers
        TimeRemaining = GameManager.instance.StartTime;

        // set the number of buttons depending on the number of colours selected
        ResetColours(GameManager.instance.NumberOfColours);

        //choose a random colour and word for the text
        ChangeColour();

        //reset the score to 0
        ScoreText.text = GameManager.instance.Score.ToString();
    }
}
