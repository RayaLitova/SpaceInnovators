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
    [SerializeField] FadingCant cantBuild;
    [SerializeField] FadingCant NotEnoughRes;
    [SerializeField] GameObject menu;
    bool created = false;
    bool Done = false;
    public bool working = false;
    GameObject newGameObject = null;


    void Update(){
        if(working){
            Vector3 objectPOS = new Vector3(0,0,1);
            
            if(!created){
                menu.SetActive(true);
                newGameObject = Instantiate(img, objectPOS, Quaternion.identity);
                created = true;
                newX = 5;
                newY = 5;
                if(!BL.unlockedRooms[DropDown.value].GetComponent<RoomStatics>().CheckRequirements()){
                    Debug.Log("Not enough resources!");
                    NotEnoughRes.fading = 255;
                    Cancel();
                }
            } 
            
            if(Done){
                BL.buildRoom(newX,newY,DropDown.value);
                Cancel();
                
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
                    cantBuild.fading = 255;
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                Cancel();
            }

        }   

    }

    void Cancel(){
        for(int i=0; i<GameObject.FindGameObjectsWithTag("UIButton").Length;i++)
            GameObject.FindGameObjectsWithTag("UIButton")[i].GetComponent<MenuOpener>().canBePressed = true;
        Destroy(newGameObject);
        newGameObject = null;
        created = false;
        working = false;
        Done = false;
        menu.SetActive(false);
    }
}
