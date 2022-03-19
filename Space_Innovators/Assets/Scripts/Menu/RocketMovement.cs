using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    private float X;
    private float Y;

    private float nextActionTime = 0f;
    private float period = 30f;

    void Start(){
        X = transform.position.x;
        Y = transform.position.y;
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up);

        if (Time.time > nextActionTime){
            nextActionTime += period;
            gameObject.transform.position = new Vector2(X,Y);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-gameObject.GetComponent<Rigidbody2D>().velocity*50);
        }
    }

}
