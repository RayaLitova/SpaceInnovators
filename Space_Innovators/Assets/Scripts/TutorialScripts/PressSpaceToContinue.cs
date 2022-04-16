using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressSpaceToContinue : MonoBehaviour
{   
    public Transform camera; 
    private string[] tutorialtext = {
        "Hi there, welcome to Space Innovators - a game in which you create and expand your own space station",
        "This here is your station. It only has three rooms now, but you can expand it by building new ones",
        "This room is the main room - this is where your minions/crewmates rest",
        "This is the oxygen room - from here oxygen is distributed across your station",
        "You can look at the status of any room by right-clicking on the working station",
        "This is the Navigation and Communication room - in here you can look at a map of nearby planets, make contact with alien civilizations and receive resources from Earth and other friendly planets",
        "Until then you can gather resources from tiny meteorites that fly by your station or receive them from Earth",
        "For now your storage is very limited",
        "If you want to expand it, you must build a storage room",
        "To build a room click on the 'Build' button and select the room you want to build",
        "Now try building a Lab - with this room you'll be able to discover blueprints for new rooms or upgrade ones that are already discovered",
        "From this menu you can see the requirements for your desired room and read the description to receive a better view of its functionality",
        "You can see your currently posessed resources and the maximum capacity of your station from the 'Storage' menu",
        "Your station cannot evolve without the help of your minions/crewmates - they go around the station and do assigned tasks",
        "Keep in mind that they don't know everything and are specialized in only 1 task, so be careful when choosing their profession",
        "Choose your new crewmates from the 'Crew' menu. Select their origin, which is responsible for their appearance and needs, and then choose their profession (Researcher)",
        "They require resources to survive. If you don't provide them enough resources they will slowly begin to die",
        "Space has exposed you to new illnesses. Keep your minions safe and healthy by regularly checking their health and sickness level by clicking on them",
        "That's everything you need to know, and remember, you must take care of your base and prevent it from destruction! Have fun with Space Innovators!"
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
        mariopositions[0] = new Vector2(0f, 0f);//o2
        mariopositions[1] = new Vector2(0f, 0f);
        mariopositions[2] = new Vector2(0f, 0f);
        mariopositions[3] = new Vector2(-7.75f, 0f);
        mariopositions[4] = new Vector2(-7.75f, 0f);
        mariopositions[5] = new Vector2(0f, 7.75f);//o2
        mariopositions[6] = new Vector2(0f, 7.75f);
        mariopositions[7] = new Vector2(0f, 7.75f);
        for(int i=8; i<17;i++)
            mariopositions[i] = new Vector2(0f, 0f);       

        minion.position = mariopositions[0];
        txt.text = tutorialtext[0];
    }

    void Update()
    {   
        if(Input.GetKeyDown("space") && currentPhrase<max_phases){
            currentPhrase++;
            minion.position = mariopositions[currentPhrase];
            camera.position = new Vector3(minion.position.x, minion.position.y, -2);    
            txt.text = tutorialtext[currentPhrase];
        } 

        if(currentPhrase>=max_phases){
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
