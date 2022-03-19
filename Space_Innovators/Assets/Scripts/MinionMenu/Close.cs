using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{    //[SerializeField]public GameObject statCanvas;
    public NPCStats stats;
    // Start is called before the first frame update
    public void Activate()
    {
            stats.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
