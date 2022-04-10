using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCameraMovement : MonoBehaviour
{
    public GameObject menu;
    public void OnClose()
    {
        menu.SetActive(false);
        GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled = true;
    }
}
