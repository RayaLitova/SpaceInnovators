using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NotificationControl : MonoBehaviour
{
    private int notificationNumber = 0;
    private bool isFull = false;
    private Dictionary<string,string> awaiting = new Dictionary<string,string>();

    public void CreateNotification(string title, string text, bool isAwaiting = false){
        if(isFull){
            Debug.Log("Max Notifications Reached");
            awaiting.Add(title,text);
            return;
        }
        if(isAwaiting){
            awaiting.Remove(awaiting.ElementAt(0).Key);
        }
        GameObject n = GameObject.Find("NotificationsCanvas").transform.GetChild(notificationNumber).gameObject;
        n.SetActive(true);
        n.transform.GetChild(1).GetComponent<Text>().text = title;
        n.transform.GetChild(2).GetComponent<Text>().text = text;

        Color c = n.transform.GetChild(0).GetComponent<Image>().color;
        c.a = 255f;

        Color c1 = n.transform.GetChild(1).GetComponent<Text>().color;
        c1.a = 255f;
        n.transform.GetChild(0).GetComponent<Image>().color = c;
        n.transform.GetChild(1).GetComponent<Text>().color = c1;
        n.transform.GetChild(2).GetComponent<Text>().color = c1;

        notificationNumber++;
        if(notificationNumber==4){
            isFull=true;
            notificationNumber = 0;
        }

    }

    public void Move(int num){
        if(isFull) isFull=false;
        int i=num;
        for(;i<3;i++){
            GameObject n = GameObject.Find("Notification ("+i+")");
            GameObject nn = GameObject.Find("Notification ("+(i+1)+")");
            if(nn==null || !nn.activeSelf) break;
            n.transform.GetChild(1).GetComponent<Text>().text = nn.transform.GetChild(1).GetComponent<Text>().text;
            n.transform.GetChild(2).GetComponent<Text>().text = nn.transform.GetChild(2).GetComponent<Text>().text;

            n.transform.GetChild(0).GetComponent<Image>().color = nn.transform.GetChild(0).GetComponent<Image>().color;
            n.transform.GetChild(1).GetComponent<Text>().color = nn.transform.GetChild(1).GetComponent<Text>().color;
            n.transform.GetChild(2).GetComponent<Text>().color = nn.transform.GetChild(2).GetComponent<Text>().color;
        }
        notificationNumber--;
        if(notificationNumber<0) notificationNumber = 3;
        GameObject.Find("Notification ("+notificationNumber+")").SetActive(false);
    }

    void Update(){
        if(!isFull && awaiting.Count>0){
            CreateNotification(awaiting.ElementAt(0).Key, awaiting.ElementAt(0).Value, true);
        }
    }
}
