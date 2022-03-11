using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWorkNeeded : MonoBehaviour
{
    static int work = 0;
    public string name = "name";
    [SerializeField]private int produce_at = 150;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Produce(){
       work++;
       if(work>=produce_at){
           //
           work=0;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
