using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToTarget : MonoBehaviour
{
    TMPro.TMP_Dropdown drop;
    [SerializeField]Camera cam;
    void Start()
    {
        drop = transform.GetComponent<TMPro.TMP_Dropdown>();
    }

    public void MoveCamera()
    {
        if(drop.value-1>=0){
            Vector3 targetPos = GameObject.FindGameObjectsWithTag("Station")[drop.value-1].transform.position;
            cam.transform.position =  new Vector3(targetPos.x,targetPos.y,cam.transform.position.z);
        }
    }
}
