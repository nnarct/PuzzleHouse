using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string NpcName;
    public string PlayerName;

    [TextArea(3,10)]
    public string[] Sentences;


}
