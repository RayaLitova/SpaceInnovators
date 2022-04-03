using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlaceToBuild : MonoBehaviour
{
    
    int newX = 5;
    int newY = 5;

    [SerializeField] BuildRegulator BL;
    [SerializeField] TMPro.TMP_Dropdown DropDown;
    [SerializeField] GameObject img;
    [SerializeField] Camera camera;
    [SerializeField] FadingCant text;
    bool created = false;
    bool Done = false;
    public bool working = false;
    GameObject newGameObject = null;
   /* void Start(){
        StartCoroutine(transform.name);
        transform.GetComponent<ChoosePlaceToBuild>().enabled = false;
    }*/


    void Update(){
        //print("booooooooooooooooooooooo");
        if(working){
            //newX = 5;
            //newY = 5;
            Vector3 objectPOS = new Vector3(0,0,1);
            
            if(!created){
                newGameObject = Instantiate(img, objectPOS, Quaternion.identity);
                created = true;
                newX = 5;
                newY = 5;
            } 
            //objectPOS = new Vector3((float)(objectPOS.x+((newX-5f)*offset)), (float)(objectPOS.y+((newY-5f)*offset)), 0f);
            
            if(Done){
                BL.buildRoom(newX,newY,DropDown.value);
                for(int i=0; i<GameObject.FindGameObjectsWithTag("UIButton").Length;i++){
                    //GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().enabled = true;
                    GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = true;
                    //GameObject.FindGameObjectsWithTag("UIbutton")[i].SetActive(true);
                }
                Destroy(newGameObject);
                newGameObject = null;
                created = false;
                working = false;
                Done = false;
                
            }
            newGameObject.transform.position = new Vector3((float)(objectPOS.x+((newX-5f)*BL.offset)), (float)(objectPOS.y+((newY-5f)*BL.offset)), 0f);
            camera.transform.position = new Vector3(newGameObject.transform.localPosition.x, newGameObject.transform.localPosition.y, -10);
            if(BL.map[newX,newY]!=2){
                newGameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255,0,0,147);
            }else{
                newGameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(0,255,0,147);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)){  
                newY++;  
            }          
            if (Input.GetKeyDown(KeyCode.DownArrow)){  
                newY--;
            }             
            if (Input.GetKeyDown(KeyCode.LeftArrow)){  
                newX--;
            }              
            if (Input.GetKeyDown(KeyCode.RightArrow)){  
                newX++; 
            }  
            if (Input.GetKeyDown(KeyCode.Return)){  
                if(BL.map[newX,newY]==2){
                    Done = true;
                }else{
                    text.fading = 255;
                }
            }
        }   

    }
}
