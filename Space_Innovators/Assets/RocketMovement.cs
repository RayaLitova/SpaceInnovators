using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        rb.AddForce(transform.up);
    }
}
