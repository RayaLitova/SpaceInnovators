using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCount : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown DropDown;
    [SerializeField] BuildRegulator BR;
    Text text;
    //string count;
    // Update is called once per frame
    void Start(){
        text = transform.GetComponent<Text>();
        
    }
    void Update()
    {   
        //count = DropDown.value;
        text.text = BR.onBoardCount[DropDown.value].ToString();
    }
}
