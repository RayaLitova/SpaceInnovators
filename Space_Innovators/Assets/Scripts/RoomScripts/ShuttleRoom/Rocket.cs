using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public string status = "Destroyed";

    SpriteRenderer img;
    public GameObject[] pilots;
    ResourcesClass resources;
    BuildRegulator BL;
    [SerializeField]RoomStatics shuttleRooom;
    public int MetalNeeded;
    public int ColorNeeded;
    public int maxFuel;
    public int Fuel;
    public int maxFood;
    public int Food;
    public int maxWater;
    public int Water;
    public int maxO2;
    public int O2;
    bool Discovered = false;
    private System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetComponent<SpriteRenderer>();
        resources = GameObject.Find("marioIdle").GetComponent<ResourcesClass>();
        BL = GameObject.Find("marioIdle").GetComponent<BuildRegulator>();
    }

    public void Restock(){
        int resourceNeeded = maxFuel - Fuel;
        if(resourceNeeded < resources.CheckResource("Fuel")){
            resourceNeeded = resources.CheckResource("Fuel");
        }
        resources.SubtractResource("Fuel", resourceNeeded);
        Fuel += resourceNeeded;

        resourceNeeded = maxFood - Food;
        if(resourceNeeded < resources.CheckResource("Food")){
            resourceNeeded = resources.CheckResource("Food");
        }
        resources.SubtractResource("Food", resourceNeeded);
        Food += resourceNeeded;

        resourceNeeded = maxWater - Water;
        if(resourceNeeded < resources.CheckResource("Water")){
            resourceNeeded = resources.CheckResource("Water");
        }
        resources.SubtractResource("Water", resourceNeeded);
        Water += resourceNeeded;

        resourceNeeded = maxO2 -O2;
        if(resourceNeeded < resources.CheckResource("O2")){
            resourceNeeded = resources.CheckResource("O2");
        }
        resources.SubtractResource("O2", resourceNeeded);
        O2 += resourceNeeded;
        
    }

    public void ReBuild(){
        if(status == "Not Builded"){
            resources.SubtractResource("Black metal", MetalNeeded);
            resources.SubtractResource("Colored metal", ColorNeeded);
        }
        
    }

    public void StartExpedition(){
        if(pilots[0]==null || pilots[1]==null){
            return;
        }
        if(Fuel == maxFuel && Food == maxFood && Water == maxWater && O2 == maxO2){
            Food = 0;
            Water = 0;
            O2 = 0;
            status = "On Expedition";
            Fuel *= 50;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        maxFuel = shuttleRooom.roomLevel * 50;
        maxFood = shuttleRooom.roomLevel * 10 * 2;
        maxWater = shuttleRooom.roomLevel * 20 * 2;
        maxO2 = shuttleRooom.roomLevel * 30 * 2;
        MetalNeeded = 30 + 5 * shuttleRooom.roomLevel;
        ColorNeeded = 10 + 1 * shuttleRooom.roomLevel;

        if(status == "On Station"){
            img.color = new Color32(255,255,255,255);
            foreach(GameObject pilot in pilots){
                pilot.transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
                pilot.transform.GetComponent<NpcAi>().enabled = true;
            }

        }else if(status == "On Expedition"){
            img.color = new Color32(255,255,255,0);
            foreach(GameObject pilot in pilots){
                pilot.transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0);
                pilot.transform.GetComponent<NpcAi>().enabled = false;
            }
            string planet = "";
            if(Fuel==0){
                if(Discovered){
                    BL.UnclockPlanet(planet, null);
                } 
                status = "On Station";
                Discovered = false;
            } 
            if(Fuel>0){
                if(!Discovered){
                    if(random.Next(1,100/shuttleRooom.roomLevel)==1){
                        planet = BL.PlanetsToUnlock[random.Next(0,BL.PlanetsToUnlock.Count-1)];
                        Discovered = true;
                    }
                }
                Fuel --;
                if(random.Next(1,100*shuttleRooom.roomLevel)<=10){
                    status ="Destroyed";
                    Discovered = false;
                }
            }
            

        }else if(status == "Destroyed"){
            transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0);
            pilots[0].GetComponent<NpcAi>().Die();
            pilots[1].GetComponent<NpcAi>().Die();
            status = "Not Builded";

        }else if(status == "Not Builded"){
            transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0);
        }
    }
}
