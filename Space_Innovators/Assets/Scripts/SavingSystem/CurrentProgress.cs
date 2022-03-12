using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[System.Serializable]
public class CurrentProgress
{
    public Recipe[] recipes = new Recipe[6];
    public Resource[] resources = new Resource[8];
    public Minion[] minions = new Minion[1];
    public int daysPassed = 0;
    public Room[] rooms = new Room[3];

    public CurrentProgress(CurrentProgressSerialized c){
        /*for(int i=0;i<c.recipes.Length;i++){
            recipes[i].name = c.recipes[i].name;
            recipes[i].description = c.recipes[i].description;
            for(int j=0;j<c.recipes[i].required_resources.Length;j++){
                recipes[i].required_resources[j].name = c.recipes[i].required_resources[j].name;
                recipes[i].required_resources[j].icon_name = c.recipes[i].required_resources[j].icon_name;
                recipes[i].required_resources[j].quantity = c.recipes[i].required_resources[j].quantity;
            }
            recipes[i].isUnlocked = Convert.ToBoolean(c.recipes[i].isUnlocked);
        }

        for(int i=0;i<c.resources.Length;i++){
            resources[i].name = c.resources[i].name;
            resources[i].icon_name = c.resources[i].icon_name;
            resources[i].quantity = c.resources[i].quantity;
        }

        for(int i=0;i<c.minions.Length;i++){
            minions[i].name = c.minions[i].name;
            for(int j=0;j<c.minions[i].tags.Length;j++){
                minions[i].tags[j]=c.minions[i].tags[j];
            }
            minions[i].energy = c.minions[i].energy;
            minions[i].sickness = c.minions[i].sickness;
            minions[i].happiness = c.minions[i].happiness;
        }

        daysPassed = c.daysPassed;

        for(int i=0;i<c.rooms.Length;i++){
            rooms[i].name = c.rooms[i].name;
            rooms[i].workDone = c.rooms[i].workDone;
            rooms[i].level = c.rooms[i].level;
        }*/
    }

    public CurrentProgress(){
        //-----------------------RECIPES---------------------------------------------
        recipes[0] = new Recipe();
        recipes[0].name = "Laboratory";
        recipes[0].description = "The laboratory is a room in which are discovered new recipes that become available for creation.";
        recipes[0].required_resources[0] = new Resource("Black metal", "null", 3);
        recipes[0].required_resources[1] = new Resource("Space rock", "null", 1);
        recipes[0].required_resources[2] = new Resource("Fuel", "null", 2);
        recipes[0].isUnlocked = true;

        recipes[1] = new Recipe();
        recipes[1].name = "Navigation";
        recipes[1].description = "From Navigation you can communicate with the Earth and other planets and look at a map all planets from which you can gather resources.";
        recipes[1].required_resources[0] = new Resource();
        recipes[1].isUnlocked = true;

        recipes[2] = new Recipe();
        recipes[2].name = "Storage";
        recipes[2].description = "The storage is where you can store all your resources.";
        recipes[2].required_resources[0] = new Resource();
        recipes[2].isUnlocked = true;

        recipes[3] = new Recipe();
        recipes[3].name = "Oxygen";
        recipes[3].description = "The oxygen room supplies your station with oxygen. You have to refill the oxygen tanks before they run out.";
        recipes[3].required_resources[0] = new Resource();
        recipes[3].isUnlocked = true;

        recipes[4] = new Recipe();
        recipes[4].name = "Community";
        recipes[4].description = "In the community room your crewmates can rest once they have finished their tasks.";
        recipes[4].required_resources[0] = new Resource();
        recipes[4].isUnlocked = true;

        recipes[5] = new Recipe();
        recipes[5].name = "Shuttle";
        recipes[5].description = "From Shuttle room you can access your shuttle and travel to other planets to gather resources.";
        recipes[5].required_resources[0] = new Resource();
        recipes[5].isUnlocked = true;

        //----------------------RESOURCES------------------------------------

        resources[0] = new Resource("Black metal", "build_metal_symbol.png", 0);
        resources[1] = new Resource("Colored metal", "color_metal_symbol_alt.png", 0);
        resources[2] = new Resource("Energy", "ENERGY_HUMAN.png", 0);
        resources[3] = new Resource("Food", "food_symbol.png", 0);
        resources[4] = new Resource("Fuel", "fuel_symbol_alt.png", 0);
        resources[5] = new Resource("Medicine", "MEDKIT.png", 0);
        resources[6] = new Resource("Space rock", "space_rock_symbol.png", 0);
        resources[7] = new Resource("Water", "water_symbol.png", 0);

        //---------------------------MINIONS---------------------------------------
        String[] tags_tmp = {"O2", "Food", "Water"};
        minions[0] = new Minion("Harry", tags_tmp, 100, 0, 100);

        //-------------------------Days Passed--------------------------------------
        daysPassed = 0;

        //-------------------------ROOMS--------------------------------

        rooms[0] = new Room("Community", 0, 1);
        rooms[1] = new Room("Oxygen", 0, 1);
        rooms[2] = new Room("Communications", 0, 1);
    }

    public void ApplyProgress(){
        
    }
}

[System.Serializable]
public class SerializableClass{
    public string[] arr = new string[100];
    public SerializableClass(){
        arr = new string[10];
    }
}

[System.Serializable]
public class Serializables{
    public SerializableClass[] arr = new SerializableClass[10];
    public Serializables(){
        arr = new SerializableClass[10];
    }
}

[System.Serializable]
public class CurrentProgressSerialized
{
    //public Recipe[] recipes = new Recipe[6];
    public Serializables[] recipes = new Serializables[6];
    public Resource[] resources = new Resource[8];
    public Minion[] minions = new Minion[1];
    public int daysPassed = 0;
    public Room[] rooms = new Room[3];
    
    public CurrentProgressSerialized(CurrentProgress c){
        for(int i=0;i<c.recipes.Length;i++){
            recipes[i] = new Serializables();
            recipes[i] = c.recipes[i].RecipeSerialize();
            //recipes[i] = new Recipe(c.recipes[i].name,c.recipes[i].description,c.recipes[i].required_resources,c.recipes[i].isUnlocked);
        }

        for(int i=0;i<c.resources.Length;i++){
            resources[i] = new Resource(c.resources[i].name, c.resources[i].icon_name, c.resources[i].quantity);
            
        }

        for(int i=0;i<c.minions.Length;i++){
            minions[i] = new Minion(c.minions[i].name, c.minions[i].tags, c.minions[i].energy, c.minions[i].sickness, c.minions[i].happiness);
        }

        daysPassed = c.daysPassed;

        for(int i=0;i<c.rooms.Length;i++){
            rooms[i] = new Room(c.rooms[i].name, c.rooms[i].workDone, c.rooms[i].level);
        }
    }
}