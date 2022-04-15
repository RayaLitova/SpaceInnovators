using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfClosed : MonoBehaviour
{
    private string actualTag = "Untagged";
    void Update()
    {
        if(transform.parent.gameObject.tag  != "Closed") actualTag = transform.parent.gameObject.tag;

        if(transform.Find("DoorUp").GetComponent<DoorsEdit>().isClosed())
            if(transform.Find("DoorDown").GetComponent<DoorsEdit>().isClosed())
                if(transform.Find("DoorRight").GetComponent<DoorsEdit>().isClosed())
                    if(transform.Find("DoorLeft").GetComponent<DoorsEdit>().isClosed())
                        transform.parent.gameObject.tag = "Closed";
                    else transform.parent.gameObject.tag = actualTag;
                else transform.parent.gameObject.tag = actualTag;
            else transform.parent.gameObject.tag = actualTag;
        else transform.parent.gameObject.tag = actualTag; 

        transform.parent.Find("SmallRoom").tag = transform.parent.gameObject.tag;
    }
}
