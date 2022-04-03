using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingCant : MonoBehaviour
{
    Text text;
    public byte fading = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(fading!=0){fading-=3;}
        text.color = new Color32(255,255,255,fading);
    }
}
