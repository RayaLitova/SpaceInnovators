using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAddCrewOptions : MonoBehaviour
{
    [SerializeField] BuildRegulator BR;
    [SerializeField] TMPro.TMP_Dropdown DropDown;
    [SerializeField] ChangePlanetIcon CrewButton;

    void OnEnable(){
        DropDown.value = 0;
        CrewButton.Profession = "";
    }

    public void ChangeProfession(){
       CrewButton.Profession = BR.ProfessionsForPlanet[CrewButton.Planet][DropDown.value-1];
    }
}
