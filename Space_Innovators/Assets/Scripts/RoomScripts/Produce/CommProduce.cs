using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommProduce : MonoBehaviour
{
    public void RecieveRandomResources(){
        string notificationText = "";
        for(int i=0;i<5;i++){
            notificationText += GameObject.Find("marioIdle").GetComponent<ResourcesClass>().AddRandomResource(4,10,null,"Earth");
            notificationText+= ", ";
        }
        GameObject.Find("marioIdle").GetComponent<NotificationControl>().CreateNotification("Recieved resources from Earth!", notificationText);
    }
}
