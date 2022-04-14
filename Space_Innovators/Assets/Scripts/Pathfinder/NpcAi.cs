using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class NpcAi : MonoBehaviour
{
    [SerializeField] public Transform actualTarget;//IN ROOM
    NPCStats Stats;

    float speed = 0.02f;
    public float nextWaipointDistance = 0f;

    Path path;
    int currentWaipoint = 0;
    
    float _t=0;
    float o2_timer = 0;
    float h2o_timer = 0;
    float food_timer = 0;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    private Animator bedAnim;
    [SerializeField] Transform NPCGFX;
    GetWorkNeeded Working;

    bool sleeping = false;
    bool isPanicked = false;
    string status;

    public Transform currentRoom;

    int currentPanicWaypoint = 0;

    Transform currentTarget; //IN ROOM
    [SerializeField]public Transform bed;//IN ROOM

    void Start()
    {
        Stats = transform.GetComponent<NPCStats>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = NPCGFX.GetComponent<Animator>();
        bedAnim = bed.GetComponent<Animator>();
        currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
        seeker.StartPath(rb.position, /**/currentTarget.transform.position, OnPathComplete);
        status = "wanderingForWork";
    }

    void FixedUpdate(){
        Consume();
        if(path == null){
            return;
        }
        Debug.Log(currentTarget);
        if(sleeping){
            anim.SetBool("Sleeping", true);
            bedAnim.SetBool("Sleeping", true);
        }else{
            anim.SetBool("Sleeping", false);
            bedAnim.SetBool("Sleeping", false);
        }

        Debug.Log(isPanicked);
        //------FIND WORK-------
        if(!isPanicked && !sleeping){
            if(status!="wanderingForWork" && actualTarget != null && (actualTarget.parent.gameObject.tag  == "Upgrading" || actualTarget.parent.gameObject.tag  == "Closed")){
                status = "wanderingForUpgrade";
            }

            if(status == "wanderingForWork"){
                if(GameObject.FindGameObjectsWithTag(Stats.targetTag).Length!=0){
                    actualTarget = GameObject.FindGameObjectsWithTag(Stats.targetTag)[0].transform;
                    actualTarget.tag = "Used:"+ actualTarget.tag;
                    RedirectCourse(actualTarget);
                    status = "default";
                }
            }else if(status == "wanderingForUpgrade"){
                if(actualTarget.parent.gameObject.tag != "Upgrading" && actualTarget.parent.gameObject.tag  != "Closed"){
                    RedirectCourse(actualTarget);
                    status = "default";
                }
            }

            if(currentTarget != null && currentTarget.parent.gameObject.tag == "Closed"){ //on target closed
                RedirectCourse(GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform);
            }
        
        }
        //----------Panic---------
        if(currentRoom!=null && currentRoom.gameObject.tag == "Closed"){
            if(!isPanicked){//execute once
                transform.position = currentRoom.position;
                sleeping = false;
                speed = 0.08f;
                RedirectCourse(GetPanicWaypoint().transform);
                isPanicked = true;   
            }
        }else if(isPanicked){
            if(actualTarget!=null && currentTarget!=actualTarget && currentTarget!=bed && currentTarget!=null){
                if(Stats.energy<Stats.max_energy)
                    currentTarget = bed;
                else currentTarget = actualTarget;
                RedirectCourse(currentTarget);
            }
            if(actualTarget == null){
                currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
                RedirectCourse(currentTarget);
            }
            
            isPanicked = false;
            if(status == "wanderingForWork" || status == "wanderingForUpgrade") speed = 0.02f;
            else speed = 0.04f;
        }

        //--------on path completed--------
        if(currentWaipoint >= path.vectorPath.Count){
            if(currentTarget == bed) sleeping = true;
            //-----------panicked--------------
            if(isPanicked){  
                sleeping = false;
                speed = 0.08f;
                RedirectCourse(GetPanicWaypoint().transform);
            
            //-------wandering----------
            }else if(!sleeping && (status == "wanderingForUpgrade" || status == "wanderingForWork")){ 
                sleeping = false;
                speed = 0.02f;           
                RedirectCourse(GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform);
            
            //---------normal state----------
            }else{ 
                speed = 0.04f;
                if(!sleeping){
                    Stats.energy--;
                    if(currentTarget.parent.gameObject.GetComponent<RoomStatics>()!=null) currentTarget.parent.gameObject.GetComponent<RoomStatics>().Produce();
                }else{
                    Stats.energy++;
                }
            }

        }

        //----------sleeping--------------
        if(Stats.energy <= 0.1*Stats.max_energy && sleeping == false && currentTarget!=bed){
            RedirectCourse(bed);
            //sleeping = true;
        }else if(Stats.energy >= Stats.max_energy && sleeping == true ){
            RedirectCourse(actualTarget);
            sleeping = false;
        }  
        anim.SetInteger("Speed",0);


        //-----------------------direction------------------------
        if(path.vectorPath[currentWaipoint].x > transform.localPosition.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){
            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 1);
            anim.SetFloat("Y", 0);
            }
        else if(path.vectorPath[currentWaipoint].x < transform.localPosition.x && 
        ((int)path.vectorPath[currentWaipoint].y == (int)transform.localPosition.y)){

            anim.SetInteger("Speed",1);
            anim.SetFloat("X", -1);
            anim.SetFloat("Y", 0);

        }
        else if(path.vectorPath[currentWaipoint].y > transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){

            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 1);

        }
        else if(path.vectorPath[currentWaipoint].y < transform.localPosition.y && 
        ( (int)path.vectorPath[currentWaipoint].x == (int)transform.localPosition.x)){
            anim.SetInteger("Speed",1);
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1);
        }

        if(!sleeping){
            transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentWaipoint], speed);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaipoint]);
        
        if(distance < nextWaipointDistance /*&& currentWaipoint < path.vectorPath.Count-1*/){
            currentWaipoint++;
        }
    }
    

    void Consume(){
        o2_timer += Time.deltaTime;
        h2o_timer += Time.deltaTime;
        food_timer += Time.deltaTime;

        if(o2_timer >= 20f){
            Stats.Consume("O2", Stats.O2);
            o2_timer =0;
        }
        if(h2o_timer >= 30f ){
            Stats.Consume("Water", Stats.Water);
            h2o_timer =0;
        }
        if(food_timer >= 40f ){
            Stats.Consume("Food", Stats.Food);
            food_timer=0;
        }
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaipoint = 0;
            anim.SetInteger("Speed",0);
        }
    }

    void RedirectCourse(Transform t){
        seeker.StartPath(rb.position, t.position, OnPathComplete);
        currentWaipoint = 0;
        currentTarget = t;
    }

    public void Die(){
        if(actualTarget!=null) actualTarget.tag = actualTarget.tag.Split(':')[1];
        bed.tag = "Bed";
        BuildRegulator mario = GameObject.Find("marioIdle").GetComponent<BuildRegulator>();
        mario.onBoardCount[transform.tag.Split('-')[0]]--;
        Destroy(gameObject);
    }

    GameObject GetPanicWaypoint(){
        currentPanicWaypoint++;
        if(currentPanicWaypoint==3) currentPanicWaypoint = 0;
        return currentRoom.Find("SmallRoom").Find("Panic("+currentPanicWaypoint+")").gameObject;
    }
}
