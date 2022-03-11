using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChageOptions : MonoBehaviour
{
    //public static Dictionary < string, Transform > Stations = new Dictionary < string, Transform > ();
    // Start is called before the first frame update
    public Transform list;
    ChangeTarget options;
 
         
    void Start()
    {
        options = list.GetComponent<ChangeTarget>();
        var dropdown = transform.GetComponent<Dropdown>(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
