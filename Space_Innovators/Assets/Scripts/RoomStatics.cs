using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomStatics : MonoBehaviour
{
    [SerializeField] Sprite Icon;
    public string[] resourcesNames;
    public int[] resourcesQuanity;

    public int produce_at;
    public int roomLevel;
    public string[] planets;
    public string[] rooms;

    public string[] producedResources;
    public int[] producedResourcesQuantity;

    private GameObject mario;
    private int work = 0;

    void Start(){
        mario = GameObject.Find("marioIdle");
    }

    public bool CheckRequirements(){
        mario = GameObject.Find("marioIdle");
        int SatisfiedRequirements = 0;
        for(int i=0; i<resourcesNames.Length; i++){
            if(mario.GetComponent<ResourcesClass>().resources[resourcesNames[i]] >= resourcesQuanity[i]) SatisfiedRequirements++;
        }
        if(SatisfiedRequirements < resourcesNames.Length) return false;

        SatisfiedRequirements = 0;
        foreach(string i in mario.GetComponent<BuildRegulator>().GetBuiltRooms()){
            foreach(string j in rooms){
                if(i==j) SatisfiedRequirements++;
            }
        }
        if(SatisfiedRequirements < rooms.Length) return false;

        SatisfiedRequirements = 0;
        foreach(string i in mario.GetComponent<BuildRegulator>().unlockedPlanets){
            foreach(string j in planets){
                if(i==j) SatisfiedRequirements++;
            }
        }
        if(SatisfiedRequirements < planets.Length) return false;

        return true;
    }

    public void Produce(){
        work++;
        Debug.Log(gameObject.name);
        if(work>=produce_at * (1/roomLevel)){
            if(gameObject.name == "Lab"){
                GameObject room = gameObject.GetComponent<LabProduce>().UnlockRoom();
                if(room!=null){
                    GameObject.Find("marioIdle").gameObject.GetComponent<BuildRegulator>().unlockedRooms.Add(room);
                    GameObject.Find("BuildButton").gameObject.GetComponent<BuildScrolScript>().UpdateBuildOptions();
                }
            }else if(gameObject.name == "Communications"){
                gameObject.GetComponent<CommProduce>().RecieveRandomResources();
            }else{
                for(int i=0; i<producedResources.Length; i++){
                    GameObject.Find("marioIdle").gameObject.GetComponent<ResourcesClass>().AddResource(producedResources[i], producedResourcesQuantity[i]);
                }
            }
            work=0;
        }
    }

    public string GetReqirementsByIndex(int index){
        if(index<resourcesNames.Length)return resourcesNames[index];
        return "";
    }

    public string GetRoomsByIndex(int index){
        if(index<rooms.Length)return rooms[index];
        return "";
    }

    public string GetPlanetsByIndex(int index){
        if(index<planets.Length)return planets[index];
        return "";
    }

    public int CheckReqirementsQuantityByIndex(int index){
        if(index<resourcesNames.Length)return  resourcesQuanity[index];
        return 0;
    }

    public string GetReqirementsQuantityByIndex(int index){
        if(index<resourcesNames.Length)return  " x"+resourcesQuanity[index];
        return "";
    }

    public Sprite GetIcon(){
        return Icon;
    }

    public static string GetDescription(string roomIndex){
        if(roomIndex == "MainRoom"){//MainRoom
            return "Provides beds for the crew";

        }else if(roomIndex == "O2"){//O2
            return "Produces oxygen based on level";

        }else if(roomIndex == "Communications"){//Communications
            return "Allows communication with planets";

        }else if(roomIndex == "Lab"){//Lab
            return "Unlock new rooms and upgrades through progress";

        }else if(roomIndex == "Storage"){//Storage
            return "Allows you to store more materials";

        }else if(roomIndex == "Water Station"){//WaterSatiton
            return "Produces water based on level";

        }else if(roomIndex == "VerticalFarm"){//VerticalFarm
            return "Produces food based on level";

        }
        return "No description";
    }
}
