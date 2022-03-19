using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSampleScene : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation op = SceneManager.LoadSceneAsync("Rooms");
    }

}
