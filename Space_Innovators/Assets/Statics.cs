using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{

    private CurrentProgress c;

    private float nextActionTime = 0f;
    private float period = 200f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > nextActionTime){
            nextActionTime += period;
            c.daysPassed++;
        }
    }

}
