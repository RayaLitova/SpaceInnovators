using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class DoorButton : MonoBehaviour
{
    public GameObject d1; //door 1
    public GameObject d2; //door 2
    private bool opened = false;

    public void SetDoors(GameObject door1, GameObject door2){
        d1 = door1;
        d2 = door2;
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<Button>().enabled = true;
    }

    public void CloseDoors(){
        d1.GetComponent<DoorsEdit>().CloseDoor();
        d2.GetComponent<DoorsEdit>().CloseDoor();
        opened = false;
        AstarPath.active.Scan();
    }

    public void OpenDoors(){
        d1.GetComponent<DoorsEdit>().OpenDoor();
        d2.GetComponent<DoorsEdit>().OpenDoor();
        opened = true;
        AstarPath.active.Scan();
    }

    public void DoorsControl(){
        if(opened){
            CloseDoors();
        }else{
            OpenDoors();
        }
    }
}
