using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacteristics : MonoBehaviour
{

    [SerializeField] Text o2;
    [SerializeField] Text water;
    [SerializeField] Text food;
    [SerializeField] TMPro.TMP_Dropdown drop;
    [SerializeField] BuildRegulator BL;
    // Update is called once per frame
    void OnEnable(){
        drop.value = 0;
        ChangeCharacteristic();
    }
    public void ChangeCharacteristic()
    {
        o2.text = NPCStats.GetStatsByTag("Earth"/*tuka sloji da e value ot scrol za paneti*/)[0].ToString();
        water.text = NPCStats.GetStatsByTag("Earth"/*tuka sloji da e value ot scrol za paneti*/)[1].ToString();
        food.text = NPCStats.GetStatsByTag("Earth"/*tuka sloji da e value ot scrol za paneti*/)[2].ToString();
    }
}
