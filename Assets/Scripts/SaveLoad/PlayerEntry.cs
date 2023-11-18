using System;

[Serializable]
public class PlayerEntry 
{
    public string PlayerName;
    public int Level;

    public PlayerEntry(string name, int Level)
    {
        PlayerName = name;
        this.Level = Level;
    }
}
