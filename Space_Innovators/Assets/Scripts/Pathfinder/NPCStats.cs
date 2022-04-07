using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStats : MonoBehaviour
{
    public int O2;
    public int Water;
    public int Food;
    public int energy;
    public int max_energy;
    public int health;
    public int max_health;
    public string Planet;
    public string Profession;
    public string targetTag;
    [SerializeField] Slider[] Bars;
    [SerializeField] Text[] vals;
    [SerializeField] Text[] maxVals;

    void Start(){
        int[] Stats = NPCStats.GetStatsByTag(Planet);
        O2 = Stats[0];
        Water = Stats[1];
        Food = Stats[2];
        max_energy = Stats[3];
        max_health = Stats[4];
        SetMaxValue(0,max_health);
        SetMaxValue(1,max_energy);
        energy = max_energy;
        health = max_health;
        targetTag = NPCStats.GetTargetTagByTag(Profession);
        
    }

    public void Consume(string name, int quantity){
        if(!GameObject.Find("marioIdle").GetComponent<ResourcesClass>().SubtractResource(name,quantity)){
            health-=15;
            if(health<=0){
                gameObject.GetComponent<NpcAi>().Die();
            }
        }
    }

    public static string GetTargetTagByTag(string Tag){
        string targettag = "";
        if(Tag.Contains("Oxygen Manager")){
            targettag = "O2";           
        }
        else if(Tag.Contains("Navigator")){//Neuforian
            targettag = "Communications";
        }
        else if(Tag.Contains("Researcher")){//Neuforian
            targettag = "Lab";
        }
        else if(Tag.Contains("WaterManager")){//Neuforian
            targettag = "Water Station";
        }
        else if(Tag.Contains("Botanist")){//Neuforian
            targettag = "Vertival Farm";
        }
        else if(Tag.Contains("Mechanic")){//Neuforian
            targettag = "Upgrading";
        }
        return targettag;
    }

    public static int[] GetStatsByTag(string Tag){
        int[] Stats = new int[5];
        if(Tag.Contains("Earth")){
            Stats[0] = 1;
            Stats[1] = 1;
            Stats[2]= 1;
            Stats[3] = 100;
            Stats[4] = 100;
            
        }
        else if(Tag.Contains("Floaroma")){//Neuforian
            Stats[0] = -1;
            Stats[1] = 3;
            Stats[2] = 0;
            Stats[3] = 150;
            Stats[4] = 75;
        }
        return Stats;
    }

    public void SetValue(int barIndex, int value){
        Bars[barIndex].value = value; 
        vals[barIndex].text = value.ToString();
    }

    public void SetMaxValue( int barIndex, int value){
        Bars[barIndex].maxValue = value; 
        maxVals[barIndex].text = value.ToString();
    }

    void Update(){
        SetValue(0,health);
        SetValue(1,energy);
    }
}
