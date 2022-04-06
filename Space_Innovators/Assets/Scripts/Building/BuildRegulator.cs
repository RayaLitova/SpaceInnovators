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
    public int[,] map = new int[11, 11];
    public GameObject[,] rooms = new GameObject[11,11];
    public int newX = 5;
    public int newY = 5;
    public float offset = 7.75f;
    Transform MainRoom;
    [SerializeField] GameObject Crewmate;
    [SerializeField] public string[] Tags;
    [SerializeField] public List<GameObject> unlockedRooms;
    [SerializeField] private List<string> BuiltRooms;
    [SerializeField] public List<string> unlockedPlanets;
    public int[] onBoardCount = new int[10];
  

    public void addCrewMate(int crewIndex, int targetIndex){
        Vector3 objectPOS = Vector3.zero;
        GameObject newGameObject = Instantiate(Crewmate, objectPOS, Quaternion.identity);
        newGameObject.tag = Tags[crewIndex];
        newGameObject.GetComponent<NpcAi>().target = GameObject.FindGameObjectsWithTag("Station")[targetIndex].transform	;
        GameObject.FindGameObjectsWithTag("Station")[targetIndex].transform.tag = "UsedStation";
        newGameObject.transform.GetComponent<NpcAi>().bed = GameObject.FindGameObjectsWithTag("Bed")[0].transform;
        GameObject.FindGameObjectsWithTag("Bed")[0].transform.tag = "UsedBed";
        onBoardCount[crewIndex]++;
    }

    public void buildRoom(int newX, int newY, int roomIndex){
        Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector3((float)(objectPOS.x+((newX-5f)*offset)), (float)(objectPOS.y+((newY-5f)*offset)), 0f);
        GameObject newGameObject = Instantiate(unlockedRooms[roomIndex], objectPOS, Quaternion.identity);
        map[newX,newY] = 1;
        rooms[newX,newY] = newGameObject;

        if(rooms[newX+1,newY] != null){
            newGameObject.transform.Find("SmallRoom").transform.Find("DoorRight").GetComponent<DoorsEdit>().OpenDoor();
            rooms[newX+1,newY].transform.Find("SmallRoom").transform.Find("DoorLeft").GetComponent<DoorsEdit>().OpenDoor();
        }
        
        if(rooms[newX-1,newY] != null){
            newGameObject.transform.Find("SmallRoom").transform.Find("DoorLeft").GetComponent<DoorsEdit>().OpenDoor();
            rooms[newX-1,newY].transform.Find("SmallRoom").transform.Find("DoorRight").GetComponent<DoorsEdit>().OpenDoor();
        }
        if(rooms[newX,newY+1] != null){
            newGameObject.transform.Find("SmallRoom").transform.Find("DoorUp").GetComponent<DoorsEdit>().OpenDoor();
            rooms[newX,newY+1].transform.Find("SmallRoom").transform.Find("DoorDown").GetComponent<DoorsEdit>().OpenDoor();
        }
        if(rooms[newX,newY-1] != null){
            newGameObject.transform.Find("SmallRoom").transform.Find("DoorDown").GetComponent<DoorsEdit>().OpenDoor();
            rooms[newX,newY-1].transform.Find("SmallRoom").transform.Find("DoorUp").GetComponent<DoorsEdit>().OpenDoor();
        }

        if(map[newX+1,newY] != 1) map[newX+1,newY] = 2;
        if(map[newX-1,newY] != 1) map[newX-1,newY] = 2;
        if(map[newX,newY+1] != 1) map[newX,newY+1] = 2;
        if(map[newX,newY-1] != 1) map[newX,newY-1] = 2;

        BuiltRooms.Add(newGameObject.name.Split('(')[0]);

        for(int i=0; i < unlockedRooms[roomIndex].GetComponent<RoomStatics>().resourcesNames.Length; i++){
            string name = unlockedRooms[roomIndex].GetComponent<RoomStatics>().resourcesNames[i];
            int quantity = unlockedRooms[roomIndex].GetComponent<RoomStatics>().resourcesQuanity[i];
            gameObject.GetComponent<ResourcesClass>().SubtractResource(name, quantity);
        }

        if(unlockedRooms[roomIndex].name == "Lab"){
            unlockedRooms.RemoveAt(roomIndex);
        }
        AstarPath.active.Scan();
    }

    public List<string> GetBuiltRooms(){
        return BuiltRooms;
    }

    void Start()
    {
        buildRoom(newX,newY,0);

        newX = 4;
        newY = 5;
        buildRoom(newX,newY,1);

        newX = 5;
        newY = 6;
        buildRoom(newX,newY,2);

        addCrewMate(0,0);
        addCrewMate(0,0);
     
    }

}
