using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGameManager : MonoBehaviour
{
    public ColourText[] ColourTexts;

    //initialises the colours of the game belonging to this canvas
    public void StartGame()
    {
        for (int i = 0; i < ColourTexts.Length; i++)
        {
            ColourTexts[i].StartGame();
        }
    }
}
