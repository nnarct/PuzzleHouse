using System;
using System.Collections.Generic;

[Serializable]
public class PlayerEntry 
{
    public string PlayerName;
    public int Level;
    public Stage1 stage1;
    public Stage2 stage2;

    public PlayerEntry(string name, int Level)
    {
        PlayerName = name;
        this.Level = Level;
        stage1 = new Stage1(0, 0, 0, 0, 0);
        stage2 = new Stage2(0, 0, 0, 0, 0);
    }
}


[Serializable]
public class Stage1
{
    public int wooden;
    public int genetic;
    public int time;
    public int earth;
    public int moon;

    public Stage1(int wooden, int genetic, int time, int earth, int moon)
    {
        this.wooden = wooden;
        this.genetic = genetic;
        this.time = time;
        this.earth = earth;
        this.moon = moon;
    }
}

[Serializable]
public class Stage2
{
    public int christmas;
    public int santa;
    public int pipe;
    public int wire;
    public int kitchenware;

    public Stage2(int christmas, int santa, int pipe, int wire, int kitchenware)
    {
        this.christmas = christmas;
        this.santa = santa;
        this.pipe = pipe;
        this.wire = wire;
        this.kitchenware = kitchenware;
    }
}
