using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColourText : MonoBehaviour
{
    public Colour[] Colours;
    public TextMeshProUGUI Text;
    public Colour WordColour;
    public Colour WordText;

    public TextMeshProUGUI TimeText;
    public float TimeRemaining;

    public Button[] Buttons;

    public TextMeshProUGUI ScoreText;

    [ContextMenu("Change Colour")]
    public void ChangeColour()
    {
        WordColour = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];
        WordText = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];

        while (WordText == WordColour)
        {
            WordText = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];
        }

        Text.text = WordText.Name;
        Text.color = new Color32(WordColour.R, WordColour.G, WordColour.B, 255);
    }

    private void Update()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            TimeText.text = Mathf.RoundToInt(TimeRemaining).ToString();
        }
        else if (GameManager.instance.CurrentGameState == GameManager.GameState.InGame)
        {
            GameManager.instance.LoseGame();
        }
    }

    public void ResetColours(int NumberOfColours)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < NumberOfColours; i++)
        {
            Buttons[i].gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        TimeRemaining = GameManager.instance.StartTime;
        ResetColours(GameManager.instance.NumberOfColours);
        ChangeColour();
        ScoreText.text = GameManager.instance.Score.ToString();
    }
}
