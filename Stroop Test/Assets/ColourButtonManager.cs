using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColourButtonManager : MonoBehaviour
{
    public Button[] Buttons;

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
  
}
