using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionScript : MonoBehaviour
{
    // Start is called before the first frame update
    BuildRegulator BL;
    ResourcesClass resources;
    [SerializeField]GameObject ResourceButton;
    Rocket roket;
    [SerializeField]GameObject rebuildButton;
    [SerializeField]GameObject restockButton;
    [SerializeField]GameObject startButton;
    [SerializeField]Text status;
    [SerializeField]Image image;
    [SerializeField]GameObject Reqs;
    [SerializeField]GameObject Ress;
    [SerializeField] Slider[] sliders;
    [SerializeField]Text[] Maxes;
    [SerializeField]Text[] Currs;
    [SerializeField] Slider progressBar;
    //ERRORS
    [SerializeField]FadingCant noReqs;
    [SerializeField]FadingCant noRess;
    [SerializeField]FadingCant noPilots;

    void Start()
    {
        BL = GameObject.Find("marioIdle").GetComponent<BuildRegulator>();
        resources = GameObject.Find("marioIdle").GetComponent<ResourcesClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(string room in BL.GetBuiltRooms()){
            if(room=="Shuttle Room"){
                ResourceButton.SetActive(true);
                roket = GameObject.Find("shuttle").GetComponent<Rocket>(); //checks for already built shuttle room
            }
        }
        if(roket!=null){ //does nothing if there isnt shuttle room
            if(BL.PilotsOnBoard.Count!=0 && roket.pilots[0]==null){
                roket.pilots[0] = BL.PilotsOnBoard[0]; // actively searches for first pilot
                BL.PilotsOnBoard.RemoveAt(0);
            }
            if(BL.PilotsOnBoard.Count!=0 && roket.pilots[1]==null){ 
                roket.pilots[1] = BL.PilotsOnBoard[0]; // actively searches for second pilot
                BL.PilotsOnBoard.RemoveAt(0);
            }
            status.text = roket.status;
            if(roket.status == "Not Built"){ // sets up all needed UI elements in the Expedition menu like resources required to build a rocket
                rebuildButton.SetActive(true);
                restockButton.SetActive(false);
                startButton.SetActive(false);
                image.gameObject.SetActive(false);
                progressBar.gameObject.SetActive(false);
                Reqs.SetActive(true);
                Reqs.transform.GetChild(0).GetComponent<Text>().text = roket.MetalNeeded.ToString();
                // ----- handles the color of required resources -----
                if(roket.MetalNeeded > resources.CheckResource("Black metal")){  
                    Reqs.transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                }else{
                    Reqs.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
                }

                Reqs.transform.GetChild(1).GetComponent<Text>().text = roket.ColorNeeded.ToString();
                if(roket.ColorNeeded > resources.CheckResource("Colored metal")){
                    Reqs.transform.GetChild(1).GetComponent<Text>().color = new Color32(255,0,0,255);
                }else{
                    Reqs.transform.GetChild(1).GetComponent<Text>().color = new Color32(0,0,0,255);
                }
                // ----------------------------------------------------
                Ress.SetActive(false);

            }else if(roket.status == "On Station"){ // sets up all needed UI elements in the Expedition menu like resources required to launch an expedition
                rebuildButton.SetActive(false);
                restockButton.SetActive(true);
                startButton.SetActive(true);
                image.gameObject.SetActive(true);
                progressBar.gameObject.SetActive(false);
                Reqs.SetActive(false);
                Ress.SetActive(true);
                for(int i=0; i<4; i++){
                    Maxes[i].text = roket.maxes[i].ToString();
                    sliders[i].maxValue = roket.maxes[i];
                    Currs[i].text = roket.currs[i].ToString(); // manages all slideBars in the menu
                    sliders[i].value = roket.currs[i];
                }


            }else if(roket.status == "On Expedition"){ // sets up all needed UI elements in the Expedition menu like expedition Progress 
                rebuildButton.SetActive(false);
                restockButton.SetActive(false);
                startButton.SetActive(false);
                image.gameObject.SetActive(true);
                Reqs.SetActive(false);
                Ress.SetActive(false);
                progressBar.gameObject.SetActive(true);
                progressBar.maxValue = roket.maxes[0]*50;
                progressBar.value = roket.currs[0];
                // PROGRESS BAR
            }
        }
    }
    //Methods that call methots of the shuttle so they can be used by buttons
    public void Restock(){
        roket.Restock();
    }

    public void ReBuild(){
        if(!roket.ReBuild()){
           noReqs.fading = 255; // sets error message
        }
    }

    public void StartExpedition(){
        if(roket.StartExpedition()==0){
            noPilots.fading = 255; // sets error message
        }else if(roket.StartExpedition()==1){
            noRess.fading = 255; // sets error message
        }else{
            print("expedition started");
        }
    }
}
