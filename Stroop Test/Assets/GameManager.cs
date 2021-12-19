using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //current state of the game
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

    //options main menu canvas
    public Canvas MainMenuCanvas;

    [Header("Options")]

    //options canvas
    public Canvas OptionsCanvas;

    //the dropdown number of colours in options canvas
    public TMP_Dropdown NumberOfColoursDropdown;

    //number of colours chosen in the dropdown
    [HideInInspector] public int NumberOfColours;

    //the dropdown number of games in options canvas
    public TMP_Dropdown NumberOfGamesDropdown;

    //number of games chosen in the dropdown
    [HideInInspector] public int NumberOfGames;

    //the dropdown of amount of time in options canvas
    public TMP_Dropdown AmountOfTimeDropdown;

    //time between each correct click before losing game
    [HideInInspector] public int StartTime;

    [Header("Game")]

    //all colours in the same order as the buttons
    public Colour[] Colours;

    //game canvas 1
    public Canvas[] GameCanvases;

    //the player's current score
    [HideInInspector] public int Score;

    [Header("Lose Screen")]

    //lose canvas
    public Canvas LoseCanvas;

    [Header("Score Screen")]

    //score canvas
    public Canvas ScoreCanvas;

    //the score text of the score screen canvas
    public TextMeshProUGUI ScoreScreenScoreText;

    //the highscore text of the score screen canvas
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
        //change the game state when the player clicks
        if(Input.GetMouseButtonDown(0))
        {
            SwitchGameState();
        }
    }

    /// <summary>
    /// switch the games state depending on its current state
    /// </summary>
    void SwitchGameState()
    {
        switch (CurrentGameState)
        {
            //in main menu state, change to option state
            case GameState.MainMenu:
                CurrentGameState = GameState.Options;
                SetActiveCanvas(OptionsCanvas);
                SetActiveCanvas(MainMenuCanvas, false);
                break;

                //in lose menu state, change to score menu state
            case GameState.LoseMenu:
                CurrentGameState = GameState.ScoreMenu;
                SetActiveCanvas(ScoreCanvas);
                SetActiveCanvas(LoseCanvas, false);
                break;

                //in score state, change to option state
            case GameState.ScoreMenu:
                CurrentGameState = GameState.MainMenu;
                SetActiveCanvas(MainMenuCanvas);
                SetActiveCanvas(ScoreCanvas, false);
                break;
        }
    }

    /// <summary>
    /// helper function to easily open or close canvases
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="setActive"></param>
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

    /// <summary>
    /// starts the current game
    /// </summary>
    public void StartGame()
    {
        //change the games state
        CurrentGameState = GameState.InGame;

        //the number of colours is the option chosen + 4
        NumberOfColours = NumberOfColoursDropdown.value + 4;

        //set the number of games
        NumberOfGames = NumberOfGamesDropdown.value;

        //disable the options canvas
        SetActiveCanvas(OptionsCanvas, false);

        //reset the score
        Score = 0;

        //change the start time depending on the option chosen
        if (AmountOfTimeDropdown.value == 0)
        {
            StartTime = 15;
        }
        else if (AmountOfTimeDropdown.value == 1)
        {
            StartTime = 10;
        }
        else
        {
            StartTime = 5;
        }

        //set a different canvas active depending on how many games were chosen
        SetActiveCanvas(GameCanvases[NumberOfGames]);
        GameCanvases[NumberOfGames].GetComponent<SubGameManager>().StartGame();
    }

    /// <summary>
    /// when the player loses
    /// </summary>
    public void LoseGame()
    {
        //if the score is larger than the current highscore of the number of colours and games
        if(Score > PlayerPrefs.GetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games{StartTime}time"))
        {
            //set the highscore to the current score
            PlayerPrefs.SetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games{StartTime}time", Score);
        }

        //change menu to the score
        CurrentGameState = GameState.ScoreMenu;

        //disable the current canvases
        for (int i = 0; i < GameCanvases.Length; i++)
        {
            SetActiveCanvas(GameCanvases[i], false);
        }

        //open the score canvas
        SetActiveCanvas(ScoreCanvas);
        
        //set the score and highscore text
        ScoreScreenScoreText.text = Score.ToString();
        ScoreScreenHighscoreText.text = PlayerPrefs.GetInt($"Highscore{NumberOfColours - 4}colours{NumberOfGames}games{StartTime}time").ToString();
    }
}
