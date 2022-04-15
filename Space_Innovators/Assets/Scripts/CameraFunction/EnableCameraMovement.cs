using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableCameraMovement : MonoBehaviour
{
    public GameObject menu;
    public void OnClose()
    {
        menu.SetActive(false);
        if(menu.name == "StationMenuGFX")
            menu.transform.Find("Upgrades").Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled = true;
    }
}
