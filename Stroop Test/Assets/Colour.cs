using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colour", menuName = "Colour")]
public class Colour : ScriptableObject
{
    public string Name;

    public byte R;
    public byte G;
    public byte B;
}
