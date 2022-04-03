using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{

    [SerializeField] Transform menu;
    public bool pressed = false;
    public bool canBePressed = true;
    // Start is called before the first frame update

    public void Open(){
        if(canBePressed){
            if(!pressed){
                for(int i=0; i<GameObject.FindGameObjectsWithTag("UIButton").Length;i++){
                    GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().pressed=false;
                    //GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = false;
                   //GameObject.FindGameObjectsWithTag("Menu")[0].SetActive(false);
                    
                }
                for(int i=0; i<GameObject.FindGameObjectsWithTag("Menu").Length;i++){
                    //GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().pressed=false;
                    //GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = false;
                    GameObject.FindGameObjectsWithTag("Menu")[i].SetActive(false);
                    
                }
                menu.gameObject.SetActive(true);
                pressed = true;
            }else{
                menu.gameObject.SetActive(false);
                pressed = false;
            }
        }
    }
}
