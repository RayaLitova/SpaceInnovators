using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarControll : MonoBehaviour
{
    [SerializeField] Slider[] Bars;
    [SerializeField] Text[] vals;
    [SerializeField] Text[] maxVals;
    ResourcesClass Resources;
    
    void Start(){
        Resources = GameObject.Find("marioIdle").GetComponent<ResourcesClass>();
        //print(Resources.resourcesMax["O2"]+"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        
    }


    public void SetValue(int barIndex, int value){
        Bars[barIndex].value = value; 
        vals[barIndex].text = value.ToString();
    }

    public void SetMaxValue( int barIndex, int value){
        Bars[barIndex].maxValue = value; 
        maxVals[barIndex].text = value.ToString();
    }

    void Update(){
        SetMaxValue(0,Resources.resourcesMax["O2"]);
        SetMaxValue(1,Resources.resourcesMax["Water"]);
        SetMaxValue(2,Resources.resourcesMax["Food"]);
        SetValue(0,Resources.resources["O2"]);
        SetValue(1,Resources.resources["Water"]);
        SetValue(2,Resources.resources["Food"]);
    }

}
