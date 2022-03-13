using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BuildRegulator : MonoBehaviour
{
    public int[,] map = new int[11, 11];
    public int newX;
    public int newY;
    Transform MainRoom;
    [SerializeField] GameObject[] startRooms;
    [SerializeField] GameObject[] Crew;
    //[SerializeField] Animator[] Aimators;
    List<GameObject> stations;
    List<GameObject> beds;

    // Start is called before the first frame update
    public void UpdateInteractables(){
        stations = new List<GameObject>();
        beds = new List<GameObject>();
        foreach(GameObject gmob in GameObject.FindGameObjectsWithTag("Station")){
            stations.Add(gmob);
        }
        foreach(GameObject gmob in GameObject.FindGameObjectsWithTag("Bed")){
            beds.Add(gmob);
        }
    }

   /* void Start()
    {
        //GameObject testPrefab = new Comms();
        Vector3 objectPOS = Vector3.zero;
        GameObject newGameObject = Instantiate(startRooms[2], objectPOS, Quaternion.identity);
        newGameObject.SetActive(true);
        MainRoom = newGameObject.transform;
        map[5,5] = 1;
        //map[6,5] = 1;
        newX = 4;
        newY = 5;
        objectPOS = Vector3.zero;
        objectPOS = new Vector3((float)(objectPOS.x+((newX-5)*8)), (float)(objectPOS.y+((newY-5)*8.15)), 0f);
        newGameObject = Instantiate(startRooms[1], objectPOS, Quaternion.identity);
        map[newX,newY] = 1;
        newX = 5;
        newY = 6;
        objectPOS = Vector3.zero;
        objectPOS = new Vector3((float)(objectPOS.x+((newX-5)*8)), (float)(objectPOS.y+((newY-5)*8.15)), 0f);
        newGameObject = Instantiate(startRooms[0], objectPOS, Quaternion.identity);
        map[newX,newY] = 1;

        AstarPath.active.Scan();
        UpdateInteractables();
        Debug.Log(stations);
        objectPOS = new Vector3(MainRoom.transform.position.x, MainRoom.transform.position.y, 0f);
        newGameObject = Instantiate(Crew[0], objectPOS, Quaternion.identity);
        newGameObject.GetComponent<NpcAi>().target = stations[0].transform	;
        stations[0].tag = "UsedStation";
        newGameObject.transform.GetComponent<NpcAi>().bed = beds[0].transform;
        beds[0].tag = "UsedBed";
        UpdateInteractables();
        //Debug.Log(stations);
        objectPOS = new Vector3(MainRoom.transform.position.x, MainRoom.transform.position.y, 0f);
        newGameObject = Instantiate(Crew[0], objectPOS, Quaternion.identity);
        newGameObject.transform.GetComponent<NpcAi>().target = stations[0].transform;
        stations[0].tag = "UsedStation";
        newGameObject.transform.GetComponent<NpcAi>().bed = beds[0].transform;
        beds[0].tag = "UsedBed";
        UpdateInteractables();
    }*/

}
