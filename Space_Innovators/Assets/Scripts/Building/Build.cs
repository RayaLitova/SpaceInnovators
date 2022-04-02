using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    [SerializeField] TMPro.TMP_Dropdown DropDown;
    
    public void build(){
        for(int i=0; i<GameObject.FindGameObjectsWithTag("Menu").Length;i++){
            GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().pressed=false;
            GameObject.FindGameObjectsWithTag("Menu")[i].SetActive(false);    
        }
        
        BL.newX++;
        BL.newY++;
        BL.buildRoom(BL.newX,BL.newY,DropDown.value);
        /*Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector2(-1.3f,-1.2f);
        GameObject newGameObject = Instantiate(to_build, objectPOS, Quaternion.identity);
        BL.map[BL.newX,BL.newY] = 1;

        AstarPath.active.Scan();
        BL.UpdateInteractables();*/
    }
 
}
