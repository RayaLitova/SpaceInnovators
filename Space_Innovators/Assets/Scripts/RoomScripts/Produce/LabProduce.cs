using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabProduce : MonoBehaviour
{
    [SerializeField] public List<GameObject> unlockableRooms;  
    [SerializeField] public List<string> unlockableUpgrades;  
    //[SerializeField] public List<string> availableUpgrades;  

    private int count = 0;
    private System.Random random = new System.Random();

    void Start(){
        unlockableUpgrades = GameObject.Find("marioIdle").GetComponent<BuildRegulator>().GetUnlockedRoomsToString();
    }

    public GameObject UnlockRoom(){
            GameObject room = unlockableRooms[0];
            unlockableRooms.Remove(room);
            count = random.Next(1, 4);
            GameObject.Find("marioIdle").GetComponent<NotificationControl>().CreateNotification("New room blueprint discovered!", "The lab has discovered the blueprint for "+room.name+"!");
            return room;
    }

    public GameObject UnlockUpgrade(){
        int upgrade = random.Next(unlockableUpgrades.Count);
        GameObject.Find("marioIdle").GetComponent<NotificationControl>().CreateNotification("New room upgrade discovered!", "The lab has discovered the blueprint for "+unlockableUpgrades[upgrade]+" upgrade!");
        GameObject.Find("marioIdle").GetComponent<BuildRegulator>().unlockedUpgrades.Add(unlockableUpgrades[upgrade]);
        unlockableUpgrades.RemoveAt(upgrade);
        return null;
    }

    public GameObject unlock(){
        count--;
        if(count==0){
            if(random.Next(1,3) == 1){
                return UnlockRoom();
            }else{
                return UnlockUpgrade(); //returns null (unlockedUpgrades already changed)
            }
        }
        if(count==0 || count==-1){
            count = random.Next(1, 4);
        }
        return null;
    }
}
