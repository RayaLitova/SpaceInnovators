using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHide : MonoBehaviour
{

    private Color c1;
    private Color c2;
    private float speed = 0.0015f;

    public void Hide(){
        GameObject.Find("marioIdle").GetComponent<NotificationControl>().Move(int.Parse(System.Text.RegularExpressions.Regex.Match(gameObject.name, @"\d+").Value));
    }
    void Start(){
        c1 = transform.GetChild(0).gameObject.GetComponent<Image>().color;
        c2 = transform.GetChild(1).gameObject.GetComponent<Text>().color;
    }
    public void change(float v){
        c1.a = v;
        c2.a = v;
    }

    void FixedUpdate(){
        c1.a -= speed;
        c2.a -= speed;

        transform.GetChild(0).gameObject.GetComponent<Image>().color = c1;
        transform.GetChild(1).gameObject.GetComponent<Text>().color = c2;
        transform.GetChild(2).gameObject.GetComponent<Text>().color = c2;
        if(c1.a <= 0f){
            GameObject.Find("marioIdle").GetComponent<NotificationControl>().Move(0);
            speed = 0.0015f;
        }else if(c1.a <= 0.4f){
            speed = 0.01f;
        }else{
            speed = 0.0015f;
        }
    }

    
}
