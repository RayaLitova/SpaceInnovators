using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressSpaceToContinue : MonoBehaviour
{   
    public Transform camera; 
    private string[] tutorialtext = {
        "Hi there, welcome to Space Innovators - a game in which you create and expand your own space station",
        "This here is your station. It only has three rooms now, but you can expand it by building new rooms",
        "And these people here are your minions/crewmates, they go around the station and do asigned tasks",
        "Keep in mind that they don't know everything and are specialized in only 1 task, so be careful when choosing new minions/crewmates",
        "This room is the main room - this is where your minions/crewmates stay when they don't have any tasks to do",
        "This is the oxygen room - from here oxygen is distributed across your station",
        "There are oxygen tanks that have to be replaced once they have been emptied or else the oxygen will run out and your crew will pass out",
        "This the Navigation and Communication room - in here you can look at a map of nearby planets, make contact with alien civilizations and send minions/crewmates on missions for resources once you have a shuttle constructed",
        "Until then you can send missions to other planets or trade with alien civilizations, you can gather resources from tiny meteorites that fly by your station",
        "Click on this meteorite to claim its resources",
        "For now your storage is very limited",
        "If you want to expand it, you must build a storage room",
        "To build a room, come talk to me (right click) and select the 'build room' option",
        "Now try building a Lab - with this room you will be able to construct more complex technology",
        "That's everything you have to know, and remember, you must take care of your base and prevent it from destruction",
        "'ъъъъм аз половината време дори не знам какво казвам'"
    };

    private Vector2[] mariopositions = new Vector2[20];
    private int currentPhrase = 0;
    public Text txt;
    public Transform mario;
    private int max_phases;
    public Transform minion;
    public Transform meteorite;

    bool flag=false;

    void Start(){
        max_phases = tutorialtext.Length;
        for(int i=0;i<tutorialtext.Length;i++){
            mariopositions[i] = new Vector2(1f,0.2f);
        }
        mariopositions[4] = new Vector2(6.6f, -1.9f);
        mariopositions[5] = new Vector2(-4.7f, -1.3f);
        mariopositions[6] = new Vector2(-4.7f, -1.3f);
        mariopositions[7] = new Vector2(3.7f, 6.3f);
        

        mario.position = mariopositions[0];
        txt.text = tutorialtext[0];
    }

    void Update()
    {   
        if(currentPhrase == 2 || currentPhrase == 3){
            camera.position = new Vector3(minion.position.x, minion.position.y, -2);
        }else if(currentPhrase == 4 || currentPhrase == 5 || currentPhrase == 6 || currentPhrase == 7){
            camera.position = new Vector3(mario.position.x, mario.position.y, -2);
        }else if(currentPhrase == 9){
            if(!flag){
                meteorite.gameObject.GetComponent<Meteorite>().ShowMeteorite();
                flag=true;
            }
            camera.position = new Vector3(meteorite.position.x, meteorite.position.y, -2);
        }

        if(Input.GetKeyDown("space") && currentPhrase<max_phases){
            currentPhrase++;
            mario.position = mariopositions[currentPhrase];
            txt.text = tutorialtext[currentPhrase];
        } 

        if(currentPhrase>=max_phases){
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
