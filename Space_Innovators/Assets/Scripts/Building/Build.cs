using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] BuildRegulator BL;
    //[SerializeField]GameObject to_build;

    public void build(){
        BL.newX = 6;
        BL.newY = 6;
        BL.buildRoom(BL.newX,BL.newY,3);
        /*Vector3 objectPOS = Vector3.zero;
        objectPOS = new Vector2(-1.3f,-1.2f);
        GameObject newGameObject = Instantiate(to_build, objectPOS, Quaternion.identity);
        BL.map[BL.newX,BL.newY] = 1;

        AstarPath.active.Scan();
        BL.UpdateInteractables();*/
    }
 
}
