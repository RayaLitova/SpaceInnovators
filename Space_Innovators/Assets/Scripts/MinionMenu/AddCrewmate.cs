using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCrewmate : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    //[SerializeField] TMPro.TMP_Dropdown PlanetDown;
    //[SerializeField] TMPro.TMP_Dropdown TargetDown;
    //public string Planet;
    //public string Profession;
    [SerializeField] ChangePlanetIcon CrewButton;
    [SerializeField] FadingCant NotEnoughBed;
    [SerializeField] FadingCant NoTarget;

    public void AddCrew(){
        if(CrewButton.Profession==""){
            //printirai che nqma target
            NoTarget.fading = 255;
            return;
        }
        if(GameObject.FindGameObjectsWithTag("Bed").Length < 1){
            //printirai che nqma leglo
            NotEnoughBed.fading = 255;
            return;
        }
        BL.addCrewMate(CrewButton.Planet/*tuka sloji da e value ot scrol za paneti*/, CrewButton.Profession/*tuka sloji da e value ot scrol za profesii*/);
        
    }
}
