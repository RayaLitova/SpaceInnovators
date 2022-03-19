using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScript : MonoBehaviour
{
    public Resource[] resources = new Resource[8];
    
    void Start()
    {
        resources[0] = new Resource("Black metal", "build_metal_symbol.png", 0);
        resources[1] = new Resource("Colored metal", "color_metal_symbol_alt.png", 0);
        resources[2] = new Resource("Energy", "ENERGY_HUMAN.png", 0);
        resources[3] = new Resource("Food", "food_symbol.png", 0);
        resources[4] = new Resource("Fuel", "fuel_symbol_alt.png", 0);
        resources[5] = new Resource("Medicine", "MEDKIT.png", 0);
        resources[6] = new Resource("Space rock", "space_rock_symbol.png", 0);
        resources[7] = new Resource("Water", "water_symbol.png", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
