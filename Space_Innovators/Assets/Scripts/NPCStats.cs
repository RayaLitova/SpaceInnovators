using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    [SerializeField]public GameObject statCanvas;
    [SerializeField]public Transform camera;    // now you have to drag and drop your canvas in the editor in the script component
    public bool statActive = false;
    public bool following = false;

    // Start is called before the first frame update
    public void Activate()
    {
        statActive = !statActive;
            statCanvas.SetActive(statActive);
            following = !following;
    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(1)){
            Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(following){
            camera.position = new Vector3( transform.position.x, transform.position.y, -2);
        }
    }
}
