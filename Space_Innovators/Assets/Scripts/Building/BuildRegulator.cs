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
    public int newX = 5;
    public int newY = 5;
    public float offset = 7.75f;
    Transform MainRoom;
    //GameObject[] startRooms;
    [SerializeField] public GameObject[] Crew;
    [SerializeField] public List<GameObject> unlockedRooms;
    [SerializeField] public List<string> unlockedPlanets;
    public int[] onBoardCount = new int[10];
    //[SerializeField] Animator[] Aimators;
    List<GameObject> stations;
    List<GameObject> beds;

    public void addCrewMate(int crewIndex){
        Vector3 objectPOS = Vector3.zero;
        //Debug.Log(GameObject.FindGameObjectsWithTag("Station"));
        GameObject newGameObject = Instantiate(Crew[crewIndex], objectPOS, Quaternion.identity);
        newGameObject.GetComponent<NpcAi>().target = GameObject.FindGameObjectsWithTag("Station")[0].transform	;
        GameObject.FindGameObjectsWithTag("Station")[0].transform.tag = "UsedStation";
        newGameObject.transform.GetComponent<NpcAi>().bed = GameObject.FindGameObjectsWithTag("Bed")[0].transform;
        GameObject.FindGameObjectsWithTag("Bed")[0].transform.tag = "UsedBed";
        newGameObject.transform.GetComponent<NPCStats>().camera = GameObject.Find("Main Camera").transform;
        onBoardCount[crewIndex]++;
    }

    public void buildRoom(int newX, int newY, int roomIndex){
        Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector3((float)(objectPOS.x+((newX-5f)*offset)), (float)(objectPOS.y+((newY-5f)*offset)), 0f);
        GameObject newGameObject = Instantiate(unlockedRooms[roomIndex], objectPOS, Quaternion.identity);
        map[newX,newY] = 1;
        if(map[newX+1,newY] != 1)map[newX+1,newY] = 2;
        if(map[newX-1,newY] != 1)map[newX-1,newY] = 2;
        if(map[newX,newY+1] != 1)map[newX,newY+1] = 2;
        if(map[newX,newY-1] != 1)map[newX,newY-1] = 2;
        AstarPath.active.Scan();
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

        Debug.Log(stations);

        addCrewMate(0);
        addCrewMate(0);
        //addCrewMate(0);
     
    }

}
