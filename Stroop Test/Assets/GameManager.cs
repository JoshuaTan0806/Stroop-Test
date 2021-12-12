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

    public Canvas MainMenuCanvas;
    public Canvas OptionsCanvas;
    public Canvas ScoreCanvas;
    public Canvas LoseCanvas;

    public TMP_Dropdown NumberOfColoursDropdown;
    public int NumberOfColours;

    public int Score;
    public int Highscore;

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
        Highscore = PlayerPrefs.GetInt("Highscore");
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
        SetActiveCanvas(OptionsCanvas, false);
    }
}