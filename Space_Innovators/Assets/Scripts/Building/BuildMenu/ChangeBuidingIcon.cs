using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBuidingIcon : MonoBehaviour
{
    //[SerializeField] public Sprite[] Images;
    [SerializeField] Image img;
    [SerializeField] Text Desc;
    [SerializeField] GameObject[] MatReqs;
    [SerializeField] GameObject[] RoomReqs;
    [SerializeField] GameObject[] PlanetReqs;
    //[SerializeField] TMPro.TMP_Dropdown drop;
    public GameObject room;
    public GameObject roomRes;
    RoomStatics roomStats;

    public void Update(){
        //img.sprite = Images[drop.value];
        GameObject mario = GameObject.Find("marioIdle");
        roomStats = room.GetComponent<RoomStatics>();

        img.sprite = roomStats.GetIcon();

        //materials requierd
        for(int i=0; i<5;i++){
            string txt = roomStats.GetReqirementsByIndex(i);
            if(mario.GetComponent<ResourcesClass>().CheckResource(txt)<roomStats.CheckReqirementsQuantityByIndex(i)){
                MatReqs[i].transform.GetChild(1).GetComponent<Text>().color = new Color32(150,21,21,255);
            }else{
                MatReqs[i].transform.GetChild(1).GetComponent<Text>().color = new Color32(0,0,0,255);
            }
            MatReqs[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(mario.GetComponent<ResourcesClass>().icons[txt]);
            MatReqs[i].transform.GetChild(1).GetComponent<Text>().text = txt +  roomStats.GetReqirementsQuantityByIndex(i);
        }

        //rooms requierd
        for(int i=0; i<3;i++){
            int check=0;
            string txt = roomStats.GetRoomsByIndex(i);
            
            foreach(string go in mario.GetComponent<BuildRegulator>().GetBuiltRooms()){
                if(go==txt)check++;
            }
            if(check==0){
                RoomReqs[i].GetComponent<Text>().color = new Color32(150,21,21,255);
            }else{
                RoomReqs[i].GetComponent<Text>().color = new Color32(0,0,0,255);
            }
            RoomReqs[i].GetComponent<Text>().text = txt;
        }

        //planets required
        for(int i=0; i<2;i++){
            int check=0;
            string txt = roomStats.GetPlanetsByIndex(i);
            
            foreach(string go in mario.GetComponent<BuildRegulator>().unlockedPlanets){
                if(go==txt)check++;
            }
            if(check==0){
                PlanetReqs[i].GetComponent<Text>().color = new Color32(150,21,21,255);
            }else{
                PlanetReqs[i].GetComponent<Text>().color = new Color32(0,0,0,255);
            }
            PlanetReqs[i].GetComponent<Text>().text = txt;
        }


        //description
        Desc.text = RoomStatics.GetDescription(room.name);
    }

    public void Reset(){
        room = roomRes;
    }

}
