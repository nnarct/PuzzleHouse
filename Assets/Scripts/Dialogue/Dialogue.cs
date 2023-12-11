using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string NpcName; //name of NPC

    // makes the text field for sentences multiline with a minimum of 3 and maximum of 10 lines
    [TextArea(3,10)]
    public string[] Sentences; //array of sentences in dialogue lines

}
