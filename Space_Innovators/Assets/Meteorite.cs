using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private bool isMeteoriteActive = false;
    public GameObject Res;
    public GameObject gatheredMenu;

    bool gatheredMenuActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gatheredMenuActive){
            gatheredMenu.SetActive(true);
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
        isMeteoriteActive = false;
        Res.GetComponent<ResourcesScript>().resources[0].quantity += 1;
        Res.GetComponent<ResourcesScript>().resources[6].quantity += 3;
        gatheredMenuActive = true;
    }
}
