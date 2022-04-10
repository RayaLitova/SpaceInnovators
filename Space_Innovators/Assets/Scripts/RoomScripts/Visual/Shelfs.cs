using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelfs : MonoBehaviour
{
    public int resourceCount = 0;
    int resourceCapacity = 10;
    public Sprite low;
    public Sprite med;
    public Sprite high1;
    public Sprite high2;
    int randomVal;
    
    void Start(){
        randomVal = Random.Range(1,3);
    }
    void Update()
    {
        if(resourceCount <= (1.0/3.0)*resourceCapacity){
            gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = low;
        }else if(resourceCount > (1.0/3.0)*resourceCapacity && resourceCount <= (2.0/3.0)*resourceCapacity){
            gameObject.gameObject.GetComponent<SpriteRenderer>().sprite = med;
        }else{
            if(gameObject.GetComponent<SpriteRenderer>().sprite!=high1 && gameObject.GetComponent<SpriteRenderer>().sprite!=high2){
                if(randomVal==1){
                    gameObject.GetComponent<SpriteRenderer>().sprite = high1;
                }else{
                    gameObject.GetComponent<SpriteRenderer>().sprite = high2;
                }
            }
        }
    }
}
