using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{

    public static CurrentProgress c;
    private float nextActionTime = 0f;
    private float period = 200f;

    void Start()
    {
        c = new CurrentProgress();
        Debug.Log(c.recipes[0]);
        gameObject.GetComponent<SavingSystem>().Save(c);
    }

    void Update()
    {
        if (Time.time > nextActionTime){
            nextActionTime += period;
            c.daysPassed++;
            Debug.Log(c.recipes);
            gameObject.GetComponent<SavingSystem>().Save(c);
        }
    }

}