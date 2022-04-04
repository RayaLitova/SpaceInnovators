using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTargetOptions : MonoBehaviour
{
    //[SerializeField] BuildRegulator BR;
    [SerializeField] TMPro.TMP_Dropdown DropDown;

    public void UpdateOptions(){
        DropDown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("No Target");
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Station")){
           options.Add(go.name);
        }
        DropDown.AddOptions(options);
        DropDown.RefreshShownValue();
    }
}
