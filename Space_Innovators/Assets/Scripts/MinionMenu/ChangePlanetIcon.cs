using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlanetIcon : MonoBehaviour
{
    public string Planet;
    public string Profession;
    public Image image;
    public Text O2;
    public Text Water;
    public Text Food;
    public Text Energy;
    public Text Health;
    public Text Count;
    [SerializeField] TMPro.TMP_Dropdown DropDown;

    // Update is called once per frame
    void Update()
    {
        image.sprite = GameObject.Find("marioIdle").GetComponent<BuildRegulator>().planetIcons[Planet];
        int[] stats = NPCStats.GetStatsByTag(Planet);
        O2.text = stats[0].ToString();
        Water.text = stats[1].ToString();
        Food.text = stats[2].ToString();
        Energy.text = stats[3].ToString();
        Health.text = stats[4].ToString();
        Count.text = GameObject.Find("marioIdle").GetComponent<BuildRegulator>().onBoardCount[Planet].ToString();
        //UpdateOptions();
    }

    public void UpdateOptions(){
        DropDown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("");
        foreach(string go in GameObject.Find("marioIdle").GetComponent<BuildRegulator>().ProfessionsForPlanet[Planet]){
           options.Add(go);
        }
        DropDown.AddOptions(options);
        DropDown.RefreshShownValue();
    }
}
