using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColourChanger : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Colour[] Colours;

    public Colour WordColour;
    public Colour WordText;

    // Start is called before the first frame update
    void Start()
    {
        ChangeColour();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeColour();
        }
    }

    [ContextMenu("Change Colour")]
    public void ChangeColour()
    {
        WordColour = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];
        WordText = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];

        while(WordText == WordColour)
        {
            WordText = Colours[Random.Range(0, GameManager.instance.NumberOfColours)];
        }

        Text.text = WordText.Name;
        Text.color = new Color32(WordColour.R, WordColour.G, WordColour.B, 255);
    }
}
