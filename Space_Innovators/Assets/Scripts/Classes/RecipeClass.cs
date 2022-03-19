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

    public Serializables RecipeSerialize(){
        Serializables tmp = new Serializables();
        tmp.arr[0] = new SerializableClass();
        tmp.arr[0].arr[0] = this.name;
        tmp.arr[0].arr[1] = this.description;

        if(this.isUnlocked){
            tmp.arr[0].arr[2] = "1";
        }else{
            tmp.arr[0].arr[2] = "0";
        }

        for(int i=0;i<=required_resources.Length;i++){
            tmp.arr[1] = new SerializableClass();
            tmp.arr[1] = required_resources[i].ResourceSerialize();
        }
        
        return tmp;
    }
}
 