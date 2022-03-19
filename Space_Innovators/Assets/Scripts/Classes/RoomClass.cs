 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string name = "None";
    public int workDone = 0;
    public int level = 1;

    public Room(string name, int workDone, int level){
        this.name = name;
        this.workDone = workDone;
        this.level = level;
    }
}
