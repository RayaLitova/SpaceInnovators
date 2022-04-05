using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBuidingIcon : MonoBehaviour
{
    [SerializeField] public Sprite[] Images;
    [SerializeField] Image img;
    [SerializeField] Text Desc;
    [SerializeField] GameObject[] Reqs;
    [SerializeField] TMPro.TMP_Dropdown drop;
    
    void OnEnable(){
        drop.value = 0;
        ChangeCharacteristic();
    }

    public void ChangeImage(){
        img.sprite = Images[drop.value];
    }

    public void ChangeCharacteristic(){
        //img.sprite = Images[drop.value];
        GameObject mario = GameObject.Find("marioIdle");
        RoomStatics room = mario.GetComponent<BuildRegulator>().unlockedRooms[drop.value].GetComponent<RoomStatics>();
        for(int i=0; i<5;i++){
            string txt = room.GetReqirementsByIndex(i);
            if(mario.GetComponent<ResourcesClass>().CheckResource(txt)<room.CheckReqirementsQuantityByIndex(i)){
                Reqs[i].transform.GetChild(1).GetComponent<Text>().color = new Color32(150,21,21,255);
            }else{
                Reqs[i].transform.GetChild(1).GetComponent<Text>().color = new Color32(0,0,0,255);
            }
            Reqs[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(mario.GetComponent<ResourcesClass>().icons[txt]);
            Reqs[i].transform.GetChild(1).GetComponent<Text>().text = txt +  room.GetReqirementsQuantityByIndex(i);
            
            
        }
        Desc.text = RoomStatics.GetDescription(drop.value);
    }

}
