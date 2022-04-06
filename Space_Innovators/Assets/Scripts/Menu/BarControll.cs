using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarControll : MonoBehaviour
{
    
    //[SerializeField] List<Text> vals;
    //[SerializeField] List<Text> maxVals;
    ResourcesClass resources;
    [SerializeField] ScrollRect ScrollView;
    [SerializeField] GameObject ScrollContent;
    [SerializeField] GameObject ScrollItem;
    Dictionary<GameObject, string> Bars = new Dictionary<GameObject, string>();
    bool created = false;

    void Start(){
        resources = GameObject.Find("marioIdle").GetComponent<ResourcesClass>();
        
        //print(Resources.resourcesMax["O2"]+"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        
    }

    void GenerateItem(string resourceName){
        GameObject item = Instantiate(ScrollItem);
        item.transform.SetParent(ScrollContent.transform, false);
        SetValue(item,resources.resources[resourceName]);
        SetMaxValue(item,resources.resourcesMax[resourceName]);
        item.transform.Find("Name").gameObject.GetComponent<Text>().text = resourceName;
        item.transform.Find("Content").Find("icon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(resources.icons[resourceName]);
        Bars.Add(item, resourceName);
    }

    public void SetValue(GameObject slider, int value){
        slider.transform.Find("Content").gameObject.GetComponent<Slider>().value = value; 
        slider.transform.Find("current").GetComponent<Text>().text = value.ToString();
        print("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
    }

    public void SetMaxValue(GameObject slider, int value){
        slider.transform.Find("Content").gameObject.GetComponent<Slider>().maxValue = value; 
        slider.transform.Find("max").GetComponent<Text>().text = value.ToString();
    }

    void Update(){
        if(!created){
            for(int i=0;i<resources.resourcesMax.Count;i++){
                GenerateItem(resources.resourcesMax.ElementAt(i).Key);
            }
            ScrollView.verticalNormalizedPosition = 1;
            created = true;
            
        }
        
        for(int i=0;i<Bars.Count;i++){
            SetValue(Bars.ElementAt(i).Key,resources.resources[Bars.ElementAt(i).Value]);
            SetMaxValue(Bars.ElementAt(i).Key,resources.resourcesMax[Bars.ElementAt(i).Value]);
        }
    }

}
