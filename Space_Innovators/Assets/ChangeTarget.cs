using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    [SerializeField]private NpcAi NPC;
    public RaycastHit hit;
    public Ray ray;
    public List<GameObject> stationList = new List<GameObject>();
    public GameObject dropdown;
    public static Dictionary < string, GameObject > Stations = new Dictionary < string, GameObject > ();
    public GetWorkNeeded stationscript;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CheckPossibleStations(){
        dropdown.SetActive(true);
        Stations = new Dictionary < string, GameObject > ();
        foreach(GameObject stObj in GameObject.FindGameObjectsWithTag("Station")) {
            stationscript = stObj.GetComponent<GetWorkNeeded>();
            Stations.Add(stationscript.name, stObj);
        }
        //foreach(GameObject fooObj in Stations) {
 
            //print(Stations);
         //}
        
    
        /*if(!Input.GetMouseButtonDown(0))    // for standalone
         {  ray = Camera.main.ScreenPointToRay (Input.mousePosition);
             if (Physics.Raycast (ray, out hit, 100)) 
              {
                     if(hit.collider.gameObject.tag == "Station")
                      {
                          print("-------------------------------------------------");
                         NPC.target = hit.transform;
                     }
                     else{
                         print("huiiiiiii");
                     }
            }
         }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
