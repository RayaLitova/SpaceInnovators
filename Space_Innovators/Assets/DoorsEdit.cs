using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsEdit : MonoBehaviour
{
    public Sprite open;
    public Sprite closed;

    public void OpenDoor(){
        gameObject.GetComponent<SpriteRenderer>().sprite = open;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseDoor(){
        gameObject.GetComponent<SpriteRenderer>().sprite = closed;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
