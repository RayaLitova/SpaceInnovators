using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2tanks : MonoBehaviour
{
    private int o2Remaining = 179;
    private int bottleNum = 180/10;
    public int minionCount = 1;

    private float nextActionTime = 0f;
    private float period = 100f;
    private bool Alive = true;
    void Update()
    {
        if (Time.time > nextActionTime && Alive){
            nextActionTime += period/minionCount;
            o2Remaining--;
            if(o2Remaining/10 != bottleNum){
                bottleNum = (o2Remaining/10)+1;
                GameObject.Destroy(transform.Find("o2tank (" + bottleNum-- + ")").gameObject);
            }
            Debug.Log("a");
            if(o2Remaining==0){
                Debug.Log("u dead lol"); //kill minions that require O2
                Alive = false;
            }
        }
    }
}
