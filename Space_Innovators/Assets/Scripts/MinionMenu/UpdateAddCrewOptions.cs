using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAddCrewOptions : MonoBehaviour
{
    [SerializeField] BuildRegulator BR;
    [SerializeField] TMPro.TMP_Dropdown DropDown;

    public void UpdateOptions(){
        DropDown.ClearOptions();
        List<string> options = new List<string>();
        foreach(string go in BR.unlockedPlanets){
           options.Add(go);
        }
        DropDown.AddOptions(options);
        DropDown.RefreshShownValue();
    }
}
