using System;
using System.Collections.Generic;

// PlayerEntry class represents a player data that save to file
[Serializable]
public class PlayerEntry 
{
    public string PlayerName; // player name

    public string CharacterChoose; // Character name chosen by the player

    public int Level; // Current level or scene the player is in

    public Stage1 Stage1; // Progress in Stage1

    public Stage2 Stage2; // Progress in Stage2

    // Constructor for creating a new player entry with initial values for stages
    public PlayerEntry(string name, int level, string characterChoose)
    {
        PlayerName = name;
        CharacterChoose = characterChoose;
        this.Level = level;
        Stage1 = new Stage1(0, 0, 0, 0, 0);
        Stage2 = new Stage2(0, 0, 0, 0, 0);
        
    }
}

// Stage1 class represents progress details for Stage 1
[Serializable]
public class Stage1
{
    // Progress details for different puzzle in Stage 1
    public int Wooden; 
    public int Genetic;
    public int Time;
    public int Earth;
    public int Moon;

    // Constructor for initializing Stage1 progress
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
    // Progress details for different puzzle in Stage 2
    public int Christmas;
    public int Santa;
    public int Pipe;
    public int Wire;
    public int Kitchenware;

    // Constructor for initializing Stage2 progress
    public Stage2(int christmas, int santa, int pipe, int wire, int kitchenware)
    {
        this.Christmas = christmas;
        this.Santa = santa;
        this.Pipe = pipe;
        this.Wire = wire;
        this.Kitchenware = kitchenware;
    }
}
