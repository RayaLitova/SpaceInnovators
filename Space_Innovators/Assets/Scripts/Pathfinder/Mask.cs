using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<SpriteMask>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
