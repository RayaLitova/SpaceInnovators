using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabProduce : MonoBehaviour
{
    [SerializeField] public List<GameObject> unlockableRooms;  
    [SerializeField] public List<GameObject> upgrades;   

    private int unlock = 0;
    private System.Random random = new System.Random();

    public GameObject UnlockRoom(){
        if(unlock==0){
            GameObject room = unlockableRooms[random.Next(unlockableRooms.Count)];
            unlockableRooms.Remove(room);
            unlock = random.Next(1, 4);
            return room;
        }
        unlock--;
        if(unlock==0 || unlock==-1){
            unlock = random.Next(1, 4);
        }
        return null;
    }
}
