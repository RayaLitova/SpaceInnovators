using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHide : MonoBehaviour
{

    public void Hide(){
        GameObject.Find("marioIdle").GetComponent<NotificationControl>().Move(int.Parse(System.Text.RegularExpressions.Regex.Match(gameObject.name, @"\d+").Value));
    }

    void Update(){
        Color tempColor1 = transform.GetChild(0).gameObject.GetComponent<Image>().color;
        tempColor1.a -= Time.deltaTime * 0.15f;
        transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor1;

        Color tempColor2 = transform.GetChild(1).gameObject.GetComponent<Text>().color;
        tempColor2.a -= Time.deltaTime * 0.15f;
        transform.GetChild(1).gameObject.GetComponent<Text>().color = tempColor2;

        Color tempColor3 = transform.GetChild(2).gameObject.GetComponent<Text>().color;
        tempColor3.a -= Time.deltaTime * 0.15f;
        transform.GetChild(2).gameObject.GetComponent<Text>().color = tempColor3;

        if(tempColor3.a <= 0f){
            GameObject.Find("marioIdle").GetComponent<NotificationControl>().Move(0);
        }
    }

    
}
