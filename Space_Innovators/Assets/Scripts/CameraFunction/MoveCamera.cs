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
        if(Input.GetMouseButton(0)){
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false){
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }else if(!Input.GetMouseButton(0)){
            drag = false;
        }
        if(drag){
            Camera.main.transform.position = Origin - Difference;
        }

        
    }
}
