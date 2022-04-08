using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewScrollScript : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    [SerializeField] ScrollRect ScrollView;
    [SerializeField] GameObject ScrollContent;
    [SerializeField] GameObject ScrollItem;


    // Start is called before the first frame update

    void GenerateItem(string planet){
        GameObject item = Instantiate(ScrollItem);
        item.transform.SetParent(ScrollContent.transform, false);
        print(BL.planetIcons[planet]);
        item.transform.Find("Button").gameObject.GetComponent<Image>().sprite = BL.planetIcons[planet];
        item.transform.Find("Button").gameObject.GetComponent<ScrollPlanetButton>().Planet = planet;
        item.transform.Find("Button").gameObject.GetComponent<ScrollPlanetButton>().CrewButton = GameObject.Find("AddCrewButton").gameObject.GetComponent<ChangePlanetIcon>();
        //SetValue(item,resources.resources[resourceName]);
        //SetMaxValue(item,resources.resourcesMax[resourceName]);
        //item.transform.Find("Name").gameObject.GetComponent<Text>().text = resourceName;
        //item.transform.Find("Content").Find("icon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(resources.icons[resourceName]);
       // Bars.Add(item, resourceName);
    }

    // Update is called once per frame
    public void UpdatePlanetOptions(){
        foreach(Transform child in ScrollContent.transform){
            Destroy(child.gameObject);
        }
        foreach(string planet in BL.unlockedPlanets){
            GenerateItem(planet);
            //print("muuuuuuuuuuuuuuuuuuuuuuuuuuuu");
        }
        ScrollView.horizontalNormalizedPosition = 1;
        
    } 
    
}
