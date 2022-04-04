using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResourcesClass : MonoBehaviour
{
    public Dictionary<string, int> resources = new Dictionary<string, int>();
    public Dictionary<string, string> icons = new Dictionary<string, string>();
    private int rnd;
    private System.Random random = new System.Random();
    private int nonGatherableResources = 4;

    void Start(){
        resources.Add("Black metal", 0);
        resources.Add("Colored metal", 0);
        resources.Add("Fuel", 0);
        resources.Add("Space Rock", 0);
        resources.Add("Water", 0);
        resources.Add("Energy", 0);
        resources.Add("Food", 0);
        resources.Add("Medicine", 0);
        resources.Add("O2", 0);

        icons.Add("Black metal", "build_metal_symbol");
        icons.Add("Colored metal", "color_metal_symbol_alt");
        icons.Add("Fuel", "fuel_symbol_alt");
        icons.Add("Space Rock", "space_rock_symbol");
        icons.Add("Water", "water_symbol");
        icons.Add("Energy", "ENERGY_HUMAN");
        icons.Add("Food", "food_symbol");
        icons.Add("Medicine", "MEDKIT");
        icons.Add("O2", "MEDKIT");

    }
    
    public void AddResource(string name, int quantity){
        resources[name] += quantity;
        Debug.Log(name+": "+resources[name]);
    }

    public void SubstractResource(string name, int quantity){
        resources[name] -= quantity;
        Debug.Log(name+": "+resources[name]);
    }

    public void AddRandomResource(int min, int max, Text text = null){
        rnd = random.Next(resources.Count - 1 - nonGatherableResources);
        string name = resources.ElementAt(rnd).Key;
        int quantity = random.Next(min, max+1);
        resources[name] += quantity;
        if(text!=null){
            text.gameObject.SetActive(true);
            text.text = quantity.ToString() + "x "+ name;
            text.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(icons[name]);
        }
        Debug.Log(name+": "+resources[name]);
    }

    public void SubstractRandomResource(int min, int max){
        rnd = random.Next(resources.Count);
        string name = resources.ElementAt(rnd).Key;
        resources[name] -= random.Next(min, max+1);
        Debug.Log(name+": "+resources[name]);
    }

}
