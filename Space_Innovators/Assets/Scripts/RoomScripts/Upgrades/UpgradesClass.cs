using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class Upgrade{
	public string roomName;
	public string description;
	public Dictionary<string, int> requirements = new Dictionary<string, int>();
	public int requiredLevel = 1;
	public int upgrade_at=100;
	public bool isAvailable = true;
	public int levelToNext = 0;

	public virtual void ApplyProgress(RoomStatics room){
		Debug.Log("Progress Applied");
	}
	public virtual bool CheckRequirements(RoomStatics room){
		Debug.Log("Checked requirements");
		return false;
	}
}	


[System.Serializable]
public class ResourceUpgrade : Upgrade{
	public string[] resourcesUpgrade;
	public int[] resourcesUpgradeQuantity;

	public ResourceUpgrade(RoomStatics room, string name, string desc, Dictionary<string, int> req, int upgrade = 100,  int level = 1, int levelTo = 0){
		roomName = name;
		description = desc;
		requirements = req;
		requiredLevel = level;
		upgrade_at = upgrade;
		resourcesUpgrade = new string[10];
		resourcesUpgradeQuantity = new int[10];
		levelToNext = levelTo;
		isAvailable = CheckRequirements(room);
	}

	public void AddResourceUpgrade(string name, int quantity){
		resourcesUpgrade = resourcesUpgrade.Concat(new string[] {name}).ToArray();
		resourcesUpgradeQuantity = resourcesUpgradeQuantity.Concat(new int[] {quantity}).ToArray();
	}

	public void AddRequirements(string name, int quantity){
		requirements.Add(name,quantity);
	}

	public override void ApplyProgress(RoomStatics room){
		for(int i=0; i<requirements.Count; i++){
			GameObject.Find("marioIdle").GetComponent<ResourcesClass>().SubtractResource(requirements.ElementAt(i).Key, requirements.ElementAt(i).Value);
			requirements[requirements.ElementAt(i).Key] *= 4/3;
		}
		room.producedResources = room.producedResources.Concat(resourcesUpgrade).ToArray();
		room.producedResourcesQuantity = room.producedResourcesQuantity.Concat(resourcesUpgradeQuantity).ToArray();
		requiredLevel+=levelToNext;
		isAvailable = CheckRequirements(room);
	}

	public override bool CheckRequirements(RoomStatics room){
        int SatisfiedRequirements = 0;
        for(int i=0; i<requirements.Count; i++){
            if(GameObject.Find("marioIdle").GetComponent<ResourcesClass>().resources[requirements.ElementAt(i).Key] >= requirements.ElementAt(i).Value) SatisfiedRequirements++;
        }
        if(SatisfiedRequirements < requirements.Count) return false;
        if(requiredLevel > room.roomLevel) return false;
        return true;
	}
}
