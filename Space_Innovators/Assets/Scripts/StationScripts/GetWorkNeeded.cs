using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWorkNeeded : MonoBehaviour
{
    static int work = 0;

    public void Produce(){
        work++;
        if(work>=gameObject.GetComponent<RoomStatics>().produce_at * (1/gameObject.GetComponent<RoomStatics>().roomLevel)){
            for(int i=0; i<gameObject.GetComponent<RoomStatics>().producedResources.Length; i++){
                GameObject.Find("marioIdle").gameObject.GetComponent<ResourcesClass>().AddResource(gameObject.GetComponent<RoomStatics>().producedResources[i], gameObject.GetComponent<RoomStatics>().producedResourcesQuantity[i]);
            }
            work=0;
        }
    }
}
