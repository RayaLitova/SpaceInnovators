using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChageOptions : MonoBehaviour
{
    //public static Dictionary < string, Transform > Stations = new Dictionary < string, Transform > ();
    // Start is called before the first frame update
    public Transform list;
    ChangeTarget options;
    public NpcAi AI;
    public Dictionary < string, GameObject > Stations = new Dictionary < string, GameObject > ();
    private GetWorkNeeded stationscript;
    public NPCStats procces;

    public Transform target_value;
    public void SearchStations(){
        Stations = new Dictionary < string, GameObject > ();
            Stations.Add("None", AI.bed.gameObject);
            foreach(GameObject stObj in GameObject.FindGameObjectsWithTag("Station")) {
                stationscript = stObj.GetComponent<GetWorkNeeded>();
                
                Stations.Add(stationscript.name, stObj);
                
            }
    }

    void Start()
    {
        target_value = gameObject.transform;
        //procces = list.GetComponent<NPCStats>();
        //if(procces.found){
            SearchStations();
            //options = list.GetComponent<ChangeTarget>();
            AI = list.GetComponent<NpcAi>();
            var dropdown = transform.GetComponent<Dropdown>();
            dropdown.options.Clear(); 
            foreach(var item in Stations){
                dropdown.options.Add(new Dropdown.OptionData(){text=item.Key});
            }
            DropdownItemSelected(dropdown);
            dropdown.onValueChanged.AddListener(delegate{DropdownItemSelected(dropdown);});
            //AI.target = Stations["None"].transform;
        //}
        //DropdownItemSelected(dropdown);
    }

    public void DropdownItemSelected(Dropdown dropdown){
        int index = dropdown.value;
        print(index);
        print(AI.target);
        target_value = Stations[dropdown.options[index].text].transform;
        //return Stations[dropdown.options[index].text].transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
}