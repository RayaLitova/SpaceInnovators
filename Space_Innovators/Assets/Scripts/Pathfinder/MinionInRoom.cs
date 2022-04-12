using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionInRoom : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D c){
        if(c.gameObject.tag == "Minion")
            c.gameObject.GetComponent<NpcAi>().currentRoom = transform.parent.gameObject;
    }
}
