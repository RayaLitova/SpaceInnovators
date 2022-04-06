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
    [SerializeField] public string[] Tags;
    [SerializeField] public List<GameObject> unlockedRooms;
    [SerializeField] private List<string> BuiltRooms;
    [SerializeField] public List<string> unlockedPlanets;
    public int[] onBoardCount = new int[10];
  
    public int GetCenter(){
        return center;
    }
    public int GetGridSize(){
        return gridSize;
    }

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
        objectPOS = new Vector3((float)(objectPOS.x+((newX-center)*offset)), (float)(objectPOS.y+((newY-center)*offset)), 0f);
        GameObject newGameObject = Instantiate(unlockedRooms[roomIndex], objectPOS, Quaternion.identity);
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
        buildRoom(center,center,0);

        //newX -= 1 ;
        //newY = 5;
        buildRoom(center-1,center,1);

        //newX += 1;
        //newY += 1;
        buildRoom(center,center+1,2);

        addCrewMate(0,0);
        addCrewMate(0,0);
     
    }

}
