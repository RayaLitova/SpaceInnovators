using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMeteorites : MonoBehaviour
{
    public GameObject meteorite;

    private float nextActionTime = 10f;
    private float period = 10f;
    private System.Random random = new System.Random();

    void Update(){
        if (Time.time > nextActionTime){
            nextActionTime += period * (1+random.Next(5));
            if(!meteorite.activeSelf){
                meteorite.SetActive(true);
                meteorite.transform.position = new Vector2(random.Next(-100, 100),random.Next(-100, 100));

            }
            
        }
    }
}