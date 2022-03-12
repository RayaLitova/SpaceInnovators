using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public string name = "None";
    public string description = "None";
    public Resource[] required_resources = new Resource[10];
    public bool isUnlocked = false;

    public Recipe(){
        this.name = "a";
        this.description = "None";
        this.isUnlocked = false;
        for(int i=0;i<10;i++){
            this.required_resources[i] = new Resource("None", "None", 0);
        }
    }

    public Recipe(string name, string description, Resource[] required_resources, bool isUnlocked){
        this.name = name;
        this.description = description;
        this.isUnlocked = isUnlocked;
        for(int i=0;i<required_resources.Length;i++){
            this.required_resources[i] = required_resources[i];
        }
    }
}
