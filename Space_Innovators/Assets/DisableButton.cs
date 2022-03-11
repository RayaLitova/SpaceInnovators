using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public Sprite DisabledButton;

    void Start()
    {
        if(!File.Exists(Application.persistentDataPath + "/progress.data")){
            gameObject.GetComponent<EventTrigger>().enabled = false;
            gameObject.GetComponent<Image>().sprite = DisabledButton;
        }
    }
}
