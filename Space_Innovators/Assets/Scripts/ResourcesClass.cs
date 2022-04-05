using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResourcesClass : MonoBehaviour
{
    public Dictionary<string, int> resources = new Dictionary<string, int>();
    public Dictionary<string, int> resourcesMax = new Dictionary<string, int>();
    public Dictionary<string, string> icons = new Dictionary<string, string>();

    public Dictionary<string, int> nonRecievableResources = new Dictionary<string, int>();
    public Dictionary<string, int[]> exceptions = new Dictionary<string, int[]>();

    private int rnd;
    private System.Random random = new System.Random();
    private int nonGatherableResources = 5;
    

    void Start(){
        nonRecievableResources.Add("Earth", 1);
        exceptions.Add("Earth", new int[]{3,5});

        resources.Add("Black metal", 17);
        resources.Add("Colored metal", 10);
        resources.Add("Fuel", 0);
        resources.Add("Space rock", 5);
        resources.Add("Water", 10);
        resources.Add("Energy", 0);
        resources.Add("Food", 10);
        resources.Add("Medicine", 0);
        resources.Add("Cotton", 8);
        resources.Add("O2", 0);

        resourcesMax.Add("Black metal", 10);
        resourcesMax.Add("Colored metal", 10);
        resourcesMax.Add("Fuel", 10);
        resourcesMax.Add("Space rock", 10);
        resourcesMax.Add("Water", 50);
        resourcesMax.Add("Energy", 0);
        resourcesMax.Add("Food", 50);
        resourcesMax.Add("Medicine", 10);
        resourcesMax.Add("O2", 50);
        resourcesMax.Add("Cotton", 10);

        icons.Add("Black metal", "build_metal_symbol");
        icons.Add("Colored metal", "color_metal_symbol_alt");
        icons.Add("Fuel", "fuel_symbol_alt");
        icons.Add("Space rock", "space_rock_symbol");
        icons.Add("Water", "water_symbol");
        icons.Add("Energy", "ENERGY_HUMAN");
        icons.Add("Food", "food_symbol");
        icons.Add("Medicine", "MEDKIT");
        icons.Add("Cotton", "cotton");
        icons.Add("O2", "o22");
        
        icons.Add("", "empty_symbol");

    }
    
    public void AddResource(string name, int quantity){
        resources[name] += quantity;
        if(resources[name]>resourcesMax[name]){
            resources[name]=resourcesMax[name];
        }
        Debug.Log(name+": "+resources[name]);
    }

    public bool SubtractResource(string name, int quantity){
        if(resources[name]<quantity){
            return false;
        }
        resources[name] -= quantity;
        Debug.Log(name+": "+resources[name]);
        return true;
    }

    public string AddRandomResource(int min, int max, Text text = null, string planet = null){
        if(text==null){
            bool flag = false;
            do{
                flag = false;
                rnd = random.Next(resources.Count - nonRecievableResources[planet]);
                foreach(int i in exceptions[planet]){
                    if(rnd == i) flag = true;
                }
            }while(flag);
        }else
            rnd = random.Next(resources.Count - nonGatherableResources);

        string name = resources.ElementAt(rnd).Key;
        int quantity = random.Next(min, max+1);
        resources[name] += quantity;
        if(resources[name]>resourcesMax[name]){
            resources[name]=resourcesMax[name];
        }
        if(text!=null){
            text.gameObject.SetActive(true);
            text.text = quantity.ToString() + "x "+ name;
            text.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(icons[name]);
        }
        return (name + " x" + quantity);
    }

    public void SubtractRandomResource(int min, int max){
        rnd = random.Next(resources.Count);
        string name = resources.ElementAt(rnd).Key;
        resources[name] -= random.Next(min, max+1);
        if(resources[name]<0){
            resources[name]=0;
        }
        Debug.Log(name+": " + resources[name]);
    }

    public void PrintResources(){
        for(int i=0; i<resources.Count;i++){
            Debug.Log(resources.ElementAt(i).Key+": "+resources.ElementAt(i).Value);
        }
    }

    public int CheckResource(string Name){
        if(Name=="")return 0;
        return resources[Name];
    }

    void Update(){
        int maxValue=10;
        int OxygenValue=50;
        int WaterValue=40;
        int FoodValue=30;
        foreach(string room in transform.GetComponent<BuildRegulator>().GetBuiltRooms()){
            if(room == "Storage"){
                maxValue+=20;
            }
            if(room == "BigStorage"){
                maxValue+=40;
            }
            if(room == "O2Storage"){
                OxygenValue += 100;
            }
            if(room == "WaterStorage"){
                WaterValue += 80;
            }
            if(room == "FoodStorage"){
                FoodValue += 60;
            }
        }
        foreach(KeyValuePair<string,int> item in resourcesMax){
            resourcesMax[item.Key] = maxValue;
        }
        resourcesMax["O2"] = OxygenValue;
        resourcesMax["Water"] = WaterValue;
        resourcesMax["Food"] = FoodValue;

    }

}
