using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class NpcAi2 : MonoBehaviour
{
    [SerializeField] public Transform actualTarget;//IN ROOM
    NPCStats Stats;

    float speed = 0.02f;
    public float nextWaipointDistance = 0f;

    Path path;
    int currentWaipoint = 0;
    bool reachedEndofPath = false;
    
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

        if(sleeping){
            anim.SetBool("Sleeping", true);
            bedAnim.SetBool("Sleeping", true);
        }else{
            anim.SetBool("Sleeping", false);
            bedAnim.SetBool("Sleeping", false);
        }

        //------FIND WORK-------
        if(!isPanicked){
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

            if(actualTarget != null && (actualTarget.parent.gameObject.tag  == "Upgrading" || actualTarget.parent.gameObject.tag  == "Closed")){
                status = "wanderingForUpgrade";
                isPanicked = false;
            }
        }
        //----------Panic---------
        if(currentRoom!=null && currentRoom.gameObject.tag == "Closed"){
            if(!isPanicked){//execute once
                sleeping = false;
                speed = 0.08f;
                RedirectCourse(GetPanicWaypoint().transform);
                isPanicked = true;   
            }
        }else if(currentTarget != null && currentTarget.parent.gameObject.tag == "Closed"){ //on target closed
            if(currentTarget==actualTarget || actualTarget == null || currentTarget==bed){ //execute once
                do{
                    currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
                    if(actualTarget == null) actualTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
                }while(currentTarget.parent.gameObject.tag == "Closed" || actualTarget == currentTarget);
                RedirectCourse(currentTarget);
                isPanicked = false;
            }
        }else{
            if(actualTarget!=null && currentTarget!=actualTarget && currentTarget!=bed && currentTarget!=null){
                if(Stats.energy<Stats.max_energy)
                    currentTarget = bed;
                else currentTarget = actualTarget;
                RedirectCourse(currentTarget);
            }
            if(currentTarget == null){
                currentTarget = GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform;
                RedirectCourse(currentTarget);
            }
            
            isPanicked = false;
        }

        //--------on path completed--------
        if(currentWaipoint >= path.vectorPath.Count){
            reachedEndofPath = true;
            if(currentTarget == bed) sleeping = true;
            //-----------panicked--------------
            if(isPanicked){  
                sleeping = false;
                speed = 0.08f;
                RedirectCourse(GetPanicWaypoint().transform);
            
            //-------wandering----------
            }else if(status == "wanderingForUpgrade" || status == "wanderingForWork"){ 
                sleeping = false;
                speed = 0.02f;           
                RedirectCourse(GameObject.FindGameObjectsWithTag("Excursion")[Random.Range(0, GameObject.FindGameObjectsWithTag("Excursion").Length)].transform);
            
            //---------normal state----------
            }else{ 
                speed = 0.04f;
                if(!sleeping){
                    Stats.energy--;
                    if(currentTarget!=null) currentTarget.parent.gameObject.GetComponent<RoomStatics>().Produce();
                }else{
                    Stats.energy++;
                }
            }

        }else{ //--------not reached end of path-------------
            reachedEndofPath = false;
        }

        //----------sleeping--------------
        if(Stats.energy <= 0.1*Stats.max_energy && sleeping == false ){
            RedirectCourse(bed);
            //sleeping = true;
        }else if(Stats.energy >= Stats.max_energy && sleeping == true ){
            RedirectCourse(/**/currentTarget.transform);
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

        if(!anim.GetBool("Sleeping")){
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
        actualTarget.tag = actualTarget.tag.Split(':')[1];
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
