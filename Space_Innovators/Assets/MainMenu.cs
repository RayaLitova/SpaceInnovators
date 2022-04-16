using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Load()
    {
        //yield return new WaitForSeconds(2f);
        AsyncOperation op = SceneManager.LoadSceneAsync("Menu");
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            Debug.Log("aaaaaaaaaaaaaaa");
            Load();
        }
    }

}
