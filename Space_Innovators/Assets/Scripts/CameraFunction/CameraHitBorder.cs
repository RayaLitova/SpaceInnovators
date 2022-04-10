using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHitBorder : MonoBehaviour
{
    Vector3 position = new Vector3();
    void Update()
    {
        
        gameObject.transform.position = position;
    }
}
