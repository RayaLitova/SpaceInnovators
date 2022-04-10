using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNotificationClick : MonoBehaviour
{
    public void onClick(){
        transform.parent.gameObject.GetComponent<NotificationHide>().Hide();
    }
}
