using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState;
    public enum GameState
    {
        MainMenu,
        InGame,
        LoseMenu,
        ScoreMenu
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    CurrentGameState = GameState.InGame;
                    break;

                case GameState.LoseMenu:
                    CurrentGameState = GameState.ScoreMenu;
                    break;

                case GameState.ScoreMenu:
                    CurrentGameState = GameState.MainMenu;
                    break;
            }
        }
    }
}
