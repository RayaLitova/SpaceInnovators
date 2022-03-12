 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string name = "None";
    public string icon_name = "None";
    public int quantity = 0;

    public Resource(string name, string icon_name, int quantity){
        this.name = name;
        this.icon_name = icon_name;
        this.quantity = quantity;
    }

    public Resource(){
        this.name = "None";
        this.icon_name = "None";
        this.quantity = 0;
    }

    public SerializableClass ResourceSerialize(){
        SerializableClass temp = new SerializableClass();
        temp.arr[0] = this.name;
        temp.arr[1] = this.icon_name;
        temp.arr[2] = this.quantity.ToString();

        return temp;
    }
}
