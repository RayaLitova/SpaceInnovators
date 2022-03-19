using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    public string name;
    private int workDone = 0;
    private int level = 1;
    public Room r;
    void Start()
    {
        r = new Room(name, workDone, level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
