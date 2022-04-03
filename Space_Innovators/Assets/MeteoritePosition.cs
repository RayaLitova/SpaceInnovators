using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritePosition : MonoBehaviour
{
    private System.Random random = new System.Random();
    
    void Start()
    {
        transform.position = new Vector2(random.Next(-100, 100),random.Next(-100, 100));
    }
}
