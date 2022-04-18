using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public string status;

    SpriteRenderer img;
    public GameObject[] pilots;
    ResourcesClass resources;
    BuildRegulator BL;
    [SerializeField]RoomStatics shuttleRooom;
    string[] ressText = {"Fuel", "Food", "Water", "O2"};
    public int[] maxes  = new int[4];
    public int[] currs  = new int[4];
    public int MetalNeeded;
    public int ColorNeeded;
    bool Discovered = false;
    private System.Random random = new System.Random();
    string planet = "";
    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetComponent<SpriteRenderer>();
        resources = GameObject.Find("marioIdle").GetComponent<ResourcesClass>();
        BL = GameObject.Find("marioIdle").GetComponent<BuildRegulator>();
    }

    public void Restock(){
        for(int i=0; i<4; i++){
            int resourceNeeded = maxes[i] - currs[i];
            if(resourceNeeded > resources.CheckResource(ressText[i])){ //fills the rocket with the needed recources for expedition
                resourceNeeded = resources.CheckResource(ressText[i]); // if there are not enough resources restocks with al of the current resources
            }
            resources.SubtractResource(ressText[i], resourceNeeded);
            currs[i] += resourceNeeded;
        }
        
    }

    public bool ReBuild(){ 
        if(MetalNeeded > resources.CheckResource("Black metal")){ // checks for resources
            return false;
        }
        if(ColorNeeded > resources.CheckResource("Colored metal")){
            return false;
        }
        if(status == "Not Built"){
            resources.SubtractResource("Black metal", MetalNeeded);
            resources.SubtractResource("Colored metal", ColorNeeded); //builds and subracts resources
            status = "On Station";
            return true;
        }
        return false;
        
    }

    public int StartExpedition(){
        if(pilots[0]==null || pilots[1]==null){ //cnacel start if there are not enough pilots
            return 0;
        }
        if(currs[0] == maxes[0] && currs[1] == maxes[1] && currs[2] == maxes[2] && currs[3] == maxes[3]){ //check resources needed to startt expedition
            
            status = "On Expedition";
            for(int i = 1; i<4; i++){ //sets recources 0 to set up for the next expedition
                currs[i] = 0;
            }
            currs[0] *= 50; //multipliews fuel so that the fixed updates subtract aproximateli 1 fuel per second
            return 2;
        }
        return 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        maxes[0] = shuttleRooom.roomLevel * 50; //50 //Fuel
        maxes[1] = shuttleRooom.roomLevel * 10 * 2; //10 //Food
        maxes[2] = shuttleRooom.roomLevel * 20 * 2; //20 //Water
        maxes[3] = shuttleRooom.roomLevel * 30 * 2; //30 //O2
        MetalNeeded = 30 + 5 * shuttleRooom.roomLevel; //30 + 5 //black metal needed for rocked building
        ColorNeeded = 10 + 1 * shuttleRooom.roomLevel; // 10 + 1 //colored metal needed for rocked building

        if(status == "On Station"){ //when rocket is on station 
            img.color = new Color32(255,255,255,255); //enables image to represent present shuttle
            if(pilots[0]!=null && pilots[1]!=null){
                foreach(GameObject pilot in pilots){
                    pilot.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255); //enables pilots
                    pilot.transform.GetComponent<NpcAi>().enabled = true;
                }
            }
            
        }else if(status == "On Expedition"){
            img.color = new Color32(255,255,255,0);
            foreach(GameObject pilot in pilots){
                pilot.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0); //disables pilots
                pilot.transform.GetComponent<NpcAi>().enabled = false;
                //eventualno izkluchi cukaneto
            }
            
            if(currs[0]==0){ //Fuel == 0
                if(random.Next(1,100*shuttleRooom.roomLevel)<=20){ //check the chances for destruction during expedition
                    currs[0]=0;
                    status ="Destroyed";
                    Discovered = false;
                }
                if(Discovered){
                    BL.UnclockPlanet(planet, null); //if planet is discovered during expedition ulocks the planet
                } 
                status = "On Station"; 
                Discovered = false;
            } 
            if(currs[0]>0){ //Fuel > 0
                if(!Discovered){
                    if(random.Next(1,100/shuttleRooom.roomLevel)==1){ //if not yet discovered check the chancess to discover
                        planet = BL.PlanetsToUnlock[random.Next(0,BL.PlanetsToUnlock.Count-1)]; //discover  //make it randomized random.Next(0,BL.PlanetsToUnlock.Count-1) vmesto 0 but its not working
                        Discovered = true;
                    }
                }
                currs[0]--; //Fuel --
                
            }
            

        }else if(status == "Destroyed"){
            transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0); //if destroyed  kill both pilots
            pilots[0].GetComponent<NpcAi>().Die();
            pilots[1].GetComponent<NpcAi>().Die();
            status = "Not Built";

        }else if(status == "Not Built"){
            transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0); //set the shuttle image not active to represent missing shuttle
        }
    }
}
