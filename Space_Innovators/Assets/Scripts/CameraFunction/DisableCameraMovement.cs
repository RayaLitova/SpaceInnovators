using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DisableCameraMovement : EventTrigger
{
    public override void OnPointerEnter(PointerEventData data)
    {
        GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled = false;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled = true;
    }
}
