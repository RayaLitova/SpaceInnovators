using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGathered : MonoBehaviour
{
    void Update()
    {
       if(Input.GetMouseButtonDown(0)){
            for(int i=0;i<4;i++){
                gameObject.transform.Find("Text (" + i + ")").gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
       }
    }
}
