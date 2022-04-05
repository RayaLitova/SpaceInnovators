using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHide : MonoBehaviour
{

    private Color c1;
    private Color c2;

    public void Hide(){
        GameObject.Find("marioIdle").GetComponent<NotificationControl>().Move(int.Parse(System.Text.RegularExpressions.Regex.Match(gameObject.name, @"\d+").Value));
    }
    void Start(){
        c1 = transform.GetChild(0).gameObject.GetComponent<Image>().color;
        c2 = transform.GetChild(1).gameObject.GetComponent<Text>().color;
    }
    

    void Update(){
        c1.a -= Time.deltaTime*0.15f;
        c2.a -= Time.deltaTime*0.15f;

        transform.GetChild(0).gameObject.GetComponent<Image>().color = c1;
        transform.GetChild(1).gameObject.GetComponent<Text>().color = c2;
        transform.GetChild(2).gameObject.GetComponent<Text>().color = c2;

        if(c1.a <= 0f){
            GameObject.Find("marioIdle").GetComponent<NotificationControl>().Move(0);
        }
    }

    
}
