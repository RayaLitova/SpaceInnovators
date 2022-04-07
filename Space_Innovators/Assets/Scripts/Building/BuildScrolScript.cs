using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScrolScript : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    [SerializeField] ScrollRect ScrollView;
    [SerializeField] GameObject ScrollContent;
    [SerializeField] GameObject ScrollItem;

    // Start is called before the first frame update

    void GenerateItem(GameObject room){
        GameObject item = Instantiate(ScrollItem);
        item.transform.SetParent(ScrollContent.transform, false);
        item.transform.Find("Button").gameObject.GetComponent<Image>().sprite = room.GetComponent<RoomStatics>().GetIcon();
        item.transform.Find("Button").gameObject.GetComponent<BuildScrollButton>().room = room;
        item.transform.Find("Button").gameObject.GetComponent<BuildScrollButton>().buildbutton = GameObject.Find("BuildButton").gameObject.GetComponent<ChangeBuidingIcon>();
        //SetValue(item,resources.resources[resourceName]);
        //SetMaxValue(item,resources.resourcesMax[resourceName]);
        //item.transform.Find("Name").gameObject.GetComponent<Text>().text = resourceName;
        //item.transform.Find("Content").Find("icon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(resources.icons[resourceName]);
       // Bars.Add(item, resourceName);
    }

    // Update is called once per frame
    public void UpdateBuildOptions(){
        foreach(Transform child in ScrollContent.transform){
            Destroy(child.gameObject);
        }
        foreach(GameObject room in BL.unlockedRooms){
            GenerateItem(room);
            //print("muuuuuuuuuuuuuuuuuuuuuuuuuuuu");
        }
        ScrollView.horizontalNormalizedPosition = 1;
        
    }
}
