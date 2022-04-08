using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPlanetButton : MonoBehaviour
{
    public string Planet;
    public ChangePlanetIcon CrewButton;

    public void ChangePlanet(){
        CrewButton.Planet = Planet;
        CrewButton.Profession = "";
        CrewButton.GetComponent<ChangePlanetIcon>().UpdateOptions();
    }
}
