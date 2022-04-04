using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    public int O2;
    public int Water;
    public int Food;
    public int max_energy;
    public int health=100;

    void Start(){
        int[] Stats = NPCStats.GetStatsByTag(transform.tag);
        O2 = Stats[0];
        Water = Stats[1];
        Food = Stats[2];
        max_energy = Stats[3];
    }

    public void Consume(string name, int quantity){
        if(!GameObject.Find("marioIdle").GetComponent<ResourcesClass>().SubtractResource(name,quantity)){
            health-=15;
            if(health<=0){
                gameObject.GetComponent<NpcAi>().Die();
            }
        }
    }

    public static int[] GetStatsByTag(string Tag){
        int[] Stats = new int[4];
        if(Tag == "Human"){
            Stats[0] = 1;
            Stats[1] = 1;
            Stats[2]= 1;
            Stats[3] = 100;
            
        }
        else if(Tag == "Floaromian"){//Neuforian
            Stats[0] = -1;
            Stats[1] = 3;
            Stats[2] = 0;
            Stats[3] = 150;
        }
        return Stats;
    }
}
