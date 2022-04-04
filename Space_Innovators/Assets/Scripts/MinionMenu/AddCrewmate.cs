using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCrewmate : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    [SerializeField] TMPro.TMP_Dropdown PlanetDown;
    [SerializeField] TMPro.TMP_Dropdown TargetDown;
    [SerializeField] FadingCant NotEnoughBed;
    [SerializeField] FadingCant NoTarget;

    void Start(){
    }

    public void AddCrew(){
        if(TargetDown.value==0){
            //printirai che nqma target
            NoTarget.fading = 255;
            return;
        }
        if(GameObject.FindGameObjectsWithTag("Bed").Length < 1){
            //printirai che nqma leglo
            NotEnoughBed.fading = 255;
            return;
        }
        BL.addCrewMate(PlanetDown.value, TargetDown.value-1);
        
    }
}
