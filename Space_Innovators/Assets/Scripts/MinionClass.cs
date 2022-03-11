using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion
{
    public string name = "None";
    public string[] tags;
    public int energy = 100;
    public int sickness = 0;
    public int happiness = 100;

    public Minion(string name, string[] tags, int energy, int sickness, int happiness){
        this.name = name;
        for(int i=0;i<tags.length;i++){
            this.tags[i] = tags[i];
        }
        this.energy = energy;
        this.sickness = sickness;
        this.happiness = happiness;
    }
}
