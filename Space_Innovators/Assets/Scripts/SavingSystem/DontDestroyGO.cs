using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGO : MonoBehaviour
{
    void Start(){
        if(GameObject.FindGameObjectsWithTag("Saving").Length<=1){
            DontDestroyOnLoad(gameObject);
        }else{  
            Destroy(gameObject);
        }  
    }
}
