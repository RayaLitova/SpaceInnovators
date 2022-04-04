using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBuildOptions : MonoBehaviour
{
    [SerializeField] BuildRegulator BR;
    [SerializeField] TMPro.TMP_Dropdown DropDown;

    public void UpdateOptions(){
        DropDown.ClearOptions();
        List<string> options = new List<string>();
        foreach(GameObject go in BR.unlockedRooms){
           options.Add(go.name);
        }
        DropDown.AddOptions(options);
        DropDown.RefreshShownValue();
    }
}
