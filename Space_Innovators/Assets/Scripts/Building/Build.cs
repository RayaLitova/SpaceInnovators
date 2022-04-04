using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    
    [SerializeField] ChoosePlaceToBuild script;
    /*void OnEnable(){
        StartCoroutine(transform.name);
        //transform.GetComponent<ChoosePlaceToBuild>().enabled = false;
    }*/

    public void build(){
        //print(GameObject.FindGameObjectsWithTag("Menu"));
        for(int i=0; i<GameObject.FindGameObjectsWithTag("UIButton").Length;i++){
            GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().pressed=false;
            GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = false;
            //GameObject.FindGameObjectsWithTag("Menu")[i].SetActive(false);
            
            //GameObject.FindGameObjectsWithTag("UIButton")[i].SetActive(false);

        }
        for(int i=0; i<GameObject.FindGameObjectsWithTag("Menu").Length;i++){
                    //GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().pressed=false;
                    //GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = false;
                    GameObject.FindGameObjectsWithTag("Menu")[i].SetActive(false);
                    
                }
        //BL.newX++;
        //BL.newY++;
        //ChoosePlaceToBuid();
        script.working = true;
        //if(transform.GetComponent<ChoosePlaceToBuild>().enabled == true)StartCoroutine(transform.name);
        //BL.buildRoom(newX,newY,DropDown.value);
        /*Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector2(-1.3f,-1.2f);
        GameObject newGameObject = Instantiate(to_build, objectPOS, Quaternion.identity);
        BL.map[BL.newX,BL.newY] = 1;

        AstarPath.active.Scan();
        BL.UpdateInteractables();*/
    }
 
}
