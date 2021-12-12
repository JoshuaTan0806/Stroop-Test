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
    public ColourText Game1Text;

    public Canvas GameCanvas2;
    public ColourText Game2Text;
    public ColourText Game2Text2;

    public Canvas GameCanvas4;
    public ColourText Game4Text;
    public ColourText Game4Text2;
    public ColourText Game4Text3;
    public ColourText Game4Text4;

    public int StartTime;
    public int Score;


    [Header("Lose Screen")]
    public Canvas LoseCanvas;


    [Header("Score Screen")]
    public Canvas ScoreCanvas;
    public TextMeshProUGUI ScoreScreenScoreText;
    public TextMeshProUGUI ScoreScreenHighscoreText;

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
        Score = 0;

        if (NumberOfGames == 0)
        {
            SetActiveCanvas(GameCanvas);
            Game1Text.StartGame();
        }
        else if(NumberOfGames == 1)
        {
            SetActiveCanvas(GameCanvas2);
            Game2Text.StartGame();
            Game2Text2.StartGame();
        }
        else
        {
            SetActiveCanvas(GameCanvas4);
            Game4Text.StartGame();
            Game4Text2.StartGame();
            Game4Text3.StartGame();
            Game4Text4.StartGame();
        }
    }

    public void LoseGame()
    {
        if(Score > PlayerPrefs.GetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games"))
        {
            PlayerPrefs.SetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games", Score);
        }

        CurrentGameState = GameState.ScoreMenu;
        SetActiveCanvas(GameCanvas, false);
        SetActiveCanvas(GameCanvas2, false);
        SetActiveCanvas(GameCanvas4, false);

        SetActiveCanvas(ScoreCanvas);
        ScoreScreenScoreText.text = Score.ToString();
        ScoreScreenHighscoreText.text = PlayerPrefs.GetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games").ToString();

    }
}
