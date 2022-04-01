using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BuildRegulator : MonoBehaviour
{
    public int[,] map = new int[11, 11];
    public int newX = 5;
    public int newY = 5;
    float offset = 7.75f;
    Transform MainRoom;
    [SerializeField] GameObject[] startRooms;
    [SerializeField] GameObject[] Crew;
    //[SerializeField] Animator[] Aimators;
    List<GameObject> stations;
    List<GameObject> beds;

    // Start is called before the first frame update
    public void UpdateInteractables(){
        //stations = GameObject.FindGameObjectsWithTag("Station");
        // /beds = GameObject.FindGameObjectsWithTag("Bed");

        /*stations = new List<GameObject>();
        beds = new List<GameObject>();
        foreach(GameObject gmob in GameObject.FindGameObjectsWithTag("Station")){
            stations.Add(gmob);
        }
        foreach(GameObject gmob in GameObject.FindGameObjectsWithTag("Bed")){
            beds.Add(gmob);
        }*/
    }
    // bugva zashtoto ne se promenqt tagovete
    // opciq za opravqne e da imam masiv s nachanite i sled tova shte se setvat manualy
    public void addCrewMate(int crewIndex){
        Vector3 objectPOS = Vector3.zero;
        Debug.Log(GameObject.FindGameObjectsWithTag("Station"));
        GameObject newGameObject = Instantiate(Crew[crewIndex], objectPOS, Quaternion.identity);
        newGameObject.GetComponent<NpcAi>().target = GameObject.FindGameObjectsWithTag("Station")[0].transform	;
        GameObject.FindGameObjectsWithTag("Station")[0].transform.tag = "UsedStation";
        newGameObject.transform.GetComponent<NpcAi>().bed = GameObject.FindGameObjectsWithTag("Bed")[0].transform;
        GameObject.FindGameObjectsWithTag("Bed")[0].transform.tag = "UsedBed";
        newGameObject.transform.GetComponent<NPCStats>().camera = GameObject.Find("Main Camera").transform;
    }

    public void buildRoom(int newX, int newY, int roomIndex){
        Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector3((float)(objectPOS.x+((newX-5f)*offset)), (float)(objectPOS.y+((newY-5f)*offset)), 0f);
        GameObject newGameObject = Instantiate(startRooms[roomIndex], objectPOS, Quaternion.identity);
        map[newX,newY] = 1;
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
     
    }

}
