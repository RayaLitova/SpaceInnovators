using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SetText : MonoBehaviour
{
    string text;
    public void Activate(string name){
        //-------------Name---------------
        transform.Find("Name").GetComponent<Text>().text = name;

        //----------Status------------
        if(GameObject.Find(name).transform.parent.gameObject.tag!="Untagged")
            transform.Find("Status").Find("StatusText").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.tag;
        else if(GameObject.Find(name).tag.Split(':')[0] == "Used")
            transform.Find("Status").Find("StatusText").GetComponent<Text>().text = "Active";
        else
            transform.Find("Status").Find("StatusText").GetComponent<Text>().text = "Inactive";

        //-------------Level----------------
        transform.Find("Level").Find("LevelText").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().roomLevel.ToString();
        //-----------Condition---------------
        transform.Find("Condition").Find("ConditionText").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().condition + "/100";
        
        //---------------------produced resources-------------------------
        text="";
        for(int i=0; i<GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().producedResources.Length;i++){
            text += GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().producedResources[i] + " x" + GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().producedResourcesQuantity[i] + ", ";
        }
        transform.Find("Resources").Find("ResourcesText").GetComponent<Text>().text = text;

        //--------------upgrade button--------------
        Transform upgrade = transform.Find("Upgrades");
        if(GameObject.Find("marioIdle").GetComponent<BuildRegulator>().unlockedUpgrades.Contains(GameObject.Find(name).transform.parent.gameObject.name)){
            upgrade.Find("Button").gameObject.SetActive(true);
            upgrade.Find("Button").Find("Text").GetComponent<Text>().text = "Level Up";
            upgrade.Find("Button").GetComponent<Button>().onClick.AddListener( GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().StartUpgrade);
            upgrade.Find("Button").GetComponent<Button>().interactable = GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().CheckUpgradeRequirements();
            
            upgrade.Find("Description").gameObject.SetActive(true);
            upgrade.Find("Description").GetComponent<Text>().text = GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().GetUpgradeDesc();

            upgrade.Find("RequiredResources").gameObject.SetActive(true);
            text="";
            for(int i=0; i<GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().resourcesNames.Length;i++){
                text += GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().resourcesNames[i] + " x" + GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().resourcesQuanity[i]*(25/100*GameObject.Find(name).transform.parent.gameObject.GetComponent<RoomStatics>().roomLevel) + ", ";
            }
            upgrade.Find("RequiredResources").Find("ResourcesText").GetComponent<Text>().text = text;

        }else{
            upgrade.Find("Button").gameObject.SetActive(false);
            upgrade.Find("Description").gameObject.SetActive(false);
            upgrade.Find("RequiredResources").gameObject.SetActive(false);
        }
    }
}
