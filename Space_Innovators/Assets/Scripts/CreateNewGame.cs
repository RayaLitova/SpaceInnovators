using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class CreateNewGame : MonoBehaviour
{
    //private GameObject data;
    void Start()
    {
        //data = GameObject.Find("Statics");
    }

    public void LoadingScreen(){
        SceneManager.LoadSceneAsync("LoadingScreen");
    }
}
