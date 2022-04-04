using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    
    [SerializeField] ChoosePlaceToBuild script;


    public void build(){
        for(int i=0; i<GameObject.FindGameObjectsWithTag("UIButton").Length;i++){
            GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().pressed=false;
            GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = false;

        }
        for(int i=0; i<GameObject.FindGameObjectsWithTag("Menu").Length;i++)
            GameObject.FindGameObjectsWithTag("Menu")[i].SetActive(false);
        
        script.working = true;
   
    }
 
}
