using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    public void Activate(string name){
        transform.Find("Name").GetComponent<Text>().text = name;
        transform.Find("Status").Find("StatusText").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.tag;
        transform.Find("Level").Find("LevelText").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().roomLevel.ToString();
        transform.Find("Condition").Find("ConditionText").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().condition + "/100";
        
        string text="";
        for(int i=0; i<GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().producedResources.Length;i++){
            text += GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().producedResources[i] + " x" + GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().producedResourcesQuantity[i] + ", ";
        }
        transform.Find("Resources").Find("ResourcesText").GetComponent<Text>().text = text;
    }
}
