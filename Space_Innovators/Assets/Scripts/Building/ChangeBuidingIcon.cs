using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBuidingIcon : MonoBehaviour
{
    [SerializeField] public Sprite[] Images;
    [SerializeField] Image img;
    [SerializeField] TMPro.TMP_Dropdown drop;
    
    void OnEnable(){
        drop.value = 0;
    }

    public void ChangeImage(){
        img.sprite = Images[drop.value];
    }

    public void ChangeCharacteristic(){
        img.sprite = Images[drop.value];
    }

}
