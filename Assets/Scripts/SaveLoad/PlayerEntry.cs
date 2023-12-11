using System;
using System.Collections.Generic;

[Serializable]
public class PlayerEntry 
{
    public string PlayerName;

    public string CharacterChoose;

    public int Level;

    public Stage1 Stage1;

    public Stage2 Stage2;

    public PlayerEntry(string name, int level, string characterChoose)
    {
        PlayerName = name;
        CharacterChoose = characterChoose;
        this.Level = level;
        Stage1 = new Stage1(0, 0, 0, 0, 0);
        Stage2 = new Stage2(0, 0, 0, 0, 0);
        
    }
}

[Serializable]
public class Stage1
{
    public int Wooden;
    public int Genetic;
    public int Time;
    public int Earth;
    public int Moon;

    public Stage1(int wooden, int genetic, int time, int earth, int moon)
    {
        this.Wooden = wooden;
        this.Genetic = genetic;
        this.Time = time;
        this.Earth = earth;
        this.Moon = moon;
    }
}

[Serializable]
public class Stage2
{
    public int Christmas;
    public int Santa;
    public int Pipe;
    public int Wire;
    public int Kitchenware;

    public Stage2(int christmas, int santa, int pipe, int wire, int kitchenware)
    {
        this.Christmas = christmas;
        this.Santa = santa;
        this.Pipe = pipe;
        this.Wire = wire;
        this.Kitchenware = kitchenware;
    }
}
