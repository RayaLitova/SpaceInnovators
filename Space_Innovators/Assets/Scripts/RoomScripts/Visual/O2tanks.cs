using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2tanks : MonoBehaviour
{
    private int bottleNum;
    public int minionCount = 1;

    private float nextActionTime = 0f;
    private float period = 100f;
    
    void Start(){
        bottleNum = GameObject.Find("marioIdle").GetComponent<ResourcesClass>().resources["O2"]/10;
        minionCount = GameObject.FindGameObjectsWithTag("Minion").Length;
    }

    void Update()
    {
        if(GameObject.Find("marioIdle").GetComponent<ResourcesClass>().resources["O2"]/10 != bottleNum){
            bottleNum = (GameObject.Find("marioIdle").GetComponent<ResourcesClass>().resources["O2"]/10);
            //for(int i=0;i<)
            //gameObject.Find()
        }
        
    }
}
