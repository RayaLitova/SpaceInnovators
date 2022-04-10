using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteorite : MonoBehaviour
{
    private bool isMeteoriteActive = true;
    public ResourcesClass res;
    public GameObject gatheredMenu;
    private System.Random random = new System.Random();

    bool gatheredMenuActive = false;
    
    void Update()
    {
        if(gatheredMenuActive){
            gatheredMenu.SetActive(true);
            gatheredMenuActive = false;
            isMeteoriteActive = true;
            gameObject.SetActive(false);
        }
        if(isMeteoriteActive){
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 0.02f);
        }else{
            if((Vector2)gameObject.transform.position != new Vector2(-7f, 2f))
                gameObject.transform.position = new Vector2(-7f, 2f);
        }
        
    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && !gatheredMenuActive){
            GetResources();
        }
    }

    public void ShowMeteorite(){
        isMeteoriteActive = true;
    }

    public void GetResources(){
        GameObject text;
        isMeteoriteActive = false;
        for(int i=0; i <= random.Next(4);i++){
            res.AddRandomResource(2,3,gatheredMenu.transform.Find("Text (" + i + ")").gameObject.GetComponent<Text>());
        }
        gatheredMenuActive = true;
    }
}
