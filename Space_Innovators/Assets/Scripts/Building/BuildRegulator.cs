using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
Map Values:
    0 - Empty, but way from structures
    1 - Taken
    2 - Empty and close to structure
    3 - Damaged    
*/

public class BuildRegulator : MonoBehaviour
{
    
    static int gridSize = 21;
    static int center = (gridSize-1)/2;
    public int[,] map = new int[gridSize, gridSize];
    public GameObject[,] rooms = new GameObject[gridSize,gridSize];
    //int newX = center;
    //int newY = center;
    public float offset = 7.75f;
    Transform MainRoom;
    [SerializeField] GameObject Crewmate;
    //[SerializeField] public string[] Tags;
    [SerializeField] public List<GameObject> unlockedRooms;
    [SerializeField] private List<string> BuiltRooms;
    [SerializeField] public List<string> unlockedPlanets;
    public Dictionary<string,Sprite> planetIcons = new Dictionary<string,Sprite>();
    public Dictionary<string,int> onBoardCount = new Dictionary<string,int>();
    public Dictionary<string,List<string>> ProfessionsForPlanet = new Dictionary<string,List<string>>();
  
    public int GetCenter(){
        return center;
    }
    public int GetGridSize(){
        return gridSize;
    }

    public void UnclockPlanet(string PlanetName, List<string> AdditionaProfesions){
        unlockedPlanets.Add(PlanetName);
        onBoardCount.Add(PlanetName,0);
        planetIcons.Add(PlanetName, Resources.Load<Sprite>(PlanetName+"Icon"));
        ProfessionsForPlanet.Add(PlanetName, new List<string>());
        ProfessionsForPlanet[PlanetName].Add("Oxygen Manager");
        ProfessionsForPlanet[PlanetName].Add("Navigator");
        ProfessionsForPlanet[PlanetName].Add("Researcher");
        ProfessionsForPlanet[PlanetName].Add("Water Manager");
        ProfessionsForPlanet[PlanetName].Add("Botanist");
        ProfessionsForPlanet[PlanetName].Add("Mechanic");
        if(AdditionaProfesions!=null){
            foreach(string prof in AdditionaProfesions){
                ProfessionsForPlanet[PlanetName].Add(prof);
            }
        }
    }

    public void addCrewMate( string Planet, string Profession){
        Vector3 objectPOS = Vector3.zero;
        GameObject newGameObject = Instantiate(Crewmate, objectPOS, Quaternion.identity);
        //newGameObject.tag = Planet+"-"+Profession;
        newGameObject.GetComponent<NPCStats>().Planet = Planet;
        newGameObject.GetComponent<NPCStats>().Profession = Profession;
        newGameObject.GetComponent<NpcAi>().actualTarget = null;
        //GameObject.FindGameObjectsWithTag("Station")[targetIndex].transform.tag = "UsedStation";
        newGameObject.transform.GetComponent<NpcAi>().bed = GameObject.FindGameObjectsWithTag("Bed")[0].transform;
        GameObject.FindGameObjectsWithTag("Bed")[0].transform.tag = "UsedBed";
        onBoardCount[Planet]++;
    }

    public void buildRoom(int newX, int newY, GameObject room){
        Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector3((float)(objectPOS.x+((newX-center)*offset)), (float)(objectPOS.y+((newY-center)*offset)), 0f);
        GameObject newGameObject = Instantiate(room, objectPOS, Quaternion.identity);
        newGameObject.name = newGameObject.name.Split('(')[0];

        map[newX,newY] = 1;

        rooms[newX,newY] = newGameObject;
        if(newX!=gridSize-1){
            if(rooms[newX+1,newY] != null){
                newGameObject.transform.Find("SmallRoom").transform.Find("DoorRight").GetComponent<DoorsEdit>().OpenDoor();
                rooms[newX+1,newY].transform.Find("SmallRoom").transform.Find("DoorLeft").GetComponent<DoorsEdit>().OpenDoor();
            }
        }
        if(newX!=0){
            if(rooms[newX-1,newY] != null){
                newGameObject.transform.Find("SmallRoom").transform.Find("DoorLeft").GetComponent<DoorsEdit>().OpenDoor();
                rooms[newX-1,newY].transform.Find("SmallRoom").transform.Find("DoorRight").GetComponent<DoorsEdit>().OpenDoor();
            }
        }
        if(newY!=gridSize-1){
            if(rooms[newX,newY+1] != null){
                newGameObject.transform.Find("SmallRoom").transform.Find("DoorUp").GetComponent<DoorsEdit>().OpenDoor();
                rooms[newX,newY+1].transform.Find("SmallRoom").transform.Find("DoorDown").GetComponent<DoorsEdit>().OpenDoor();
            }
        }
        if(newY!=0){
            if(rooms[newX,newY-1] != null){
                newGameObject.transform.Find("SmallRoom").transform.Find("DoorDown").GetComponent<DoorsEdit>().OpenDoor();
                rooms[newX,newY-1].transform.Find("SmallRoom").transform.Find("DoorUp").GetComponent<DoorsEdit>().OpenDoor();
            }
        }

        if(newX!=gridSize-1){if(map[newX+1,newY] != 1) map[newX+1,newY] = 2;}

        if(newX!=0){if(map[newX-1,newY] != 1) map[newX-1,newY] = 2;}

        if(newY!=gridSize-1){if(map[newX,newY+1] != 1) map[newX,newY+1] = 2;}

        if(newY!=0){if(map[newX,newY-1] != 1) map[newX,newY-1] = 2;}

        BuiltRooms.Add(newGameObject.name);
        for(int i=0; i < room.GetComponent<RoomStatics>().resourcesNames.Length; i++){
            string name = room.GetComponent<RoomStatics>().resourcesNames[i];
            int quantity = room.GetComponent<RoomStatics>().resourcesQuanity[i];
            gameObject.GetComponent<ResourcesClass>().SubtractResource(name, quantity);
        }

        if(room.name == "Lab"){
            unlockedRooms.Remove(room);
        }
        AstarPath.active.Scan();
    }

    public List<string> GetBuiltRooms(){
        return BuiltRooms;
    }

    void Start()
    {   
        
        
        UnclockPlanet("Earth", null);
        List<string> Floaromaspecific  = new List<string>();
        Floaromaspecific.Add("nqkva profesiq deto shte e samo za tam");
        UnclockPlanet("Floaroma", Floaromaspecific);
        buildRoom(center,center,unlockedRooms[0]);

        buildRoom(center-1,center,unlockedRooms[1]);

        buildRoom(center,center+1,unlockedRooms[2]);

        //addCrewMate("Earth","Oxygen Manager");
        //addCrewMate("Earth","Navigator");
        //addCrewMate("Earth","Researcher");
     
    }

}
