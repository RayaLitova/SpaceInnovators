using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
 
[CustomEditor(typeof(UpgradesGameObject))]
 
public class CustomListEditor : Editor {
 
    enum displayFieldType {DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields}
    displayFieldType DisplayFieldType;
 
    UpgradesGameObject t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;
 
    void OnEnable(){
        t = (UpgradesGameObject)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("upgrades"); // Find the List in our script and create a refrence of it
    }
 
    public override void OnInspectorGUI(){
        GetTarget.Update();
   
        //Choose how to display the list<> Example purposes only
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("",DisplayFieldType);
   
        //Resize our list
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.LabelField("Define the list size with a number");
        ListSize = ThisList.arraySize;
        ListSize = EditorGUILayout.IntField ("List Size", ListSize);
   
        if(ListSize != ThisList.arraySize){
            while(ListSize > ThisList.arraySize){
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while(ListSize < ThisList.arraySize){
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }
   
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.LabelField("Or");
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
   
        EditorGUILayout.LabelField("Add a new item with a button");
   
        if(GUILayout.Button("Add New")){
            t.upgrades.Add(new Upgrade());
        }
   
        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        GetTarget.ApplyModifiedProperties();
    }
}