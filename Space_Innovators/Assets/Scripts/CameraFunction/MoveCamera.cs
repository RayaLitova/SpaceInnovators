using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private Vector3 Origin;
    private Vector3 Difference;
    public static bool drag = false;

    private void Update()
    {
        if(Input.GetMouseButton(0) && GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled){
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false){
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }else if(!Input.GetMouseButton(0) || !GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled){
            drag = false;
        }
        if(drag && GameObject.Find("marioIdle").GetComponent<CameraMovement>().isCameraEnabled){
            Camera.main.transform.position = Origin - Difference;
        }

        
    }
}
