using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openStationMenu : MonoBehaviour
{
    void OnMouseOver(){
        if(Input.GetMouseButtonDown(1)){
            GameObject.Find("StationMenu").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("StationMenu").transform.GetChild(0).GetComponent<SetText>().Activate(gameObject.name);
        }
    }
}
