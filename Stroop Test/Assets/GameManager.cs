using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState CurrentGameState;
    public enum GameState
    {
        MainMenu,
        Options,
        InGame,
        LoseMenu,
        ScoreMenu
    }

    [Header("Main Menu")]
    public Canvas MainMenuCanvas;

    [Header("Options")]
    public Canvas OptionsCanvas;
    public TMP_Dropdown NumberOfColoursDropdown;
    public int NumberOfColours;
    public TMP_Dropdown NumberOfGamesDropdown;
    public int NumberOfGames;

    [Header("Game")]
    public Canvas GameCanvas;
    public int Score;
    public Button[] Buttons;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;

    [Header("Lose Screen")]
    public Canvas LoseCanvas;


    [Header("Score Screen")]
    public Canvas ScoreCanvas;
    public TextMeshProUGUI ScoreScreenScoreText;
    public TextMeshProUGUI ScoreScreenHighscoreText;

    [Header("Colour")]
    public Colour[] Colours;
    public TextMeshProUGUI ColourText;
    public Colour WordColour;
    public Colour WordText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SwitchGameState();
        }
    }

    void SwitchGameState()
    {
        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                CurrentGameState = GameState.Options;
                SetActiveCanvas(OptionsCanvas);
                SetActiveCanvas(MainMenuCanvas, false);
                break;

            case GameState.LoseMenu:
                CurrentGameState = GameState.ScoreMenu;
                SetActiveCanvas(ScoreCanvas);
                SetActiveCanvas(LoseCanvas, false);
                break;

            case GameState.ScoreMenu:
                CurrentGameState = GameState.MainMenu;
                SetActiveCanvas(MainMenuCanvas);
                SetActiveCanvas(ScoreCanvas, false);
                break;
        }
    }

    void SetActiveCanvas(Canvas canvas, bool setActive = true)
    {
        if(setActive)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        CurrentGameState = GameState.InGame;
        NumberOfColours = NumberOfColoursDropdown.value + 4;
        NumberOfGames = NumberOfGamesDropdown.value;
        SetActiveCanvas(OptionsCanvas, false);
        SetActiveCanvas(GameCanvas);
        Score = 0;
        ScoreText.text = Score.ToString();
        ResetColours(NumberOfColours);
        ChangeColour();
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

    [ContextMenu("Change Colour")]
    public void ChangeColour()
    {
        WordColour = Colours[Random.Range(0, NumberOfColours)];
        WordText = Colours[Random.Range(0, NumberOfColours)];

        while (WordText == WordColour)
        {
            WordText = Colours[Random.Range(0, NumberOfColours)];
        }

        ColourText.text = WordText.Name;
        ColourText.color = new Color32(WordColour.R, WordColour.G, WordColour.B, 255);
    }

    public void LoseGame()
    {
        if(Score > PlayerPrefs.GetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games"))
        {
            PlayerPrefs.SetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games", Score);
        }

        CurrentGameState = GameState.ScoreMenu;
        SetActiveCanvas(GameCanvas, false);
        SetActiveCanvas(ScoreCanvas);
        ScoreScreenScoreText.text = Score.ToString();
        ScoreScreenHighscoreText.text = PlayerPrefs.GetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games").ToString();

    }
}
