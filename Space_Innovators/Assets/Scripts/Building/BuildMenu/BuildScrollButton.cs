using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScrollButton : MonoBehaviour
{
    public GameObject room;
    public ChangeBuidingIcon buildbutton;

    public void ChangeRoomToBuild(){
        buildbutton.room = room;
    }
}
