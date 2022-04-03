using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCrewmate : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    [SerializeField] TMPro.TMP_Dropdown DropDown;

    public void AddCrew(){
        BL.addCrewMate(DropDown.value);
    }
}
