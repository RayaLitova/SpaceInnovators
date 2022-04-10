using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private Vector3 Origin;
    private Vector3 Difference;
    public static bool drag = false;
    private Vector3 position = new Vector3();

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

        position.x = Mathf.Clamp(Camera.main.transform.position.x, -75 + Camera.main.orthographicSize, 75 - Camera.main.orthographicSize);
        position.y = Mathf.Clamp(Camera.main.transform.position.y, -55 + Camera.main.orthographicSize, 55 - Camera.main.orthographicSize);
        position.z = -10;
        Camera.main.transform.position = position;
    }
}
