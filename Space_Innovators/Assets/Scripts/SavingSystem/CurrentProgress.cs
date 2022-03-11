using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class CurrentProgress
{
    public Recipe[] recipes;
    public Resource[] resources;
    public Minion[] minions;
    public int daysPassed = 0;
    public Room[] rooms;

    public CurrentProgress(CurrentProgressSerialized c){
        for(int i=0;i<c.recipes.length;i++){
            recipes[i].name = c.recipes[i].name;
            recipes[i].description = c.recipes[i].description;
            for(int j=0;j<c.recipes[i].required_resources.length;j++){
                recipes[i].required_resources[j].name = c.recipes[i].required_resources[j].name;
                recipes[i].required_resources[j].icon_name = c.recipes[i].required_resources[j].icon_name;
                recipes[i].required_resources[j].quantity = c.recipes[i].required_resources[j].quantity;
            }
            recipes[i].isUnlocked = Convert.ToBoolean(c.recipes[i].isUnlocked);
        }

        for(int i=0;i<c.resources.length;i++){
            resources[i].name = c.resources[i].name;
            resources[i].icon_name = c.resources[i].icon_name;
            resources[i].quantity = c.resources[i].quantity;
        }

        for(int i=0;i<c.minions.length;i++){
            minions[i].name = c.minions[i].name;
            for(int j=0;j<c.minions[i].tags.length;j++){
                minions[i].tags[j]=c.minions[i].tags[j];
            }
            minions[i].energy = c.minions[i].energy;
            minions[i].sickness = c.minions[i].sickness;
            minions[i].happiness = c.minions[i].happiness;
        }

        daysPassed = c.daysPassed;

        for(int i=0;i<c.rooms.length;i++){
            rooms[i].name = c.rooms[i].name;
            rooms[i].workDone = c.rooms[i].workDone;
            rooms[i].level = c.rooms[i].level;
        }
    }

    public CurrentProgress(){
        //-----------------------RECIPES---------------------------------------------
        recipes[0].name = "Laboratory";
        recipes[0].description = "The laboratory is a room in which are discovered new recipes that become available for creation.";
        recipes[0].required_resources[0] = new Resource("Black metal", "null", 3);
        recipes[0].required_resources[1] = new Resource("Space rock", "null", 1);
        recipes[0].required_resources[2] = new Resource("Fuel", "null", 2);
        recipes[0].isUnlocked = true;

        recipes[1].name = "Navigation";
        recipes[1].description = "From Navigation you can communicate with the Earth and other planets and look at a map all planets from which you can gather resources.";
        recipes[1].required_resources[0] = new Resource();
        recipes[1].isUnlocked = true;

        recipes[2].name = "Storage";
        recipes[2].description = "The storage is where you can store all your resources.";
        recipes[2].required_resources[0] = new Resource();
        recipes[2].isUnlocked = true;

        recipes[3].name = "Oxygen";
        recipes[3].description = "The oxygen room supplies your station with oxygen. You have to refill the oxygen tanks before they run out.";
        recipes[3].required_resources[0] = new Resource();
        recipes[3].isUnlocked = true;

        recipes[4].name = "Community";
        recipes[4].description = "In the community room your crewmates can rest once they have finished their tasks.";
        recipes[4].required_resources[0] = new Resource();
        recipes[4].isUnlocked = true;

        recipes[5].name = "Shuttle";
        recipes[5].description = "From Shuttle room you can access your shuttle and travel to other planets to gather resources.";
        recipes[5].required_resources[0] = new Resource();
        recipes[5].isUnlocked = true;

        //----------------------RESOURCES------------------------------------

        resources[0]
    }

    public void ApplyProgress(){
        
    }
}

[System.Serializable]
public class CurrentProgressSerialized
{
    public CurrentProgressSerialized(CurrentProgress c){
       
    }
}